using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScalerInverter2D : MonoBehaviour
{
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;
    }

    void Update()
    {
        if (parentTransform != null)
        {
            Vector3 parentScale = parentTransform.localScale;
            transform.localScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z);
        }
    }
}
