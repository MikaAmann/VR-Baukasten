using UnityEngine;

public class ScoringBlock : MonoBehaviour
{
    public float primitiveMultiplier = 1f;
    private CycleMaterial cycleMaterial;

    private void Awake()
    {
        cycleMaterial = GetComponent<CycleMaterial>();
    }

    public float GetHeightFactor()
    {
        float y = transform.position.y;

        if (y < 12f) return 1f;
        else if (y < 14f) return 2f;
        else if (y < 16f) return 3f;
        else if (y < 18f) return 4f;
        else return 4f;
    }

    public float GetScore()
    {
        float heightFactor = GetHeightFactor();
        float materialMultiplier = cycleMaterial.GetCurrentMaterial().scoreMultiplier;

        return primitiveMultiplier * materialMultiplier * heightFactor;
    }
}

