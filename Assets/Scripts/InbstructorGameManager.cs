using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class InbstructorGameManager : MonoBehaviour
{
    public GameObject ContactBorneContaminant;
    public GameObject AirborneContaminant;

    // Height offset from the spawner's position
    public float heightOffset = 1.0f;
    // Start is called before the first frame update


    private List<GameObject> meshContaminants;
    private List<GameObject> particleContaminants;

    void Start()
    {
        meshContaminants = new List<GameObject>();
        particleContaminants = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeSetupMode()
    {

    }

    public void CreateNewTrainingRoomSetup()
    {
       
    }

    public void ContactBorneContaminantSpawn()
    {
        Vector3 spawnPosition = transform.position - new Vector3(0, 0.1f, -heightOffset);
        GameObject spawnedContaminant = Instantiate(ContactBorneContaminant, spawnPosition, Quaternion.identity);
        meshContaminants.Add(spawnedContaminant);

    }

    public void AirBorneContaminantSpawn()
    {
        Vector3 spawnPosition = transform.position - new Vector3(0, 0.1f, -heightOffset);
        GameObject spawnedContaminant = Instantiate(AirborneContaminant, spawnPosition, Quaternion.identity);
        particleContaminants.Add(spawnedContaminant);
    }

    public void StartTestMode()
    {
        //List<GrabInteractable> meshInteractables = new List<GrabInteractable>();
        //List<GrabInteractable> particleInteractables = new List<GrabInteractable>();

        foreach (GameObject contaminant in meshContaminants)
        {
            if (contaminant != null)
            {
                Grabbable[] grabs = contaminant.GetComponents<Grabbable>();
                foreach(Grabbable grab in grabs)
                {
                    grab.enabled = false;
                }
                contaminant.GetComponent<PanelHoverState>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<GrabInteractable>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<BoxCollider>().enabled = false;
                contaminant.transform.Find("RotatorVerticalTop").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorVerticalBottom").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorHorizontalLeft").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorHorizontalRight").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerTopLeft").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerTopRight").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerBottomLeft").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerBottomRight").gameObject.SetActive(false);
                //meshInteractables = contaminant.GetComponentsInChildren<GrabInteractable>().ToList();
                //foreach (GrabInteractable interactable in meshInteractables)
                //{
                //    if(interactable != null)
                //    {
                //        interactable.enabled = false;
                //    }
                //}
            }
        }

        foreach (GameObject contaminant in particleContaminants)
        {
            if (contaminant != null)
            {
                Grabbable[] grabs = contaminant.GetComponents<Grabbable>();
                foreach (Grabbable grab in grabs)
                {
                    grab.enabled = false;
                }
                contaminant.GetComponent<PanelHoverState>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<GrabInteractable>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<BoxCollider>().enabled = false;
                contaminant.transform.Find("RotatorVerticalTop").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorVerticalBottom").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorHorizontalLeft").gameObject.SetActive(false);
                contaminant.transform.Find("RotatorHorizontalRight").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerTopLeft").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerTopRight").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerBottomLeft").gameObject.SetActive(false);
                contaminant.transform.Find("ScalerBottomRight").gameObject.SetActive(false);
                //particleInteractables = contaminant.GetComponentsInChildren<GrabInteractable>().ToList();
                //foreach (GrabInteractable interactable in particleInteractables)
                //{
                //    if (interactable != null)
                //    {
                //        interactable.enabled = false;
                //    }
                //}
            }
        }
    }

    public void EndTestMode()
    {

        //List<GrabInteractable> meshInteractables = new List<GrabInteractable>();
        //List<GrabInteractable> particleInteractables = new List<GrabInteractable>();

        foreach (GameObject contaminant in meshContaminants)
        {
            if (contaminant != null)
            {
                Grabbable[] grabs = contaminant.GetComponents<Grabbable>();
                foreach (Grabbable grab in grabs)
                {
                    grab.enabled = true;
                }
                contaminant.GetComponent<PanelHoverState>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<GrabInteractable>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<BoxCollider>().enabled = true;
                contaminant.transform.Find("RotatorVerticalTop").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorVerticalBottom").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorHorizontalLeft").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorHorizontalRight").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerTopLeft").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerTopRight").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerBottomLeft").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerBottomRight").gameObject.SetActive(true);
                //meshInteractables = contaminant.GetComponentsInChildren<GrabInteractable>().ToList();
                //foreach (GrabInteractable interactable in meshInteractables)
                //{
                //    if(interactable != null)
                //    {
                //        interactable.enabled = false;
                //    }
                //}
            }
        }

        foreach (GameObject contaminant in particleContaminants)
        {
            if (contaminant != null)
            {
                Grabbable[] grabs = contaminant.GetComponents<Grabbable>();
                foreach (Grabbable grab in grabs)
                {
                    grab.enabled = true;
                }
                contaminant.GetComponent<PanelHoverState>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<GrabInteractable>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                contaminant.transform.Find("PanelInteractable").gameObject.GetComponent<BoxCollider>().enabled = true;
                contaminant.transform.Find("RotatorVerticalTop").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorVerticalBottom").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorHorizontalLeft").gameObject.SetActive(true);
                contaminant.transform.Find("RotatorHorizontalRight").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerTopLeft").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerTopRight").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerBottomLeft").gameObject.SetActive(true);
                contaminant.transform.Find("ScalerBottomRight").gameObject.SetActive(true);
                //particleInteractables = contaminant.GetComponentsInChildren<GrabInteractable>().ToList();
                //foreach (GrabInteractable interactable in particleInteractables)
                //{
                //    if (interactable != null)
                //    {
                //        interactable.enabled = false;
                //    }
                //}
            }
        }
    }
}
