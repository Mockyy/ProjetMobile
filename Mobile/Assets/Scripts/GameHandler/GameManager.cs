using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static event Action StartMoving; 
    public static event Action StartBuilding;

    public enum PlayPhase {Moving, Building};
    public static PlayPhase phase;

    public static GameManager Instance
    {
        get
        {
            if (!_instance)
                Debug.Log("Game Manager is missing");

            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;

        phase = PlayPhase.Building;

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangePhase()
    {
        if (phase == PlayPhase.Building)
        {
            StartMoving?.Invoke();
            phase = PlayPhase.Moving;
        }
        else
        {
            StartBuilding?.Invoke();
            phase = PlayPhase.Building;
        }
    }
}