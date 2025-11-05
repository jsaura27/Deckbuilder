using System;
using System.Collections.Generic;

public interface ICardEffect
{
    void Resolve();
}

public class EffectPipeline
{
    private Queue<ICardEffect> queue = new Queue<ICardEffect>();

    public void Enqueue(ICardEffect e) => queue.Enqueue(e);

    public void ProcessAll()
    {
        while (queue.Count > 0)
        {
            var e = queue.Dequeue();
            try { e.Resolve(); } catch (Exception ex) { UnityEngine.Debug.LogError(ex); }
        }
    }
}
