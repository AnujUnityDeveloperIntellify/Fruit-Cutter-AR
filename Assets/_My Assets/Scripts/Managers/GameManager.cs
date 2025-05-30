using System.Collections;
using System.Collections.Generic;
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
        private string FruitTag = "Fruit";
        // Start is called before the first frame update
        void Start()
        {
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

        public void OnTragetImageFound()
        {
            if (currentLevelState == LevelState.YetToStart) 
            {
                currentLevelState = LevelState.Start;
                ActionManager.OnStartCounter?.Invoke();
            }
        }
        public void OnTargetImageLost()
        {
           /* if (currentLevelState == LevelState.Start)
            {
                ActionManager.OnGameOver?.Invoke();
            }*/
        }

        private void LevelStart()
        {
            ActionManager.OnResetGameSceneUi?.Invoke();
            ActionManager.ToggleLevelUI?.Invoke(true);
            ActionManager.OnStartFruitSpwan?.Invoke();
        }
        private void GameOver()
        {
            currentLevelState = LevelState.None;
            ActionManager.ToggleLevelUI?.Invoke(false);
            ActionManager.ToggleGameOverUI?.Invoke(true);
        }
        private void ToggleImageTracker(bool isActive)
        {
            observerBehaviour.enabled = isActive;
        }
    }
}

