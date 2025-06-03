using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FruitCutter
{
    public class GameOverUIController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Button restartBtn;
        [SerializeField] private TextMeshProUGUI gameOverTxt;

        private const float animationDuration = 0.4f;
        [SerializeField] private float startFontSize;
        [SerializeField] private float endFontSize;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            restartBtn.onClick.AddListener(OnClickRestartBtn);
        }
        void Start()
        {

        }
        private void OnEnable()
        {
            StartGameOverAnimation();
        }
        private void OnDisable()
        {
            ResetGameOverTxt();
        }

        #endregion Unity Methods

        #region Custom Methods
        private void OnClickRestartBtn()
        {
            ActionManager.ToggleMainMenuUI?.Invoke(true);
            ActionManager.ToggleGameOverUI?.Invoke(false);
        }
        private void ResetGameOverTxt()
        {
            gameOverTxt.gameObject.SetActive(false);    
            gameOverTxt.fontSize = startFontSize;
        }
        private void StartGameOverAnimation()
        {
            restartBtn.interactable = false;
            StartCoroutine(AnimateText("Game Over"));
        }
        private IEnumerator AnimateText(string message)
        {
            gameOverTxt.text = message;
            gameOverTxt.fontSize = startFontSize;
            gameOverTxt.gameObject.SetActive(true);

            float elapsedTime = 0f;
            while (elapsedTime < animationDuration)
            {
                float t = elapsedTime / animationDuration;
                float newSize = Mathf.Lerp(startFontSize, endFontSize, t);
                gameOverTxt.fontSize = newSize;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            gameOverTxt.fontSize = endFontSize;
            restartBtn.interactable = true;
        }
        #endregion Custom Methods

    }
}

