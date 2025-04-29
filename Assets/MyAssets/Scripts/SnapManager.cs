using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject ghostPrefab;
    public InputActionReference snapButton;

    private GameObject ghostInstance;
    private Vector3? snapTargetPosition = null;
    private Quaternion snapTargetRotation;

    private XRGrabInteractable grab;
    private bool isHeld = false;

    private void Awake()
    {
        ghostInstance = Instantiate(ghostPrefab);
        ghostInstance.SetActive(false);

        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(_ => isHeld = true);
        grab.selectExited.AddListener(_ =>
        {
            if (snapTargetPosition != null && snapButton.action.IsPressed())
            {
                // Snap ausführen
                transform.rotation = snapTargetRotation;
                transform.position = snapTargetPosition.Value;
                
            }

            // Aufräumen
            isHeld = false;
            snapTargetPosition = null;
            ghostInstance.SetActive(false);
        });
    }

    private void Update()
    {
        if (!isHeld) return;

        if (snapTargetPosition != null && snapButton.action.IsPressed())
        {
            ghostInstance.transform.rotation = snapTargetRotation;
            ghostInstance.transform.position = snapTargetPosition.Value;
            
            ghostInstance.SetActive(true);
        }
        else
        {
            ghostInstance.SetActive(false);
        }
    }

    public void SetSnapTarget(Vector3 position, Quaternion rotation) //, Quaternion rotation
    {
        snapTargetPosition = position;
        snapTargetRotation = rotation;
    }

    public void ClearSnapTarget()
    {
        snapTargetPosition = null;
        ghostInstance.SetActive(false);
    }
}