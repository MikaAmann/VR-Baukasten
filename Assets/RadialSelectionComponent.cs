using NUnit.Framework;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class RadialSelectionComponent : MonoBehaviour
{
    [Header("Input")]
    public InputActionReference spawnButtonInput;

    [UnityEngine.Range(2, 20)]
    public int numRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float spacingPart = 0.04f;
    public Transform handTransform;

    public UnityEvent<int> OnPartSelected;

    private System.Collections.Generic.List<GameObject> spawnedParts = new System.Collections.Generic.List<GameObject>();
    private int currentSelectedRadialPart = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        spawnButtonInput.action.Enable();
    }

    private void OnDisable()
    {
        spawnButtonInput.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnButtonInput.action.WasPressedThisFrame())
        {
            SpawnRadialPart();
            Debug.Log("Button Was Pressed");
        }

        if (spawnButtonInput.action.IsPressed())
        {
            getSelectedRadialPart();
        }
        if (spawnButtonInput.action.WasReleasedThisFrame())
        {
            HideAndTriggerSelected();
        }

    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }
    public void getSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProjected = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
        {
            angle += 360;
        }
        Debug.Log("angle " + angle);
        currentSelectedRadialPart = (int)angle * numRadialPart / 360;
        for (int i = 0; i < spawnedParts.Count; i++) {
            if (i == currentSelectedRadialPart) {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }

        }
    }

    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = Quaternion.LookRotation(radialPartCanvas.position - Camera.main.transform.position, Vector3.up);

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }
        spawnedParts.Clear();
            for (int i = 0; i < numRadialPart; i++)
        {
            float angle =  - i * 360 / numRadialPart - spacingPart/2 +180;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);
            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;
            spawnedRadialPart.GetComponent<Image>().fillAmount = (float)1 / numRadialPart - spacingPart;

            spawnedParts.Add(spawnedRadialPart);
        }
    }
}
