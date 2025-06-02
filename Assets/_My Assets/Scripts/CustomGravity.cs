using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    [SerializeField] private float gravityScale = 0.5f; 
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * gravityScale - Physics.gravity, ForceMode.Acceleration);
    }
}
