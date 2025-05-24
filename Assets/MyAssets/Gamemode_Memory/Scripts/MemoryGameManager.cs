using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Random = UnityEngine.Random;

public class MemoryGameManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject[] TowerList;
    public GameObject hologramPrefab;             
    public Transform buildOrigin;                 
    public Transform hologramSpawnPoint;
    public float hologramTimer;
    public BuildZone buildZone;
    
    private GameObject currentReferenceTower;
    private GameObject currentHologram;
    private Boolean gameStarted;
    
    //[Header("Evaluation")]

    public float matchTolerance = 0.3f;
   
    public void StartGame()
    {
        if(!gameStarted){
            gameStarted = true;

            int aPos = Random.Range(0, TowerList.Length);
            
            GameObject selectedTower = TowerList[aPos];
            
            currentReferenceTower = Instantiate(selectedTower, buildOrigin.position, Quaternion.identity);
            
            currentHologram = Instantiate(hologramPrefab, hologramSpawnPoint.position, Quaternion.identity);
            currentHologram.GetComponent<HologrammRenderer>()?.DisplayHoloTower(aPos);
            

            //Event-Listener: Sobald Hologramm aufgenommen wird → Timer starten
            XRGrabInteractable grab = currentHologram.GetComponent<XRGrabInteractable>();
            grab.selectEntered.AddListener(_ => StartCoroutine(HologramHeldTimer()));
            
        }
        
    }

    public void Evaluate()
    {
        GameObject[] memoryBlocks = GameObject.FindGameObjectsWithTag("Memory");
        List<GameObject> placedBlocks = buildZone.placedBlocksInZone;

        int correctMatches = 0;

        foreach (GameObject memoryBlock in memoryBlocks)
        {
            BlockType memoryType = memoryBlock.GetComponent<BlockType>();
            GameObject bestMatch = null;
            float bestDistance = Mathf.Infinity;

            foreach (GameObject placedBlock in placedBlocks)
            {
                Debug.Log(placedBlock);
                
                BlockType placedType = placedBlock.GetComponent<BlockType>();
                Debug.Log("Type: " + (memoryType != placedType));
                if(memoryType != placedType)
                    continue;
                
                float dist = Vector3.Distance(memoryBlock.transform.position, placedBlock.transform.position);
                Debug.Log(dist);
                if (dist < matchTolerance && dist < bestDistance)
                {
                    bestDistance = dist;
                    bestMatch = placedBlock;
                }
            }
            
            if (bestMatch != null)
            {
                correctMatches++;
                placedBlocks.Remove(bestMatch);
            }
        }
        int totalMemoryBlocks = memoryBlocks.Length;
        int extraBlocks = placedBlocks.Count;
        
        float totalScore = correctMatches * 1.0f
                           + (memoryBlocks.Length - correctMatches) * 0f
                           + extraBlocks * -0.5f;

        float maxScore = memoryBlocks.Length * 1.0f;
        float finalScorePercent = Mathf.Clamp01(totalScore / maxScore);
        
        Debug.Log("Score: "+finalScorePercent);
        
        EndGame();
        
        
    }

    public void EndGame()
    {
        if (currentReferenceTower != null)
        {
            Destroy(currentReferenceTower);
            if (currentHologram != null) {
                Destroy(currentHologram);
            }
            gameStarted = false;
        }
    }
    
    private IEnumerator HologramHeldTimer()
    {
        yield return new WaitForSeconds(hologramTimer);
        Destroy(currentHologram);
    }
    
    
}
