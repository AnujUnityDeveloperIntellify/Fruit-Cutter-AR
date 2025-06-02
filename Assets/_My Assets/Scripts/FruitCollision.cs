using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FruitCutter
{
    public class FruitCollision : MonoBehaviour
    {
        private float rotationForce = 200;
        private Rigidbody rbFruit;
        private void Awake()
        {
            rbFruit = GetComponent<Rigidbody>();    
        }
        private void OnCollisionEnter(Collision collision)
        {
            GameObject other = collision.gameObject;
            //Debug.LogError("Collision Called ");
            if (other.layer == LayerMask.NameToLayer("Ground"))
            {
              //  Debug.LogError("Deactivate " + other.name);
                if (rbFruit != null)
                {
                    ActionManager.OnDeactivateFruit?.Invoke(rbFruit);
                }
            }
        }
        private void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationForce);
        }
    }
}

