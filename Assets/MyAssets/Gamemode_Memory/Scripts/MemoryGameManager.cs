using System;
using System.Collections;
using UnityEngine;
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
    
    private GameObject currentReferenceTower;
    private GameObject currentHologram;
    private Boolean gameStarted;
   
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
