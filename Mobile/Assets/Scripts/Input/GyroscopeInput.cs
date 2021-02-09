using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeInput : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(horizontal, 0, vertical), ForceMode.Force);
    }
}
