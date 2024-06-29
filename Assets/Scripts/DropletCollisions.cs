using System.Collections.Generic;
using UnityEngine;
public class DropletCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "LeftHand" || other.tag == "RightHand")
        {
            other.gameObject.GetComponent<SkinnedMeshRenderer>().material = this.gameObject.GetComponent<ParticleSystem>().GetComponent<ParticleSystemRenderer>().material;
            other.gameObject.GetComponent<SkinnedMeshRenderer>().material.color = this.gameObject.GetComponent<ParticleSystem>().main.startColor.color;
        }
    }
}