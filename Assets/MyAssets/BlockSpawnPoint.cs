using UnityEngine;
using System.Collections;

public class BlockSpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    private GameObject currentBlock;
    private bool isRespawning = false;

    public void Start()
    {
        SpawnBlock();
    }

    public void SpawnBlock()
    {
        currentBlock = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

        currentBlock.AddComponent<BlockSpawnStatus>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != currentBlock) return;

        BlockSpawnStatus status = other.GetComponent<BlockSpawnStatus>();
        if (status == null) return;

        // Nur beim allerersten Verlassen reagieren
        if (status.hasLeftSpawn) return;

        status.hasLeftSpawn = true;

        if (!isRespawning)
            StartCoroutine(DelayedRespawn());
    }

    public void NotifyBlockRemoved()
    {
        if (!isRespawning)
            StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(2f);

        // Sicherheitscheck: ist Position immer noch leer?
        if (currentBlock == null)
            SpawnBlock();

        isRespawning = false;
    }
}
