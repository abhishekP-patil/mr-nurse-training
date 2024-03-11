using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnTouch : MonoBehaviour
{
    public Material redMaterial;

    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Renderer>().material = redMaterial;
    }
}

