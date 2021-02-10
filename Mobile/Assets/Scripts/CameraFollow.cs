using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 distance;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target.transform);
        transform.position = target.transform.position + distance;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
