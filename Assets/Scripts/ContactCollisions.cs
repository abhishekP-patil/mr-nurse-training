using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactCollisions : MonoBehaviour
{
    public bool isReadable;
    public bool isTouchable;
    public bool isReadableActive;

    public GameObject readableObject;
    public GameObject spawnedObject;

    public float heightOffset = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTouchable == true)
        {
            if (other.gameObject.tag == "LeftHand" || other.gameObject.tag == "RightHand")
            {
                other.gameObject.GetComponent<SkinnedMeshRenderer>().material = this.gameObject.GetComponent<MeshRenderer>().material;
            }
        }
    }

    public void toggleReadableSpawn()
    {
        isReadableActive = !isReadableActive;

        if(isReadableActive == true)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, heightOffset, 0);
            spawnedObject = Instantiate(readableObject, spawnPosition, Quaternion.identity);
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
        }
        else
        {
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
        }
    }

    public void SpawnReadable()
    {
        if (isReadable == true)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, heightOffset, 0);
            spawnedObject = Instantiate(readableObject, spawnPosition, Quaternion.identity);
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
        }
    }
}
