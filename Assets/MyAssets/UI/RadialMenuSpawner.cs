using UnityEngine;

public class RadialSelectionSpawner : MonoBehaviour
{
    public RadialSelectionComponent radialSelectionComponent;
    public GameObject[] partPrefabs; // Match this with numRadialPart
    public Transform handTransform;
    public GameObject cleanupManager;

    private void OnEnable()
    {
        radialSelectionComponent.OnPartSelected.AddListener(SpawnSelectedPart);
    }

    private void OnDisable()
    {
        radialSelectionComponent.OnPartSelected.RemoveListener(SpawnSelectedPart);
    }

    void SpawnSelectedPart(int index)
    {
        if (index < 0 || index >= partPrefabs.Length) return;

      

        Vector3 spawnPosition = handTransform.position + handTransform.forward * 1.2f;
        Quaternion spawnRotation = Quaternion.identity;

        GameObject spawned = Instantiate(partPrefabs[index], spawnPosition, spawnRotation);
        cleanupManager.GetComponent<Cleanup>().spawnedBlocks.Add(spawned);

    }

}
