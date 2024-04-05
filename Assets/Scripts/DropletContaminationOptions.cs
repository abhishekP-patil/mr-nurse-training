using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletContaminationOptions : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public GameObject contaminant;

    // Start is called before the first frame update
    void Start()
    {
        var main = contaminant.GetComponent<ParticleSystem>().main;
        fcp.color = main.startColor.color;
    }

    // Update is called once per frame
    void Update()
    {
        var main = contaminant.GetComponent<ParticleSystem>().main;
        main.startColor = fcp.color;
    }
}
