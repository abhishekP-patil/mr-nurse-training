using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactContaminationOptionsMenuSpawner : MonoBehaviour
{
    public GameObject ContactContaminationOptionsMenu;
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

    public void ContactContaminationOptionsMenuSpawn(){
        isMenu = !isMenu;

        if(isMenu == true)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, heightOffset, 0);
            spawnedObject = Instantiate(ContactContaminationOptionsMenu, spawnPosition, Quaternion.identity);
            spawnedObject.GetComponent<ContactContaminantColorPicker>().contaminant = contaminant;
        }
        else
        {
            if(spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
        }
    }
}
