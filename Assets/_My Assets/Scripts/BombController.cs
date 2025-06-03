using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class BombController : MonoBehaviour
    {
        #region Variables

        [SerializeField] MeshRenderer bombMR;
        [SerializeField] ParticleSystem bombPS;
        private float rotationForce = 200;
        private Rigidbody rbBomb;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            rbBomb = GetComponent<Rigidbody>(); 
        }
        void Start()
        {

        }

        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            OnResetBomb();
        }
        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            if (other.layer == LayerMask.NameToLayer("Ground"))
            {
                //  Debug.LogError("Deactivate " + other.name);
                if (rbBomb != null)
                {
                    ActionManager.OnResetBomb?.Invoke(rbBomb);
                    OnResetBomb();
                }
            }
        }
        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationForce);
        }
        #endregion Unity Methods

        #region Custom Methods
        internal void OnBombBlast()
        {
            ActionManager.OnStopFruitSpwan?.Invoke();
            ActionManager.OnPlayGrenadeBlastAudio?.Invoke();
           // Debug.LogError("OnBomb Blast");
            bombMR.enabled = false;
            bombPS.gameObject.SetActive(true);
            bombPS.Play();
            Invoke("OverLevel", 0.4f);
        }
        private void OverLevel()
        {
            ActionManager.OnGameOver?.Invoke();
        }
        private void OnResetBomb()
        {
            bombMR.enabled = true;
            bombPS.gameObject.SetActive(false);
        }
        #endregion Custom Methods
    }

}
