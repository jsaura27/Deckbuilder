using System;
using UnityEngine;

namespace Deckbuilder.Combat
{
    // Minimal, compile-safe TurnManager skeleton to start implementation
    [Serializable]
    public class TurnManager : MonoBehaviour
    {
        public int CurrentTurn { get; private set; } = 0;

        public event Action<int> OnTurnStarted;

        public void StartTurn()
        {
            CurrentTurn++;
            OnTurnStarted?.Invoke(CurrentTurn);
        }

        public void ResetTurns()
        {
            CurrentTurn = 0;
        }
    }
}
