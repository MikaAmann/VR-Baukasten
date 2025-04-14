using UnityEngine;

public class SnapAnchor_Save : MonoBehaviour
{
    public bool isOccupied = false;

    private void OnTriggerEnter(Collider other)
    {
        SnapAnchor_Save otherAnchor = other.GetComponent<SnapAnchor_Save>();

        // Stelle sicher, dass beide gültige Ankerpunkte und nicht vom selben Objekt sind
        if (otherAnchor != null && !otherAnchor.isOccupied && !isOccupied &&
            otherAnchor.transform.root != transform.root)
        {
            SnapToObject(otherAnchor);
        }
    }

    private void SnapToObject(SnapAnchor_Save targetAnchor)
    {
        // Referenzen auf die Objekte
        Transform heldObject = transform.parent;
        Transform targetObject = targetAnchor.transform.parent;

        // Berechne Weltpositions-Offset
        Vector3 offset = transform.position - heldObject.position;

        // Setze Position so, dass der eigene Anker auf dem Zielanker liegt
        Vector3 newPosition = targetAnchor.transform.position - offset;
        heldObject.position = newPosition;

        // Setze Rotation auf die Rotation des Zielankers
        heldObject.rotation = targetAnchor.transform.rotation;

        // Markiere Anker als belegt
        isOccupied = true;
        targetAnchor.isOccupied = true;
    }
}