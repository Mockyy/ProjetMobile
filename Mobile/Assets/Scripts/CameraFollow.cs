using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 relativePos;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.transform.position;
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}
