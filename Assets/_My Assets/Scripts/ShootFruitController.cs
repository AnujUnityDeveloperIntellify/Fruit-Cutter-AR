using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;



namespace FruitCutter
{
    public class ShootFruitController : MonoBehaviour
    {
        private Camera arCamera;
        [SerializeField] private LayerMask fruitLayer;
        [SerializeField] private LayerMask bombLayer;
        private BombController bombController;
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
            if(Input.touchCount > 0)
            {
                if ((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved))
                {
                    Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 40f, fruitLayer ))
                    {
                        OnCurrentFruitHit(hit.collider.gameObject);
                    }
                    else if (Physics.Raycast(ray, out hit, 40f, bombLayer))
                    {
                        bombController = hit.collider.gameObject.GetComponent<BombController>();
                        if (bombController != null)
                        {
                            OnCurrentBombHit(bombController);
                        }
                    }
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
        private void OnCurrentBombHit(BombController bombController)
        {
            bombController.OnBombBlast();
        }

    }
}
// if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))


