using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FruitCutter
{
    public class ShootFruitController : MonoBehaviour
    {
        private Camera arCamera;
        [SerializeField] private LayerMask fruitLayer;
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
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit,40f,fruitLayer))
                {
                    hit.collider.gameObject.SetActive(false);
                    StartCoroutine(OnCurrentFruitHit());
                }
            }
        }
        private IEnumerator OnCurrentFruitHit()
        {
            yield return new WaitForSeconds(0.8f);
            ActionManager.OnRespwanFruit?.Invoke();
        }

    }
}

