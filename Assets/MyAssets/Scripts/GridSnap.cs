using UnityEngine;

public class GridSnap : MonoBehaviour
{
    public float gridSize = 0.1f;
    public bool snapOnRelease = true;

    private bool isInBuildZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuildZone"))
            isInBuildZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BuildZone"))
            isInBuildZone = false;
    }

    public void SnapToGrid()
    {
        if (!isInBuildZone) return;

        Vector3 pos = transform.position;

        float x = Mathf.Round(pos.x / gridSize) * gridSize;
        float y = Mathf.Round(pos.y / gridSize) * gridSize;
        float z = Mathf.Round(pos.z / gridSize) * gridSize;

        transform.position = new Vector3(x, y, z);
    }

}
