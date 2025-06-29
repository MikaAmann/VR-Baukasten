using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    public List<GameObject> spawnedBlocks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void killSwitch()
    {
        foreach(GameObject block in spawnedBlocks)
        {
            spawnedBlocks.Remove(block);
            Destroy(block);
        }
    }
}
