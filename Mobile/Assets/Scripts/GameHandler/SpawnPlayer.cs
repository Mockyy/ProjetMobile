using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    private GameObject playerObject;

    private void OnEnable()
    {
        GameManager.StartMoving += Spawn;
        GameManager.StartBuilding += Despawn;
    }

    private void OnDisable()
    {
        GameManager.StartMoving -= Spawn;
        GameManager.StartBuilding -= Despawn;
    }

    private void Spawn()
    {
        playerObject = Instantiate(player, transform.position, transform.rotation) as GameObject;
    }

    private void Despawn()
    {
        Destroy(playerObject);
    }
}