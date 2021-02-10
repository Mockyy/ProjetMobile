using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    private GameObject playerObject;
    private GameObject camObject;

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
        camObject = Instantiate(cam, transform.position, transform.rotation) as GameObject;
        camObject.GetComponent<CameraFollow>().SetTarget(playerObject.transform);
    }

    private void Despawn()
    {
        Destroy(playerObject);
        Destroy(camObject);
    }
}