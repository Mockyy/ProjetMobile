using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    private GameObject grabbedObj = null;
    private GameObject[] tiles;
    private float distanceMax = Mathf.Infinity;
    private float dist;

    private Plane objPlane;
    private Vector3 touchOffset;

    private void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    void Update()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // la première touche

            if (touch.phase == TouchPhase.Began)
            {
                //Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
                Ray touchRay = GenerateTouchRay(touch);

                if (Physics.Raycast(touchRay, out RaycastHit hit))
                {
                    if (hit.transform.CompareTag("Movable_Obstacle"))
                    {
                        grabbedObj = hit.transform.gameObject;
                        objPlane = new Plane(Camera.main.transform.right, grabbedObj.transform.position);

                        //Mouse offset
                        Ray mRay = Camera.main.ScreenPointToRay(touch.position);
                        float rayDistance;
                        objPlane.Raycast(mRay, out rayDistance);
                        touchOffset = grabbedObj.transform.position - mRay.GetPoint(rayDistance);
                    }
                }
            }

            if (touch.phase == TouchPhase.Ended && grabbedObj)
            {
                grabbedObj.GetComponent<MeshRenderer>().material.color = Color.grey;
                grabbedObj = null;
            }

            if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && grabbedObj)
            {
                //Vector3 touchedPos = Camera.main.ScreenToWorldPoint(
                //   new Vector3(touch.position.x, touch.position.y, 25));

                Ray mRay = Camera.main.ScreenPointToRay(touch.position);
                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                {
                    grabbedObj.transform.position = mRay.GetPoint(rayDistance) + touchOffset;
                }

                grabbedObj.GetComponent<MeshRenderer>().material.color = Color.red;

                //grabbedObj.transform.position = touchedPos;

                GetClosestTile();
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
                    grabbedObj = hit.transform.gameObject;
                    grabbedObj.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0) && grabbedObj)
        {
            grabbedObj.GetComponent<MeshRenderer>().material.color = Color.grey;
            grabbedObj = null;
        }
    }

    private void OnMouseDrag()
    {
        if (grabbedObj)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 offset = grabbedObj.transform.position - mouseWorldPos;

            grabbedObj.transform.position = mouseWorldPos + offset;
        }
    }

    private Ray GenerateTouchRay(Touch t)
    {
        Vector3 touchPosFar = new Vector3(t.position.x, t.position.y, Camera.main.farClipPlane);
        Vector3 touchPosNear = new Vector3(t.position.x, t.position.y, Camera.main.nearClipPlane);
        Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
        Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

        Ray mr = new Ray(touchPosN, touchPosF - touchPosN);

        return mr;
    }

    public Transform GetMovingObject()
    {
        return grabbedObj.transform;
    }

    public GameObject GetClosestTile()
    {
        GameObject closestTile = null;

        foreach (GameObject go in tiles)
        {
            if (Vector3.Distance(grabbedObj.transform.position, go.transform.position) <= dist)
            {
                closestTile.GetComponent<Tile>().SetClosest(false);
                dist = Vector3.Distance(grabbedObj.transform.position, go.transform.position);
                closestTile = go;
            }
        }

        dist = distanceMax;
        closestTile.GetComponent<Tile>().SetClosest(true);

        return closestTile;
    }
}