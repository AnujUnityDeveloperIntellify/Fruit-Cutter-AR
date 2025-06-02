using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCutter
{
    public class MainMenuUIController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Button startBtn;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            startBtn.onClick.AddListener(OnClickStartBtn);  
        }
        void Start()
        {

        }


        #endregion Unity Methods

        #region Custom Methods
        private void OnClickStartBtn()
        {
            GameManager.currentLevelState = LevelState.YetToStart;
            ActionManager.ToggleMainMenuUI?.Invoke(false);
            ActionManager.ToggleGameSceneUI?.Invoke(true);
#if UNITY_ANDROID
            ActionManager.ToggleImageTracker?.Invoke(true);
#endif
        }
        #endregion Custom Methods

    }
}

