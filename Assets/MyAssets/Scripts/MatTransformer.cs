using System.Collections;
using UnityEngine;

public class MatTransformer : MonoBehaviour
{
    [Range(0, 1)]
    public float intensity;
    public float duration;

    private bool hasHit = false;
    private bool isResetting = false;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            collision.gameObject.GetComponent<CycleMaterial>()?.CycleSMOArray();
            hasHit = true;
            TriggerHapticFeedback();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasHit && !isResetting)
        {
            StartCoroutine(ResetHaveHitAfterDelay(1f));
        }
    }
    
    private IEnumerator ResetHaveHitAfterDelay(float delay)
    {
        isResetting = true;
        yield return new WaitForSeconds(delay);
        hasHit = false;
        isResetting = false;
    }

    private void TriggerHapticFeedback()
    {
        if (grabInteractable.isSelected)
        {
            UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor = grabInteractable.interactorsSelecting[0];
            if (interactor is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInputInteractor controllerInteractor)
            {
                controllerInteractor.SendHapticImpulse(intensity, duration);
            }
        }
    }
}
