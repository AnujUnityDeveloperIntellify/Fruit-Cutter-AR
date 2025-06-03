using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace FruitCutter
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private Transform arCamera;
        [SerializeField] private float verticalOffset;
        [SerializeField] private float horizontalRange;
        private float spawnDistance;

        [Header("Launch Force Settings")]
         private float minXForce = -0.2f;
         private float maxXForce = 0.2f;
         private float minYForce = 2f;
         private float maxYForce = 2.8f;

        [SerializeField] private int fruitCount;
        [SerializeField] private GameObject currentFruit;

        [SerializeField] private List<Rigidbody> AllFruitLists;
        [SerializeField] private List<Rigidbody> activeFruits = new();
        private Coroutine spwanFruit_Coroutine;
        [SerializeField] private Vector3 initialFruitPos;
        private void OnEnable()
        {
            spawnDistance = GameManager.objectSpwanDistance;
            ActionManager.OnRespawnFruit += RespawnFruit;
            ActionManager.OnStartFruitSpwan += StartFruitSpwan;
            ActionManager.OnResetAllFruits += ResetAllFruits;
            ActionManager.OnStopFruitSpwan += StopFruitSpwan;
            ActionManager.OnDeactivateFruit += DeactivateFruit;
            ActionManager.OnResetBomb += ResetBomb;
        }

        private void OnDisable()
        {
            ActionManager.OnRespawnFruit -= RespawnFruit;
            ActionManager.OnStartFruitSpwan -= StartFruitSpwan;
            ActionManager.OnResetAllFruits -= ResetAllFruits;
            ActionManager.OnStopFruitSpwan -= StopFruitSpwan;
            ActionManager.OnDeactivateFruit -= DeactivateFruit;
            ActionManager.OnResetBomb -= ResetBomb;

        }
        private void Start()
        {
            
        }
        private void RespawnFruit(Rigidbody currentFruit)
        {
            Vector3 spawnPos = arCamera.position + arCamera.forward * spawnDistance;
            spawnPos.y += verticalOffset;
            spawnPos += arCamera.right * Random.Range(-horizontalRange, horizontalRange);

            currentFruit.velocity = Vector3.zero;
            currentFruit.angularVelocity = Vector3.zero;
            currentFruit.transform.position = spawnPos;
            currentFruit.transform.rotation = Quaternion.identity;
            currentFruit.gameObject.SetActive(true);

            float xForce = Random.Range(minXForce, maxXForce);
           // float xForce = 0f;
            float yForce = Random.Range(minYForce, maxYForce);
            //float yForce = maxYForce;
            Vector3 bounceForce = new Vector3(xForce, yForce, 0f);
            currentFruit.AddForce(bounceForce, ForceMode.Impulse);

            if (!activeFruits.Contains(currentFruit))
                activeFruits.Add(currentFruit);
#if UNITY_EDITOR
            //EditorApplication.isPaused = true;
#elif UNITY_ANDROID

#endif
        }
        private void StartFruitSpwan()
        {
            ActionManager.OnResetAllFruits?.Invoke();
            spwanFruit_Coroutine = StartCoroutine(SpawningFruit());
        }
        private void StopFruitSpwan()
        {
            StopCoroutine(spwanFruit_Coroutine);
            ActionManager.OnResetAllFruits?.Invoke();   
        }
        private IEnumerator SpawningFruit()
        {
            yield return new WaitForSeconds(1.0f);
            while (GameManager.currentLevelState == LevelState.Start)
            {
                List<Rigidbody> inactiveFruits = GetInactiveFruits(AllFruitLists);
                if (inactiveFruits.Count > 0)
                {
                    Rigidbody selected = inactiveFruits[Random.Range(0, inactiveFruits.Count)];
                    ActionManager.OnRespawnFruit?.Invoke(selected);
                }
               // yield break;
              yield return new WaitForSeconds(1.0f); 
            }
        }
        private List<Rigidbody> GetInactiveFruits(List<Rigidbody> fruitLists)
        {
            List<Rigidbody> inActiveFruits = new List<Rigidbody>();
            inActiveFruits.Clear();
            foreach (Rigidbody rb in fruitLists) 
            {
                if(!rb.gameObject.activeInHierarchy)
                {
                    inActiveFruits.Add(rb); 
                }
            }
            return inActiveFruits;
        }
        private void ResetAllFruits()
        {
            foreach (Rigidbody rb in AllFruitLists)
            {
                ResetFruit(rb);
            }
            activeFruits.Clear();
        }
        private void ResetFruit(Rigidbody fruit)
        {
           // Debug.LogError("Reset Fruit " + fruit.name);
            fruit.velocity = Vector3.zero;
            fruit.angularVelocity = Vector3.zero;
            fruit.transform.rotation = Quaternion.identity;
            fruit.gameObject.SetActive(false);
            fruit.transform.position = new Vector3(initialFruitPos.x, initialFruitPos.y,spawnDistance);
        }
        private void DeactivateFruit(Rigidbody hitFruit)
        {
            if (activeFruits.Contains(hitFruit))
            {
                ResetFruit(hitFruit);
                activeFruits.Remove(hitFruit);
            }
        }
        private void ResetBomb(Rigidbody hitBomb)
        {
            if(activeFruits.Contains(hitBomb))
            {
                ResetFruit(hitBomb);
                activeFruits.Remove(hitBomb);
            }
        }
    }
}

