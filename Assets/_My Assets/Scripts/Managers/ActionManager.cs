using System;
using UnityEngine;

namespace FruitCutter
{
    public class ActionManager : MonoBehaviour
    {
        #region GameScene UI Actions

        internal static Action OnStartCounter;
        internal static Action OnResetGameSceneUi;

        internal static Action<bool> ToggleMainMenuUI;
        internal static Action<bool> ToggleLevelUI;
        internal static Action<bool> ToggleGameOverUI;
        internal static Action<bool> ToggleGameSceneUI;
        #endregion GameScene UI Actions

        internal static Action<bool> ToggleImageTracker;
        
        internal static Action<Rigidbody> OnRespawnFruit;
        internal static Action OnStartFruitSpwan;
        internal static Action OnResetAllFruits;
        internal static Action OnStopFruitSpwan;
        internal static Action<Rigidbody> OnDeactivateFruit;

        internal static Action OnlevelStart;
        internal static Action OnGameOver;


        internal static Action OnEarnScore;
        internal static Action OnResetPlayerData;
        internal static Func<int> GetPlayerCurrentScore;
        internal static Func<int> GetPlayerCurrentlives;
        internal static Action<int> OnUpdatePlayerScore;

        internal static Action OnPlayFruitCutAudio;
    }
}

