using System;
using UnityEngine;

public class PlayerOnPlattformManager : MonoBehaviour
{
    public bool playerOnPlattform = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerOrigin"))
        {
            playerOnPlattform = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerOrigin"))
        {
            playerOnPlattform = false;
        }
    }
}
