using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class GameDataManager : MonoBehaviour
    {
        private int playerScore;
        private int playerLives;

        private void OnEnable()
        {
            ActionManager.OnEarnScore += EarnScore;
            ActionManager.GetPlayerCurrentScore += GetPlayerScore;
            ActionManager.OnResetPlayerData += ResetPlayerData;
            ActionManager.GetPlayerCurrentlives += GetPlayerLives;
        }
        private void OnDisable()
        {
            ActionManager.OnEarnScore -= EarnScore;
            ActionManager.GetPlayerCurrentScore -= GetPlayerScore;
            ActionManager.OnResetPlayerData -= ResetPlayerData;
            ActionManager.GetPlayerCurrentlives -= GetPlayerLives;  
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
    }
}

