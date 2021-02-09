using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCamera : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.StartMoving += Disable;
        GameManager.StartBuilding += Enable;
    }

    private void OnDisable()
    {
        GameManager.StartMoving -= Disable;
        GameManager.StartBuilding -= Enable;
    }

    private void Disable()
    {
        gameObject.GetComponent<Camera>().enabled = false;
        gameObject.GetComponent<AudioListener>().enabled = false;
        gameObject.GetComponent<TouchInput>().enabled = false;
    }

    private void Enable()
    {
        gameObject.GetComponent<Camera>().enabled = true;
        gameObject.GetComponent<AudioListener>().enabled = true;
        gameObject.GetComponent<TouchInput>().enabled = true;
    }
}
