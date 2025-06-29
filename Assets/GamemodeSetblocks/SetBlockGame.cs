using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public Transform parent;
    private bool gameEnded = false;
    public float endDelay;
    public ScoreManager ScoreManager;

    void Start()
    {
        ScoreManager.UpdateScore = false;
    }
    void Update()
    {
        
    }

    public async void ToggleGamemode()
    {
        if (!gameRunning && !gameEnded)
        {
            gameRunning = true;
            int numBlocks = blockArray.Length;
            int rand;
            MaterialScriptableObject[] mso = blockArray[0].GetComponent<CycleMaterial>().mso;
            foreach (Transform child in spawnPoints.transform)
            {
                rand = Random.Range(0, numBlocks);
                if (rand == numBlocks)
                    continue;
                GameObject toSpawn = blockArray[rand];
                GameObject spawnable = Instantiate(toSpawn, child.position, Quaternion.identity, parent);
                int randMat = Random.Range(0, mso.Length);
                await Task.Delay(200);
                spawnable.GetComponent<CycleMaterial>().currentIndex = randMat;
                ApplyMaterialProperties(mso[randMat], spawnable);
                await Task.Delay(100);
            }

        }
        else {
            gameRunning = false;
            gameEnded = true;
            StartCoroutine(Delay());
            ScoreManager.GetCurrentScore();
        }
    }


    private void DestroyLeftovers()
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        gameEnded = false;
    }

    private void ApplyMaterialProperties(MaterialScriptableObject newMaterial, GameObject spawnable)
    {
        float baseMass = spawnable.GetComponent<Rigidbody>().mass;
        spawnable.GetComponent<Rigidbody>().mass = baseMass * newMaterial.weightModifier;
        spawnable.GetComponent<Collider>().material = newMaterial.physicsMaterial;
        spawnable.GetComponent<MeshRenderer>().material = newMaterial.material;
        spawnable.GetComponent<AudioSource>().clip = newMaterial.impactSound;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(endDelay);
        DestroyLeftovers();
    }
}

