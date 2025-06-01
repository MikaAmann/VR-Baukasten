using UnityEngine;

public class ScoringBlock : MonoBehaviour
{
    public float primitiveMultiplier = 1f;
    public float materialMultiplier = 1f;

    public float GetHeightFactor()
    {
        float y = transform.position.y;

        if (y < 2f) return 1f;        // Ebene 1
        else if (y < 4f) return 2f;   // Ebene 2
        else return 3f;              // Ebene 3
    }

    public float GetScore()
    {
        float heightFactor = GetHeightFactor();
        return primitiveMultiplier * materialMultiplier * heightFactor;
    }
}

