using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class GroundController : MonoBehaviour
    {
        [SerializeField] private LayerMask fruitLayer;
        private Rigidbody rbFruit;
        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;

            if (other.layer == fruitLayer)
            {
                Debug.LogError("Deactivate " + other.name);

                Rigidbody rbFruit = other.GetComponent<Rigidbody>();
                if (rbFruit != null)
                {
                    ActionManager.OnDeactivateFruit?.Invoke(rbFruit);
                }
            }
        }
    }
}

