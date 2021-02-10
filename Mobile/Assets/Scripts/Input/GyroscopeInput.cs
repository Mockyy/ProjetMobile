using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeInput : MonoBehaviour
{
    private Rigidbody rb;

    private float speed = 5f;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS

        direction = new Vector3(Input.acceleration.x, 0, Input.acceleration.y);
#endif

#if UNITY_EDITOR

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0, vertical);
#endif

        rb.AddTorque(direction * speed);
        //rb.AddForce(new Vector3(horizontal * speed, 0, vertical * speed), ForceMode.Acceleration);
    }
}
