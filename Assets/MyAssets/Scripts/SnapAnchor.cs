using UnityEngine;

public class SnapAnchor : MonoBehaviour
{
    private SnapManager manager;
    private bool hasEntered;
    private Collider currentCol;

    private void Awake()
    {
        manager = GetComponentInParent<SnapManager>();
    }

    private void FixedUpdate()
    {
        if (hasEntered)
        {
            if (currentCol == null || currentCol.GetComponent<SnapAnchor>() == null)
            {
                hasEntered = false;
                manager?.ClearSnapTarget();
                return;
            }
            SetCurrentSnapPosition();
        }
    }

    private void SetCurrentSnapPosition()
    {
        if (currentCol == null) return;

        SnapAnchor otherAnchor = currentCol.GetComponent<SnapAnchor>();
        if (otherAnchor == null || otherAnchor.transform.root == transform.root)
        {
            hasEntered = false;
            manager?.ClearSnapTarget();
            return;
        }
        
        Vector3 offset1 = transform.parent.position - transform.position;
        Vector3 snappedPosition = currentCol.bounds.center + offset1;
        //Debug.Log("LocalPosition: " + transform.position);
        
        Vector3 rotatedLocalPosition = otherAnchor.transform.rotation * transform.localPosition;
        Vector3 futureAnchorWorldPosition = transform.parent.position + rotatedLocalPosition;
        
        // Offset berechnen
        Vector3 offset =  otherAnchor.transform.position - futureAnchorWorldPosition;
        //Vector3 snappedPosition =  transform.position + offset;

        manager?.SetSnapTarget(snappedPosition, otherAnchor.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();
        if (otherAnchor != null && otherAnchor.transform.root != transform.root)
        {
            hasEntered = true;
            currentCol = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();
        if (otherAnchor != null && otherAnchor.transform.root != transform.root)
        {
            hasEntered = false;
            currentCol = null;
            manager?.ClearSnapTarget();
        }
    }
}
