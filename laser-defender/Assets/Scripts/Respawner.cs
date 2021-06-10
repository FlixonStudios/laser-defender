using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    [SerializeField] Vector3 respawnPoint = new Vector3(0,-6,-1);

    public GameObject GetPlayerObject()
    {
        return playerObject;
    }
    public Vector3 GetRespawnPoint()
    {
        return respawnPoint;
    }

}
