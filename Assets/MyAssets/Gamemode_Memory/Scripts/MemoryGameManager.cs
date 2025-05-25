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
    public Canvas canvas;
    
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
        try
        {
            GameObject[] memoryBlocks = GameObject.FindGameObjectsWithTag("Memory");
            List<GameObject> placedBlocksOriginal = buildZone.placedBlocksInZone;
            List<GameObject> placedBlocks = new List<GameObject>(placedBlocksOriginal);
        
            Debug.Log("Memory: "+ memoryBlocks.Length + ", Placed: " + placedBlocks.Count );

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
                    //Debug.Log("Type: " + (memoryType.type != placedType.type));
                    if (memoryType.type != placedType.type)
                        continue;

                    float dist = Vector3.Distance(memoryBlock.transform.position, placedBlock.transform.position);
                    if (dist < matchTolerance && dist < bestDistance)
                    {
                        bestDistance = dist;
                        bestMatch = placedBlock;
                        Debug.Log("Dist: " + dist + "at Type: " + placedType.type);
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
            Debug.Log("Extra: " + extraBlocks);
            Debug.Log("Matches: " + correctMatches);

            float totalScore = correctMatches * 1.0f
                               + extraBlocks * -0.5f;

            float maxScore = memoryBlocks.Length * 1.0f;
            float finalScorePercent = Mathf.Clamp01(totalScore / maxScore);
            
            canvas.GetComponent<ScoreUI>().UpdateScoreUI(finalScorePercent);

            Debug.Log("Score: " + finalScorePercent);

            EndGame();
        }
        catch (Exception ex)
        {
            Debug.LogError("Fehler in Evaluate: " + ex.Message);
        }

        
    }

    public void EndGame()
    {
        if (currentReferenceTower != null)
            Destroy(currentReferenceTower);
    
        if (currentHologram != null)
            Destroy(currentHologram);

        gameStarted = false;
        
        foreach (GameObject block in buildZone.placedBlocksInZone)
        {
            Destroy(block,.2f);
        }

        buildZone.placedBlocksInZone.Clear();
    }
    
    private IEnumerator HologramHeldTimer()
    {
        yield return new WaitForSeconds(hologramTimer);
        Destroy(currentHologram);
    }
}
