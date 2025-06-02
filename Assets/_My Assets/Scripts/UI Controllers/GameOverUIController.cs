using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCutter
{
    public class GameOverUIController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Button restartBtn;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            restartBtn.onClick.AddListener(OnClickRestartBtn);
        }
        void Start()
        {

        }


        #endregion Unity Methods

        #region Custom Methods
        private void OnClickRestartBtn()
        {
            ActionManager.ToggleMainMenuUI?.Invoke(true);
            ActionManager.ToggleGameOverUI?.Invoke(false);
        }
        #endregion Custom Methods

    }
}

