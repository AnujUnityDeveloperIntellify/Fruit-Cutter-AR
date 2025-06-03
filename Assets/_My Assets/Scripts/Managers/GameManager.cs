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
        [SerializeField] private bool TestTarget;
        // Start is called before the first frame update
        void Start()
        {
            if(!TestTarget)
            {
                ActionManager.ToggleImageTracker?.Invoke(false);
            }
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
            if(TestTarget)
            {
                TargetStatusTxt.text = "Target Found";
            }
           
#if UNITY_ANDROID
            if (currentLevelState == LevelState.YetToStart) 
            {
                currentLevelState = LevelState.Start;
                ActionManager.OnStartCounter?.Invoke();
            }
#endif
        }
        public void OnTargetImageLost()
        {
            if(TestTarget)
            {
                TargetStatusTxt.text = "Target Lost";
            }
            
            /* if (currentLevelState == LevelState.Start)
             {
                 ActionManager.OnGameOver?.Invoke();
             }*/
        }

        private void LevelStart()
        {
            ActionManager.OnResetPlayerData?.Invoke();  
            ActionManager.ToggleLevelUI?.Invoke(true);
            ActionManager.OnStartFruitSpwan?.Invoke();
        }
        private void GameOver()
        {
            currentLevelState = LevelState.None;
            ActionManager.ToggleGameOverUI?.Invoke(true);
            ActionManager.ToggleLevelUI?.Invoke(false);
            ActionManager.ToggleGameSceneUI?.Invoke(false);
            
        }
        private void ToggleImageTracker(bool isActive)
        {
            observerBehaviour.enabled = isActive;
        }
    }
}

