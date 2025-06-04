using FruitCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FruitCutter
{
    public class FruitController : MonoBehaviour
    {
        MeshRenderer mr;
        SphereCollider sphereCollider;
        [SerializeField] SpriteRenderer juiceSprite;
        private float rotationForce = 200;
        private Rigidbody rbFruit;
        private bool isRotation;
        private void Awake()
        {
            sphereCollider= GetComponent<SphereCollider>();
            rbFruit = GetComponent<Rigidbody>();
            mr = GetComponent<MeshRenderer>();
            juiceSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            
            
        }
        private void OnEnable()
        {
            isRotation = true;
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
            if (isRotation) 
            {
                transform.Rotate(Vector3.up * Time.deltaTime * rotationForce);
            }
           
        }
        internal void OnFruitCut()
        {
            sphereCollider.enabled = false;
            mr.enabled = false;
            isRotation = false;
            ActionManager.RotateSpriteTowardCamera?.Invoke(juiceSprite);
            juiceSprite.gameObject.SetActive(true);
            rbFruit.isKinematic = true;
#if UNITY_EDITOR
           // EditorApplication.isPaused = true;
#endif
            Invoke("OnReset", 0.4f);
        }
        private void OnReset()
        {
            sphereCollider.enabled = true;
            mr.enabled = true;
            juiceSprite.gameObject.SetActive(false);
            rbFruit.isKinematic = false;
            ActionManager.OnDeactivateFruit?.Invoke(rbFruit);
        }
    }
}

