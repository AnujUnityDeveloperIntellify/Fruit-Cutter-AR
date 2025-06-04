using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vuforia;

namespace FruitCutter
{
    public enum LevelState
    {
        None,
        YetToStart,
        Start,
        Complete
    }
    public class GameManager : MonoBehaviour
    {
        internal static LevelState currentLevelState;
        [SerializeField] private ObserverBehaviour observerBehaviour;
        internal static string FruitTag = "Fruit";
        [SerializeField] private TextMeshProUGUI TargetStatusTxt;
        internal const float objectSpwanDistance = 1.5f;
        // [SerializeField] private bool TestTarget;
        // Start is called before the first frame update
        void Start()
        {
            if (observerBehaviour)
            {
                observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
            }
            ActionManager.ToggleImageTracker?.Invoke(false);
        }
        private void OnEnable()
        {
            currentLevelState = LevelState.None;
            ActionManager.OnGameOver += GameOver;
            ActionManager.OnlevelStart += LevelStart;
            ActionManager.ToggleImageTracker += ToggleImageTracker;

        }
        private void OnDisable()
        {
            ActionManager.ToggleImageTracker -= ToggleImageTracker;
            ActionManager.OnlevelStart -= LevelStart;
            ActionManager.OnGameOver -= GameOver;
        }

        private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
        {
            TargetStatusTxt.text = $" {targetStatus.Status} | {targetStatus.StatusInfo}";
            if (targetStatus.Status == Status.TRACKED)
            {
                if (currentLevelState == LevelState.YetToStart)
                {
                    currentLevelState = LevelState.Start;
                    ActionManager.OnStartCounter?.Invoke();
                }
            }
            else 
            {
                if (currentLevelState == LevelState.Start)
                {
                    ActionManager.OnStopFruitSpwan?.Invoke();
                    ActionManager.OnResetAllFruits?.Invoke();
                    ActionManager.OnGameOver?.Invoke();
                }
            }
            //if (targetStatus.Status == Status.EXTENDED_TRACKED)
        }
        private void LevelStart()
        {
            ActionManager.OnResetPlayerData?.Invoke();
            ActionManager.ToggleLevelUI?.Invoke(true);
            ActionManager.OnStartFruitSpwan?.Invoke();
        }
        private void GameOver()
        {
            ActionManager.OnSavedHighestScore?.Invoke();
            ActionManager.ToggleImageTracker?.Invoke(false);
            currentLevelState = LevelState.None;
            ActionManager.ToggleGameOverUI?.Invoke(true);
            ActionManager.ToggleLevelUI?.Invoke(false);
            ActionManager.ToggleGameSceneUI?.Invoke(false);

        }
        private void ToggleImageTracker(bool isActive)
        {
            if (isActive)
            {
                StartCoroutine(RefreshObserver());
            }
            else
            {
                observerBehaviour.enabled = false;
            }
        }
        private IEnumerator RefreshObserver()
        {
            observerBehaviour.enabled = false;
            yield return null;
            observerBehaviour.enabled = true;
        }
    }
}

