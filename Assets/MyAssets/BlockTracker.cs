using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BlockTracker : MonoBehaviour
{
    public BlockSpawnPoint owningSpawnPoint;

    private void Start()
    {
        var grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null)
        {
            grab.selectExited.AddListener(OnGrabReleased);
        }
    }

    private void OnGrabReleased(SelectExitEventArgs args)
    {
        // Objekt wurde losgelassen � evtl. weggetragen?
        StartCoroutine(CheckIfReallyGone());
    }

    private IEnumerator CheckIfReallyGone()
    {
        yield return new WaitForSeconds(0.1f);

        // Ist das Objekt mehr als X Meter vom Spawn entfernt? Dann weg.
        float dist = Vector3.Distance(transform.position, owningSpawnPoint.transform.position);
        if (dist > 0.5f) // frei einstellbar
        {
            owningSpawnPoint.NotifyBlockRemoved();
            Destroy(this); // Tracker nicht mehr n�tig
        }
    }
}
