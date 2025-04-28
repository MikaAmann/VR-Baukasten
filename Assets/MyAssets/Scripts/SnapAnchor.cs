using UnityEngine;

public class SnapAnchor : MonoBehaviour
{
    private SnapManager manager;

    private void Awake()
    {
        manager = GetComponentInParent<SnapManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();
        

        if (otherAnchor != null && otherAnchor.transform.root != transform.root)
        {
            // Offset korrekt im Local-Space berechnen und in Welt-Space umwandeln
            //Vector3 localOffset = transform.localPosition;
            //Vector3 rotatedOffset = transform.parent.rotation * localOffset;
            //Vector3 currentAnchorPosition = transform.parent.position + rotatedOffset;
            //Vector3 offset = otherAnchor.transform.position - currentAnchorPosition;
            
            // Zielposition berechnen, sodass die eigene Hitbox auf der anderen sitzt
            //Vector3 snappedPosition = otherAnchor.transform.position + offset;
            
            //--
            //Vector3 offset = otherAnchor.transform.position - transform.position;
            //Vector3 snappedPosition = transform.parent.position + offset;
            //--
            Vector3 offset = transform.position - transform.parent.position;
            Vector3 snappedPosition = otherAnchor.transform.position - offset;
            
            
            // Gib dem Manager die Zielposition
            manager?.SetSnapTarget(snappedPosition, otherAnchor.transform.rotation); //otherAnchor.transform.rotation
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