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
        private FruitController fruitController;
      
        void Start()
        {
            arCamera= GetComponent<Camera>();
        }
        private void OnEnable()
        {
            ActionManager.RotateSpriteTowardCamera += FaceSpriteTowardsCamera;
        }
        private void OnDisable()
        {
            ActionManager.RotateSpriteTowardCamera -= FaceSpriteTowardsCamera;
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
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 40f, fruitLayer ))
                    {
                        fruitController = hit.collider.gameObject.GetComponent<FruitController>();
                        if(fruitController != null)
                        {
                            OnCurrentFruitHit(fruitController);
                        }
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
        private void OnCurrentFruitHit(FruitController fruitController)
        {
            ActionManager.OnPlayFruitCutAudio?.Invoke();
            fruitController.OnFruitCut();
            ActionManager.OnEarnScore?.Invoke();

        }
        private void OnCurrentBombHit(BombController bombController)
        {
            bombController.OnBombBlast();
        }
        private void FaceSpriteTowardsCamera(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == null || arCamera == null) return;

            Transform spriteTransform = spriteRenderer.transform;

            Vector3 cameraPosition = arCamera.transform.position;
            Vector3 directionToCamera = cameraPosition - spriteTransform.position;
            directionToCamera.y = 0f;
            if (directionToCamera.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                spriteTransform.rotation = targetRotation;
            }
        }
    }
}
// if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))


