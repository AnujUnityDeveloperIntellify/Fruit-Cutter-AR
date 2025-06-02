using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitCutter
{
    public class ShootFruitController : MonoBehaviour
    {
        private Camera arCamera;
        [SerializeField] private LayerMask fruitLayer;
        private Rigidbody rbFruit;
        void Start()
        {
            arCamera= GetComponent<Camera>();
        }
        void Update()
        {
            if(GameManager.currentLevelState == LevelState.Start)
            {
                ShootRaycast();
            }
        }

        private void ShootRaycast()
        {
         
#if UNITY_ANDROID
            if (Input.touchCount == 1 && (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase ==TouchPhase.Moved))
            {
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 40f, fruitLayer))
                {
                    OnCurrentFruitHit(hit.collider.gameObject);
                }
            }
#endif

        }
        private void OnCurrentFruitHit(GameObject fruit)
        {
            ActionManager.OnPlayFruitCutAudio?.Invoke();
            rbFruit = fruit.GetComponent<Rigidbody>();
            ActionManager.OnDeactivateFruit?.Invoke(rbFruit);
            ActionManager.OnEarnScore?.Invoke();

        }

    }
}

