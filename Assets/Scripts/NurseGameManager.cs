using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseGameManager : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem particleSystem = other.gameObject.GetComponent<ParticleSystem>();
        /*if (particleSystem != null)
        {
            // Get the start color of the particle system
            ParticleSystem.MainModule mainModule = particleSystem.main;
            Color particleColor = mainModule.startColor.color;
            Debug.Log(particleColor);
            // Update the hand's material _ColorBottom property to match the particle system's start color

            Material handMaterial = GetComponent<MeshRenderer>().material;
            handMaterial.SetColor("_ColorBottom", particleColor);
            handMaterial.SetColor("Color Top", particleColor);
            GetComponent<MeshRenderer>().material = handMaterial; // This may not be necessary if you're directly modifying the material
        }*/
        if (other.CompareTag("Cube"))
        {
            // Assume that the cube's material is a standard material
            Renderer cubeRenderer = other.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                Color cubeColor = cubeRenderer.material.color;
                Material handMaterial = skinnedMeshRenderer.material;
                handMaterial.SetColor("_ColorTop", cubeColor);
                skinnedMeshRenderer.material = handMaterial; // This may not be necessary if you're directly modifying the material
            }
        }
    }
}