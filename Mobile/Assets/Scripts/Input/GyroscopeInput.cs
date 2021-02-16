using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeInput : MonoBehaviour
{
    private float MAX_SPEED = 10f;

    private Rigidbody rb;

    private float speed = 10f;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private Transform cameraPivot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IOS

        horizontal = Input.acceleration.x;
        vertical = Input.acceleration.y;

#endif

#if UNITY_EDITOR

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

#endif

        direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();

        direction = Camera.main.transform.TransformDirection(direction);

    }

    public void FixedUpdate()
    {
        rb.AddForce(direction * speed, ForceMode.Acceleration);
        Mathf.Clamp(rb.velocity.x, 0f, MAX_SPEED);
        Mathf.Clamp(rb.velocity.z, 0f, MAX_SPEED);
        Debug.DrawRay(transform.position + Vector3.up, direction, Color.red);
    }
    

    public void SetCameraPivot(Transform transform)
    {
        cameraPivot = transform;
    }

    public void RotateCamera()
    {
        Quaternion rota;
        if (direction.magnitude != 0f)
        {
            rota = Quaternion.LookRotation(direction, Vector3.up);
            cameraPivot.rotation = Quaternion.Slerp(cameraPivot.rotation, rota, 10 * Time.deltaTime);
        }
    }
}
