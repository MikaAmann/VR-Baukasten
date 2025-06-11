using UnityEngine;

public class ScoringBlock : MonoBehaviour
{
    public float primitiveMultiplier = 1f;
    public float materialMultiplier = 1f;

    public float GetHeightFactor()
    {
        float y = transform.position.y;

        if (y < 1f) return 1f;
        else if (y < 2f) return 2f;
        else if (y < 3f) return 3f;
        else if (y < 4f) return 4f;
        else return 4f;
    }

    public float GetScore()
    {
        float heightFactor = GetHeightFactor();
        return primitiveMultiplier * materialMultiplier * heightFactor;
    }
}
