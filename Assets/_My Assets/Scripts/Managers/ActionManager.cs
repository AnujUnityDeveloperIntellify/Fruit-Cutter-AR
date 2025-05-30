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
        #endregion GameScene UI Actions

        internal static Action<bool> ToggleImageTracker;
        internal static Action OnRespwanFruit;
        internal static Action OnStartFruitSpwan;

        internal static Action OnlevelStart;
        internal static Action OnGameOver;
    }
}

