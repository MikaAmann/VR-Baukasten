using UnityEngine;

public class Spawn_Object : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnpoint;

    public void SpawnObject()
    {
        Instantiate(objectToSpawn, spawnpoint);
    }
}
