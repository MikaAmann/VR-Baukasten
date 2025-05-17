using UnityEngine;

public class CycleForm : MonoBehaviour
{
    public GameObject[] formCyclePrefabs;
    public int currentIndex = 0;

    public void CycleToNextForm()
    {
        int nextIndex = (currentIndex + 1) % formCyclePrefabs.Length;
        GameObject nextForm = Instantiate(formCyclePrefabs[nextIndex], transform.position, transform.rotation);
        CycleForm nextCycle = nextForm.GetComponent<CycleForm>();
        if (nextCycle != null)
            nextCycle.currentIndex = nextIndex;

        Destroy(gameObject);
    }
}
