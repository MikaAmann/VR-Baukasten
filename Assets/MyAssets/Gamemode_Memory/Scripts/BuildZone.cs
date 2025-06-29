using System.Collections.Generic;
using UnityEngine;

public class BuildZone : MonoBehaviour
{
    public List<GameObject> placedBlocksInZone = new();
    public Transform Parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildBlocks"))
        {
            placedBlocksInZone.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuildBlocks"))
        {
            placedBlocksInZone.Remove(other.gameObject);
        }
    }
        
}
