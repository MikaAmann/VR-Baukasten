using System.Diagnostics;
using UnityEngine;

public class Destroy_test : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        
    }
}
