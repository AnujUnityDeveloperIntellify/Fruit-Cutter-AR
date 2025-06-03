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
        [SerializeField] private GameObject LevelUIHolder;

        [SerializeField] private GameObject HighestScoreHolder;
        [SerializeField] private TextMeshProUGUI highestScoreText;

        private const float animationDuration = 0.6f;
        private const float delayBetweenTexts = 0.2f;

        [SerializeField] private float startFontSize;
        [SerializeField] private float endFontSize;
        #endregion Variables

        #region Unity Methods
        private void Awake()
        {
            
        }
        void Start()
        {

        }
        private void OnEnable()
        {
           // Debug.LogError("CU State "+ GameManager.currentLevelState);
            ActionManager.OnStartCounter += StartCounter;
            ActionManager.OnResetGameSceneUi += ResetGameSceneUI;
            ActionManager.ToggleLevelUI += ToggleLevelUI;
            ActionManager.OnUpdatePlayerScore += UpdateScoreTxt;
            


            ActionManager.OnResetGameSceneUi?.Invoke();
#if UNITY_EDITOR
            GameManager.currentLevelState = LevelState.Start;
            ActionManager.OnStartCounter?.Invoke();
#endif

            SetHighestScore((int)ActionManager.GetHighestScore?.Invoke());

        }
        private void OnDisable()
        {
            ActionManager.OnStartCounter -= StartCounter;
            ActionManager.OnResetGameSceneUi -= ResetGameSceneUI;
            ActionManager.ToggleLevelUI -= ToggleLevelUI;
            ActionManager.OnUpdatePlayerScore -= UpdateScoreTxt;
            

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
            ResetCounterTxt();
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
            scoreText.text = "0";
        }
        private void ResetCounterTxt()
        {
            countDownTxt.text = "";
            countDownTxt.fontSize = startFontSize;
        }
        private void UpdateScoreTxt(int value)
        {
            scoreText.text = value.ToString();  
        }
       
        private void ToggleLevelUI(bool isActive)
        {
            LevelUIHolder.SetActive(isActive);  
        }

        private void SetHighestScore(int value)
        {
            if(value > 0)
            {
                HighestScoreHolder.SetActive(true);
            }
            else
            {
                HighestScoreHolder.SetActive(false);    
            }
        }
        #endregion Custom Methods

    }
}

