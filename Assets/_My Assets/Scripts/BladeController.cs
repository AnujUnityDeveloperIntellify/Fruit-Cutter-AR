using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class BladeController : MonoBehaviour
    {
        #region Variables
        private Rigidbody rb;
        [SerializeField] private SphereCollider bladeSC;
        [SerializeField] private TrailRenderer bladeTR;
        private bool isCutting;
        #endregion Variables

        #region Unity Methods

        private void Awake()
        {
            rb = GetComponent<Rigidbody>(); 
        }
        void Start()
        {

        }
        private void Update()
        {
            if(GameManager.currentLevelState == LevelState.Start)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    StartCutting();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    StopCutting();
                }

                BladeFollowMouse();
            }
        }

        #endregion Unity Methods

        #region Custom Methods
        private void StartCutting()
        {
            isCutting = true;
            bladeSC.enabled = true;
            bladeTR.enabled = true; 
        }
        private void StopCutting()
        {
            isCutting = false;
            bladeSC.enabled = false;
            bladeTR.enabled = false;
        }
        private void BladeFollowMouse()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = GameManager.objectSpwanDistance;
            rb.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        #endregion Custom Methods

    }
}

