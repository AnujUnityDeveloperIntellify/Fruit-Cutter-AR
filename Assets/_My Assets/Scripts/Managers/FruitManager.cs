using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class FruitManager : MonoBehaviour
    {
        [SerializeField] private Transform arCamera;
        [SerializeField] private float spawnDistance;
        [SerializeField] private int fruitCount;
        private Vector3 reSpawnPos;
        [SerializeField] private GameObject currentFruit;

        private void OnEnable()
        {
            ActionManager.OnRespwanFruit += RespwanFruit;
            ActionManager.OnStartFruitSpwan += StartFruitSpwan;
            reSpawnPos = arCamera.position + arCamera.forward * spawnDistance;
        }

        private void OnDisable()
        {
            ActionManager.OnRespwanFruit -= RespwanFruit;
            ActionManager.OnStartFruitSpwan -= StartFruitSpwan;
        }
        private void RespwanFruit()
        {
            currentFruit.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            currentFruit.transform.position = reSpawnPos;
            currentFruit.SetActive(true);

        }
        private void StartFruitSpwan()
        {
            RespwanFruit();

        }
    }
}

