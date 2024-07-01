using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContactContaminantColorPicker : MonoBehaviour
{

    public FlexibleColorPicker fcp;
    public GameObject contaminant;
    public GameObject readableToggle;
    public GameObject touchableToggle;
    public GameObject contaminantParent;

    public Material greenBacteriophage;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetupContaminantOptions(GameObject contaminant)
    {
        var main = contaminant.GetComponent<Renderer>().material;
        fcp.color = main.color;
        bool readableStatus = contaminant.GetComponent<ContactCollisions>().isReadable;
        readableToggle.GetComponent<Toggle>().isOn = readableStatus;
        bool touchableStatus = contaminant.GetComponent<ContactCollisions>().isTouchable;
        touchableToggle.GetComponent<Toggle>().isOn = touchableStatus;
    }

    // Update is called once per frame
    void Update()
    {
        var main = contaminant.GetComponent<Renderer>().material;
        main.color = fcp.color;
    }

    public void toggleReadable()
    {
        //bool readableStatus = contaminant.GetComponent<ContactCollisions>().isReadable;
        contaminant.GetComponent<ContactCollisions>().isReadable = readableToggle.GetComponent<Toggle>().isOn;
    }

    public void toggleTouchable()
    {
        //bool touchableStatus = contaminant.GetComponent<ContactCollisions>().isTouchable;
        contaminant.GetComponent<ContactCollisions>().isTouchable = touchableToggle.GetComponent<Toggle>().isOn;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DeleteContaminant()
    {
        Destroy(contaminantParent);
        Destroy(gameObject);
    }

    public void ChangeMaterial()
    {
        fcp.color = greenBacteriophage.color;
        contaminant.GetComponent<Renderer>().material = greenBacteriophage;
    }
}
