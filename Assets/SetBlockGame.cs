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

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public async void ToggleGamemode()
    {
        if (!gameRunning)
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
                GameObject spawnable = blockArray[rand];
                Instantiate(spawnable, child.position, Quaternion.identity);
                int randMat = Random.Range(0, mso.Length);
                await Task.Delay(200);
                spawnable.GetComponent<CycleMaterial>().currentIndex = randMat;
                ApplyMaterialProperties(mso[randMat], spawnable);
                await Task.Delay(100);
            }

        }
        else {
            gameRunning = false;
            foreach (GameObject block in buildZone.placedBlocksInZone)
            {
                Destroy(block, .2f);
            }

            buildZone.placedBlocksInZone.Clear();
        }
    }


    public void ApplyMaterialProperties(MaterialScriptableObject newMaterial, GameObject spawnable)
    {
        float baseMass = spawnable.GetComponent<Rigidbody>().mass;
        spawnable.GetComponent<Rigidbody>().mass = baseMass * newMaterial.weightModifier;
        spawnable.GetComponent<Collider>().material = newMaterial.physicsMaterial;
        spawnable.GetComponent<MeshRenderer>().material = newMaterial.material;
        spawnable.GetComponent<AudioSource>().clip = newMaterial.impactSound;
    }
    IEnumerator MyDelayedMethod()
    {
        Debug.Log("Wait starts");
        yield return new WaitForSeconds(0.2f); // 2-second delay
        Debug.Log("Wait ends");
    }
}

