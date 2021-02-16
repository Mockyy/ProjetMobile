using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    private GameObject touchedObject;

    void Update()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // la première touche

            if (touch.phase == TouchPhase.Began)
            {
                Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(touchRay, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Movable_Obstacle"))
                    {
                        touchedObject = hit.transform.gameObject;
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                touchedObject.GetComponent<MeshRenderer>().material.color = Color.grey;
                touchedObject = null;
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved && touchedObject != null)
            {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(
                    new Vector3(touch.position.x, touch.position.y, 25));

                touchedObject.GetComponent<MeshRenderer>().material.color = Color.red;

                touchedObject.transform.position = touchedPos;
            }
        }
#endif

        if (Input.GetMouseButtonDown(0))
        {
            Ray clicRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(clicRay, out RaycastHit hit))
            {
                if (hit.transform.tag == "Movable_Obstacle")
                {
                    touchedObject = hit.transform.gameObject;
                    touchedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0) && touchedObject != null)
        {
            touchedObject.GetComponent<MeshRenderer>().material.color = Color.grey;
            touchedObject = null;
        }
    }

    private void OnMouseDrag()
    {
        if (touchedObject != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = touchedObject.transform.position - mouseWorldPos;

            touchedObject.transform.position = mouseWorldPos + offset;
        }
    }
}