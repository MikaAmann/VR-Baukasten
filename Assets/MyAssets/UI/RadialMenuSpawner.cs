using UnityEngine;

public class RadialSelectionSpawner : MonoBehaviour
{
    public RadialSelectionComponent radialSelectionComponent;
    public GameObject[] partPrefabs; // Match this with numRadialPart
    public Transform handTransform;

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

        foreach (Transform child in handTransform)
        {
            Destroy(child.gameObject);
        }

        Vector3 spawnPosition = handTransform.position + handTransform.forward * 1.2f;
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(partPrefabs[index], spawnPosition, spawnRotation);
    }

}
