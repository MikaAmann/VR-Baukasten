using UnityEngine;

public class HammerHit : MonoBehaviour
{
    private bool hasHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHit) { 
            CycleForm form = other.GetComponent<CycleForm>();
            if (form != null)
            {
                form.CycleToNextForm();
            }
            hasHit = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        hasHit = false;
    }
}
