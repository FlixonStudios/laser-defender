using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{        
    private void Awake()
    {
        var objectNames = new List<string>();
        var gameObjectList = new List<GameObject>();
 
        foreach (PersistThis gameState in FindObjectsOfType<PersistThis>())
        {
            // check if current gameState is already in the list
            if (objectNames.Contains(gameState.name))
            {
                Destroy(gameState.gameObject);
            }
            else
            {
                objectNames.Add(gameState.name);

                //DontDestroyOnLoad(gameState);
            }
        }
        for (int i = 0; i < objectNames.Count; i++)
        {
            Debug.Log(objectNames[i]);
        }
    }
}
