using System;
using System.IO;
using UnityEngine;

public class RunStateManager : MonoBehaviour
{
    public static RunStateManager Instance { get; private set; }

    public RunState CurrentRun { get; private set; }

    public event Action<RunState> OnRunStarted;
    public event Action<RunState> OnRunEnded;
    public event Action OnRunReset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public RunState CreateNewRun(long? seed = null)
    {
        CurrentRun = new RunState();
        if (seed.HasValue)
            CurrentRun.seed = seed.Value;
        CurrentRun.startedAt = DateTime.UtcNow.ToString("o");
        OnRunStarted?.Invoke(CurrentRun);
        return CurrentRun;
    }

    public string SaveRunToJson(string fileName = null)
    {
        if (CurrentRun == null)
            throw new InvalidOperationException("No current run to save.");

        string json = JsonUtility.ToJson(CurrentRun, true);
        string path = Application.persistentDataPath;
        if (string.IsNullOrEmpty(fileName)) fileName = $"run_{CurrentRun.runId}.json";
        string full = Path.Combine(path, fileName);
        File.WriteAllText(full, json);
        return full;
    }

    public RunState LoadRunFromJson(string path)
    {
        if (!File.Exists(path)) throw new FileNotFoundException(path);
        string json = File.ReadAllText(path);
        CurrentRun = JsonUtility.FromJson<RunState>(json);
        return CurrentRun;
    }

    public void EndRun(string result = null)
    {
        if (CurrentRun == null) return;
        CurrentRun.endedAt = DateTime.UtcNow.ToString("o");
        OnRunEnded?.Invoke(CurrentRun);
    }

    public void ResetRun()
    {
        CurrentRun = null;
        OnRunReset?.Invoke();
    }
}