using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactContaminantColorPicker : MonoBehaviour
{

    public FlexibleColorPicker fcp;
    public GameObject contaminant;

    // Start is called before the first frame update
    void Start()
    {
        var main = contaminant.GetComponent<Renderer>().material;
        fcp.color = main.color;
    }

    // Update is called once per frame
    void Update()
    {
        var main = contaminant.GetComponent<Renderer>().material;
        main.color = fcp.color;
    }
}
