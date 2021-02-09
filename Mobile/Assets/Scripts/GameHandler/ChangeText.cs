using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.StartMoving += Move;
        GameManager.StartBuilding += Build;
    }

    private void OnDisable()
    {
        GameManager.StartMoving -= Move;
        GameManager.StartBuilding -= Build;
    }

    private void Move()
    {
        GetComponent<TextMeshProUGUI>().text = "Build";
    }

    private void Build()
    {
        GetComponent<TextMeshProUGUI>().text = "Play";
    }
}
