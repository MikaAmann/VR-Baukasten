using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapAnchor : MonoBehaviour
{
    public bool isOccupied = false;
    public GameObject ghostPrefab;
    public InputActionReference snapButton; // Y-Taste

    private SnapAnchor targetAnchor;
    private GameObject ghostInstance;

    private bool isSelected = false;
    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        ghostInstance = Instantiate(ghostPrefab);
        ghostInstance.SetActive(false);

        grabInteractable = GetComponentInParent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(_ => isSelected = true);
            grabInteractable.selectExited.AddListener(_ =>
            {
                if (targetAnchor != null && ghostInstance.activeSelf)
                    SnapToObject(targetAnchor);

                ClearSnap();
                isSelected = false;
            });
        }
    }

    private void Update()
    {
        if (!isSelected) return;

        if (snapButton.action.IsPressed())
        {
            if (targetAnchor)
            {
                Vector3 offset = transform.position - transform.parent.position;
                Vector3 ghostPos = targetAnchor.transform.position - offset;

                ghostInstance.transform.position = ghostPos;
                ghostInstance.transform.rotation = targetAnchor.transform.rotation;
                ghostInstance.SetActive(true);
            }
            else
            {
                ghostInstance.SetActive(false);
            }
        }
        else
        {
            ghostInstance.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();

        if (otherAnchor != null && !otherAnchor.isOccupied && !isOccupied &&
            otherAnchor.transform.root != transform.root)
        {
            targetAnchor = otherAnchor;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SnapAnchor otherAnchor = other.GetComponent<SnapAnchor>();
        if (otherAnchor != null && otherAnchor == targetAnchor)
        {
            targetAnchor = null;
            ghostInstance.SetActive(false);
        }
    }

    private void SnapToObject(SnapAnchor target)
    {
        Transform heldObject = transform.parent;
        Vector3 offset = transform.position - heldObject.position;
        Vector3 newPosition = target.transform.position - offset;

        heldObject.position = newPosition;
        heldObject.rotation = target.transform.rotation;

        isOccupied = true;
        target.isOccupied = true;

        ghostInstance.SetActive(false);
    }

    private void ClearSnap()
    {
        targetAnchor = null;
        ghostInstance.SetActive(false);
    }
}
