using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropletContaminationOptions : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public GameObject contaminant;
    public GameObject readableToggle;
    public GameObject touchableToggle;
    public GameObject contaminantParent;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetupContaminantOptions(GameObject contaminant)
    {
        var main = contaminant.GetComponent<ParticleSystem>().main;
        fcp.color = main.startColor.color;
        bool readableStatus = contaminant.GetComponent<DropletCollisions>().isReadable;
        readableToggle.GetComponent<Toggle>().isOn = readableStatus;
        bool touchableStatus = contaminant.GetComponent<DropletCollisions>().isTouchable;
        touchableToggle.GetComponent<Toggle>().isOn = touchableStatus;
    }

    // Update is called once per frame
    void Update()
    {
        var main = contaminant.GetComponent<ParticleSystem>().main;
        main.startColor = fcp.color;
    }

    public void toggleReadable()
    {
        //bool readableStatus = contaminant.GetComponent<ContactCollisions>().isReadable;
        contaminant.GetComponent<DropletCollisions>().isReadable = readableToggle.GetComponent<Toggle>().isOn;
    }

    public void toggleTouchable()
    {
        //bool touchableStatus = contaminant.GetComponent<ContactCollisions>().isTouchable;
        contaminant.GetComponent<DropletCollisions>().isTouchable = touchableToggle.GetComponent<Toggle>().isOn;
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
}
