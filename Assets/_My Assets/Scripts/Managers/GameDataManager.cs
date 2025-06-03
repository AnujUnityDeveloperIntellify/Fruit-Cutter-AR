using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class GameDataManager : MonoBehaviour
    {
        private int playerScore;
        private int playerLives;
        private int currentHigestScore;
        private const string KEY_PLAYER_HIGEST_SCORE = "PlayerHighScore";
        private void OnEnable()
        {
            LoadCurrentHigestScore();
            ActionManager.OnSavedHighestScore += SaveHighestScore;
            ActionManager.OnEarnScore += EarnScore;
            ActionManager.GetPlayerCurrentScore += GetPlayerScore;
            ActionManager.OnResetPlayerData += ResetPlayerData;
            ActionManager.GetPlayerCurrentlives += GetPlayerLives;
            ActionManager.GetHighestScore += GetHighestScore;
        }
        private void OnDisable()
        {
            ActionManager.OnSavedHighestScore -= SaveHighestScore;
            ActionManager.OnEarnScore -= EarnScore;
            ActionManager.GetPlayerCurrentScore -= GetPlayerScore;
            ActionManager.OnResetPlayerData -= ResetPlayerData;
            ActionManager.GetPlayerCurrentlives -= GetPlayerLives;
            ActionManager.GetHighestScore -= GetHighestScore;

        }
        private int GetPlayerScore()
        {
            return playerScore; 
        }
        private int GetPlayerLives()
        {
            return playerLives;
        }
        private void ResetPlayerData()
        {
            playerScore = 0;
            playerLives = 3;
            ActionManager.OnUpdatePlayerScore?.Invoke(playerScore);
        }
        private void EarnScore()
        {
            playerScore++;
            ActionManager.OnUpdatePlayerScore?.Invoke(playerScore); 

        }
        private void LoadCurrentHigestScore()
        {
            if(PlayerPrefs.HasKey(KEY_PLAYER_HIGEST_SCORE))
            {
                currentHigestScore = PlayerPrefs.GetInt(KEY_PLAYER_HIGEST_SCORE);
            }
        }
        private void SaveHighestScore()
        {
            if(playerScore > currentHigestScore)
            {
                PlayerPrefs.SetInt(KEY_PLAYER_HIGEST_SCORE, playerScore); 
            }
        }
        private int GetHighestScore()
        {
            return currentHigestScore;
        }
    }
}

