using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private GameObject MainMenuUIHolder;
        [SerializeField] private GameObject GameOverUIHolder;
        [SerializeField] private GameObject GameSceneUIHolder;
        #endregion Variables

        #region Unity Methods

        void Start()
        {

        }
        private void OnEnable()
        {
            ActionManager.ToggleMainMenuUI += ToggleMainMenuUI;
            ActionManager.ToggleGameOverUI += ToggleGameOverUI;
            ActionManager.ToggleGameSceneUI += ToggleGameSceneUI;
            //ActionManager.ToggleImageTracker?.Invoke(true);
        }
        private void OnDisable()
        {
            ActionManager.ToggleMainMenuUI -= ToggleMainMenuUI;
            ActionManager.ToggleGameOverUI -= ToggleGameOverUI;
            ActionManager.ToggleGameSceneUI -= ToggleGameSceneUI;   
        }

        #endregion Unity Methods

        #region Custom Methods
        private void ToggleMainMenuUI(bool isActive)
        {
            MainMenuUIHolder.SetActive(isActive);
        }
        private void ToggleGameOverUI(bool isActive)
        {
            GameOverUIHolder.SetActive(isActive);
        }
        private  void ToggleGameSceneUI(bool isActive)
        {
            GameSceneUIHolder.SetActive(isActive);  
        }
        #endregion Custom Methods

    }

}
