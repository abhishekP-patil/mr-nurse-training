using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseGameManager : MonoBehaviour
{
    public Material redMaterial; // Assign this material in the inspector

    // Assuming this script is attached to the hand GameObject
    private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        // Get the SkinnedMeshRenderer component on start
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Create an array to hold the new materials
        var materials = new Material[skinnedMeshRenderer.materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            // Assign the red material to each entry in the array
            materials[i] = redMaterial;
        }

        // Update the materials array of the SkinnedMeshRenderer
        skinnedMeshRenderer.materials = materials;
    }
}