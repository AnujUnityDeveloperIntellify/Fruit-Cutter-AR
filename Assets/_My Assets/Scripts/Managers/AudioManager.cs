using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private AudioSource fruitCutAudioSource;
        #endregion Variables

        #region Unity Methods

        void Start()
        {

        }

        private void OnEnable()
        {
            ActionManager.OnPlayFruitCutAudio += PlayFruitCutAudio;
        }
        private void OnDisable()
        {
            ActionManager.OnPlayFruitCutAudio -= PlayFruitCutAudio;
        }
        #endregion Unity Methods

        #region Custom Methods
        private void PlayFruitCutAudio()
        {
            fruitCutAudioSource.time = 0;   
            fruitCutAudioSource.Play();
        }
        #endregion Custom Methods
    }
}

