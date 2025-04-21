using UnityEngine;

public class SnapAnchor : MonoBehaviour
{
    private SnapManager manager;

    private void Awake()
    {
        // Holt sich den Manager vom Elternobjekt (z. B. dem Cube selbst)
        manager = GetComponentInParent<SnapManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();

        if (otherAnchor != null && otherAnchor.transform.root != transform.root)
        {
            // Berechne Position, an die gesnapped werden sollte
            Vector3 localOffset = transform.localPosition;
            Vector3 snapPosition = other.transform.position - (transform.parent.rotation * localOffset);

            //Vector3 offset = transform.position - transform.parent.position;
            //Vector3 snapPosition = otherAnchor.transform.position - offset;

            // Gib dem Manager die Zielposition
            manager?.SetSnapTarget(snapPosition, otherAnchor.transform.rotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();

        if (otherAnchor != null && otherAnchor.transform.root != transform.root)
        {
            manager?.ClearSnapTarget();
        }
    }
}