using System.Collections.Generic;
using UnityEngine;

public class BuildZoneTrigger : MonoBehaviour
{
    private HashSet<ScoringBlock> blocksInZone = new HashSet<ScoringBlock>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            ScoringBlock block = other.GetComponent<ScoringBlock>();
            if (block != null)
            {
                blocksInZone.Add(block);
                Debug.Log("Block hinzugefügt: " + other.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            ScoringBlock block = other.GetComponent<ScoringBlock>();
            if (block != null && blocksInZone.Contains(block))
            {
                blocksInZone.Remove(block);
                Debug.Log("Block entfernt: " + other.name);
            }
        }
    }

    public List<ScoringBlock> GetBlocksInZone()
    {
        return new List<ScoringBlock>(blocksInZone);
    }
}
