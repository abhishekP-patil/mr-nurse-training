using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletContaminationOptionsMenuSpawner : MonoBehaviour
{
    public GameObject DropletContaminationOptionsMenu;
    public float heightOffset = 0.3f;
    public GameObject contaminant;
    public GameObject contaminantParent;
    public GameObject spawnedObject;
    public bool isMenu;

    // Start is called before the first frame update
    void Start()
    {
        isMenu = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropletContaminationOptionsMenuSpawn()
    {
        isMenu = !isMenu;

        if (isMenu == true)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, heightOffset, 0);
            spawnedObject = Instantiate(DropletContaminationOptionsMenu, spawnPosition, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;
                directionToPlayer.y = 0; // Remove Y component to keep rotation only on Y axis

                // Ensure directionToPlayer is not the zero vector before calculating the rotation
                if (directionToPlayer != Vector3.zero)
                {
                    // Create a quaternion for rotation towards the player on the Y axis
                    Quaternion toRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

                    toRotation *= Quaternion.Euler(0, 180, 0);

                    // Apply the rotation to this GameObject
                    spawnedObject.transform.rotation = toRotation;
                }
            }
            spawnedObject.GetComponent<DropletContaminationOptions>().contaminant = contaminant;
            spawnedObject.GetComponent<DropletContaminationOptions>().contaminantParent = contaminantParent;
            spawnedObject.GetComponent<DropletContaminationOptions>().SetupContaminantOptions(contaminant);
        }
        else
        {
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
        }
    }
}
