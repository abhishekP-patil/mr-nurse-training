using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Oculus;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;

public class NewInstructorGameManager : MonoBehaviour
{
    public GameObject ContactBorneContaminant;
    public GameObject AirborneContaminant;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject SceneMainMenu;
    public GameObject initTooltip;
    public GameObject exitTestModeTooltip;

    public bool isMenuActive = false;

    // Height offset from the spawner's position
    public float heightOffset = 1.0f;
    // Start is called before the first frame update


    private List<GameObject> meshContaminants;
    private List<GameObject> particleContaminants;
    private Material leftHandMaterial;
    private Material rightHandMaterial;

    public bool isTestMode = false;

    void Start()
    {
        meshContaminants = new List<GameObject>();
        particleContaminants = new List<GameObject>();
        leftHandMaterial = LeftHand.GetComponent<SkinnedMeshRenderer>().material;
        rightHandMaterial = RightHand.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            if (OVRInput.GetDown(OVRInput.Button.One) && isTestMode == false)
            {
                initTooltip.SetActive(false);
                SceneMainMenu.SetActive(!SceneMainMenu.activeSelf);
                SceneMainMenu.transform.position = gameObject.transform.position;
                //SceneMainMenu.transform.rotation = gameObject.transform.rotation;

                Vector3 sourceEulerAngles = gameObject.transform.rotation.eulerAngles;

                // Create a new Vector3 for the destination rotation, setting Z rotation to the destination's current Z rotation
                Vector3 destinationEulerAngles = new Vector3(sourceEulerAngles.x, sourceEulerAngles.y, 0);

                // Convert the Euler angles back to a Quaternion and apply it to the destination
                SceneMainMenu.transform.rotation = Quaternion.Euler(destinationEulerAngles);

            }
        }
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
        {

            if (OVRInput.GetDown(OVRInput.Button.Three) && isTestMode == true)
            {
                EndTestMode();
            }
        }
    }

    public void InitializeSetupMode()
    {

    }

    public void CreateNewTrainingRoomSetup()
    {
       
    }

    public void ContactBorneContaminantSpawn()
    {
        Vector3 spawnPosition = SceneMainMenu.transform.position;
        SceneMainMenu.SetActive(false);
        GameObject spawnedContaminant = Instantiate(ContactBorneContaminant, spawnPosition, Quaternion.identity);
        meshContaminants.Add(spawnedContaminant);

    }

    public void AirBorneContaminantSpawn()
    {
        Vector3 spawnPosition = SceneMainMenu.transform.position;
        SceneMainMenu.SetActive(false);
        GameObject spawnedContaminant = Instantiate(AirborneContaminant, spawnPosition, Quaternion.identity);
        particleContaminants.Add(spawnedContaminant);
    }

    public void StartTestMode()
    {
        //List<GrabInteractable> meshInteractables = new List<GrabInteractable>();
        //List<GrabInteractable> particleInteractables = new List<GrabInteractable>();


        LeftHand.GetComponent<BoxCollider>().enabled = true;
        RightHand.GetComponent<BoxCollider>().enabled = true;
        SceneMainMenu.SetActive(false);
        isTestMode = true;
        exitTestModeTooltip.SetActive(true);

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
                GameObject panel = contaminant.transform.Find("PanelInteractable").gameObject;
                panel.GetComponent<GrabInteractable>().enabled = false;
                panel.GetComponent<HandGrabInteractable>().enabled = false;
                panel.GetComponent<BoxCollider>().enabled = false;
                GameObject contaminantMenu = panel.GetComponent<ContactContaminationOptionsMenuSpawner>().spawnedObject;
                if(contaminantMenu != null)
                {
                    Destroy(contaminantMenu);
                }
                if(panel.transform.Find("Cube").gameObject.GetComponent<ContactCollisions>().isReadable == true)
                {
                    panel.transform.Find("Cube").gameObject.GetComponent<ContactCollisions>().SpawnReadable();
                }
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
                GameObject panel = contaminant.transform.Find("PanelInteractable").gameObject;
                panel.transform.Find("Particle System").gameObject.GetComponent<DropletCollisions>().SpawnReadable();
                panel.GetComponent<GrabInteractable>().enabled = false;
                panel.GetComponent<HandGrabInteractable>().enabled = false;
                panel.GetComponent<BoxCollider>().enabled = false;
                GameObject contaminantMenu = panel.GetComponent<ContactContaminationOptionsMenuSpawner>().spawnedObject;
                if (contaminantMenu != null)
                {
                    Destroy(contaminantMenu);
                }
                //panel.transform.Find("Particle System").gameObject.GetComponent<DropletCollisions>().SpawnReadable();
                //panel.transform.Find("Particle System").gameObject.SetActive(false);
                //if (panel.transform.Find("Particle System").GetComponent<DropletCollisions>().isReadable == true)
                //{
                //  panel.transform.Find("Particle System").GetComponent<PokeInteractable>().enabled = true;
                //}
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
        isTestMode = false;

        LeftHand.GetComponent<BoxCollider>().enabled = false;
        RightHand.GetComponent<BoxCollider>().enabled = false;
        exitTestModeTooltip.SetActive(false);
        initTooltip.SetActive(true);

        //List<GrabInteractable> meshInteractables = new List<GrabInteractable>();
        //List<GrabInteractable> particleInteractables = new List<GrabInteractable>();

        LeftHand.GetComponent<SkinnedMeshRenderer>().material = leftHandMaterial;
        RightHand.GetComponent<SkinnedMeshRenderer>().material = rightHandMaterial;

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
                GameObject panel = contaminant.transform.Find("PanelInteractable").gameObject;
                panel.GetComponent<GrabInteractable>().enabled = true;
                panel.GetComponent<HandGrabInteractable>().enabled = true;
                panel.GetComponent<BoxCollider>().enabled = true;
                panel.transform.Find("Cube").GetComponent<PokeInteractable>().enabled = false;
                GameObject readableInfo = panel.transform.Find("Cube").GetComponent<ContactCollisions>().spawnedObject;
                if(readableInfo != null)
                {
                    Destroy(readableInfo);
                }
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
                GameObject panel = contaminant.transform.Find("PanelInteractable").gameObject;
                panel.GetComponent<GrabInteractable>().enabled = true;
                panel.GetComponent<HandGrabInteractable>().enabled = true;
                panel.GetComponent<BoxCollider>().enabled = true;
                GameObject readableInfo = panel.transform.Find("Particle System").GetComponent<DropletCollisions>().spawnedObject;
                if (readableInfo != null)
                {
                    Destroy(readableInfo);
                }
                panel.transform.Find("Particle System").GetComponent<PokeInteractable>().enabled = false;
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
