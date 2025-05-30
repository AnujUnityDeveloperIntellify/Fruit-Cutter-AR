using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FruitCutter
{
    public class GameSceneUiController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI countDownTxt;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button startBtn;
        [SerializeField] private Button restartBtn;
        [SerializeField] private GameObject LevelUIHolder;
        [SerializeField] private GameObject GameOverUIHolder;
        [SerializeField] private GameObject MainMenuUIHolder;

        private const float animationDuration = 0.6f;
        private const float delayBetweenTexts = 0.2f;

        [SerializeField] private float startFontSize;
        [SerializeField] private float endFontSize;
        #endregion Variables

        #region Unity Methods
        private void Awake()
        {
            startBtn.onClick.AddListener(OnClickStartBtn);
            restartBtn.onClick.AddListener(OnClickRestartBtn);
        }
        void Start()
        {

        }
        private void OnEnable()
        {
            ActionManager.OnStartCounter += StartCounter;
            ActionManager.OnResetGameSceneUi += ResetGameSceneUI;

            ActionManager.ToggleGameOverUI += ToggleGameOverUI;
            ActionManager.ToggleMainMenuUI += ToggleMainMenuUI;
            ActionManager.ToggleLevelUI += ToggleLevelUI;
            
        }
        private void OnDisable()
        {
            ActionManager.OnStartCounter -= StartCounter;
            ActionManager.OnResetGameSceneUi -= ResetGameSceneUI;

            ActionManager.ToggleGameOverUI -= ToggleGameOverUI;
            ActionManager.ToggleMainMenuUI -= ToggleMainMenuUI;
            ActionManager.ToggleLevelUI -= ToggleLevelUI;
        }

        #endregion Unity Methods

        #region Custom Methods
        private void StartCounter()
        {
            StartCoroutine(StartCountDown());
        }
        private IEnumerator StartCountDown()
        {
            countDownTxt.gameObject.SetActive(true);    

            yield return new WaitForSeconds(0.6f);
            yield return AnimateText("3");
            yield return new WaitForSeconds(delayBetweenTexts);

            yield return AnimateText("2");
            yield return new WaitForSeconds(delayBetweenTexts);

            yield return AnimateText("1");
            yield return new WaitForSeconds(delayBetweenTexts);

            countDownTxt.gameObject.SetActive(false);
            ActionManager.OnlevelStart?.Invoke();

        }
        private IEnumerator AnimateText(string message)
        {
            countDownTxt.text = message;
            countDownTxt.fontSize = startFontSize;
            countDownTxt.gameObject.SetActive(true);

            float elapsedTime = 0f;
            while (elapsedTime < animationDuration)
            {
                float t = elapsedTime / animationDuration;
                float newSize = Mathf.Lerp(startFontSize, endFontSize, t);
                countDownTxt.fontSize = newSize;

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            countDownTxt.fontSize = endFontSize;
        }

        private void ResetGameSceneUI()
        {
            scoreText.text = "";
        }
        private void OnClickStartBtn()
        {
            GameManager.currentLevelState = LevelState.YetToStart;
            ActionManager.ToggleMainMenuUI?.Invoke(false);
            ActionManager.ToggleImageTracker?.Invoke(true);
        }
        private void OnClickRestartBtn()
        {
            ActionManager.ToggleMainMenuUI?.Invoke(true);
            ActionManager.ToggleGameOverUI?.Invoke(false);
        }
        private void ToggleGameOverUI(bool isActive)
        {
            GameOverUIHolder.SetActive(isActive);   
        }
        private void ToggleMainMenuUI(bool isActive)
        {
            MainMenuUIHolder.SetActive(isActive);   
        }
        private void ToggleLevelUI(bool isActive)
        {
            LevelUIHolder.SetActive(isActive);  
        }
        #endregion Custom Methods

    }
}

