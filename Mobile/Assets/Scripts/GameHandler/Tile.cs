using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool isClosest;

    private void OnEnable()
    {
        GameManager.StartBuilding += Show;
        GameManager.StartMoving += Hide;
    }

    private void OnDisable()
    {
        GameManager.StartBuilding -= Show;
        GameManager.StartMoving -= Hide;
    }

    private void Update()
    {
        if (isClosest)
        {
            GetComponent<MeshRenderer>().materials[1].color = Color.red;
        }
        else
        {
            GetComponent<MeshRenderer>().materials[1].color = Color.black;
        }
    }

    public void Hide()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Show()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void SetClosest(bool b)
    {
        isClosest = b;
    }

    public bool GetClosest()
    {
        return isClosest;
    }
}
