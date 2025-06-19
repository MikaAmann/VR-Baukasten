using UnityEngine;

public class CycleMaterial : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float baseMass;
    public MaterialScriptableObject[] mso;
    public int currentIndex = 0;
    
    void Awake()
    {
        baseMass = GetComponent<Rigidbody>().mass;
        ApplyMaterialProperties(mso[0]);
    }
    
    public void CycleSMOArray()
    {
        currentIndex = (currentIndex + 1) % mso.Length;
        ApplyMaterialProperties(mso[currentIndex]);
    }
    
    private void ApplyMaterialProperties(MaterialScriptableObject newMaterial)
    {
        GetComponent<Rigidbody>().mass = baseMass * newMaterial.weightModifier;
        GetComponent<Collider>().material = newMaterial.physicsMaterial;
        GetComponent<MeshRenderer>().material = newMaterial.material;
        GetComponent<AudioSource>().clip = newMaterial.impactSound;
    }
}
