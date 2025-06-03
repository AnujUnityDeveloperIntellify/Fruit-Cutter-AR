using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class AudioManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private AudioSource fruitCutAudioSource;
        [SerializeField] private AudioSource grenadeBlastAudioSource;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            SetAudioForDevice(fruitCutAudioSource);
            SetAudioForDevice(grenadeBlastAudioSource);

        }
        void Start()
        {

        }

        private void OnEnable()
        {
            ActionManager.OnPlayFruitCutAudio += PlayFruitCutAudio;
            ActionManager.OnPlayGrenadeBlastAudio += PlayGrenadeBlastAudio;
        }
        private void OnDisable()
        {
            ActionManager.OnPlayFruitCutAudio -= PlayFruitCutAudio;
            ActionManager.OnPlayGrenadeBlastAudio -= PlayGrenadeBlastAudio;
        }
        #endregion Unity Methods

        #region Custom Methods
        private void PlayFruitCutAudio()
        {
            fruitCutAudioSource.time = 0;   
            fruitCutAudioSource.Play();
        }
        private void PlayGrenadeBlastAudio()
        {
            grenadeBlastAudioSource.time = 0;
            grenadeBlastAudioSource.Play();
        }    

        private void SetAudioForDevice(AudioSource audioSource)
        {
#if UNITY_EDITOR
            audioSource.volume = 0f;
#elif UNITY_ANDROID

#endif
        }
#endregion Custom Methods
    }
}

