using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletContaminationOptionsMenuSpawner : MonoBehaviour
{
    public GameObject DropletContaminationOptionsMenu;
    public float heightOffset = 0.3f;
    public GameObject contaminant;
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
            spawnedObject.GetComponent<DropletContaminationOptions>().contaminant = contaminant;
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
