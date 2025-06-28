using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = UnityEngine.Random;
public class SetBlockGame : MonoBehaviour
{


    [Header("Setup")]
    public GameObject[] blockArray;
    public BuildZone buildZone;
    public GameObject[] presets;
    private bool gameRunning;
    public GameObject spawnPoints;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleGamemode()
    {
        if (!gameRunning)
        {
            gameRunning = true;
            int numBlocks = blockArray.Length;
            int rand;
            foreach (Transform child in spawnPoints.transform)
            {
                rand = Random.Range(0, numBlocks);
                if (rand == numBlocks)
                    continue;
                Instantiate(blockArray[rand], child.position, Quaternion.identity);
            }

        }
        else {

        }
    }
}
