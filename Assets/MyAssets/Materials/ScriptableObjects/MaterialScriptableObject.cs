using UnityEngine;

[CreateAssetMenu(fileName = "MaterialScriptableObject", menuName = "Scriptable Objects/MaterialScriptableObject")]
public class MaterialScriptableObject : ScriptableObject
{
    //public string materialName;
    public PhysicsMaterial physicsMaterial;
    public Material material;
    public float weightModifier;
    public float scoreMultiplier;
    public AudioClip impactSound;
}
