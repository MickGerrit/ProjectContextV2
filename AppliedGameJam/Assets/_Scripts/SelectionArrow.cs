using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrow : MonoBehaviour {

    private Transform offsetLoc;
    private Transform followTarget;
    public Transform playerCamLoc;
    public Transform planet;
    public GameObject buttonGatherWood;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public bool canSelect;
    public bool isSelecting;
    private bool doOnce;
    public bool isSelectingTree;
    public bool isSelectingInhabitant;
    public bool isSelectingBush;

    public Camera cam;
    public LayerMask layerMask;
    public RaycastHit hit;
    public GameObject hitObject;
    public GameObject prevObject;


    // Use this for initialization
    void Start () {
        canSelect = true;
        doOnce = true;
        isSelectingTree = false;
        isSelectingInhabitant = false;
        isSelectingBush = false;
}

    // Update is called once per frame
    void Update() {

        prevObject = hitObject;
        if (isSelecting)
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.position = hitObject.transform.position + new Vector3(offsetX, offsetY, offsetZ);
            this.transform.LookAt(playerCamLoc);
        }
        else
        {
            hitObject = planet.transform.gameObject;
            this.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (canSelect)
        {
            // Select
            if (Input.GetButtonDown("Fire1") && canSelect)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && Input.GetButton("Fire1") && doOnce)
                {
                    doOnce = false;
                    isSelecting = true;
                    hitObject = hit.transform.gameObject;
                    if (hit.transform.gameObject.tag == "Tree")
                    {
                        isSelectingTree = true;
                        //buttonGatherWood.SetActive(true);
                        //buttonGatherWood.GetComponent<SpriteRenderer>().enabled = true;
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "Inhabitant")
                    {
                        isSelectingInhabitant = true;
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "Bush")
                    {
                        isSelectingBush = true;
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "TownHall")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "Factory")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "Mine")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "House1")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "House2")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }
                    else if (hit.transform.gameObject.tag == "House3")
                    {
                        offsetX = 0f;
                        offsetY = 2.5f;
                        offsetZ = -3f;
                    }


                    if (hit.transform.gameObject.tag != "Tree")
                        isSelectingTree = false;
                    if (hit.transform.gameObject.tag != "Inhabitant")
                        isSelectingInhabitant = false;
                    if (hit.transform.gameObject.tag != "Bush")
                        isSelectingBush = false;
                }
                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && Input.GetButtonDown("Fire1") && hit.transform.gameObject != hitObject)
                {
                    isSelecting = false;
                    buttonGatherWood.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        if(Input.GetButtonUp("Fire1"))
            doOnce = true;
    }

    IEnumerator DisableButtonCycle()
    {
        yield return new WaitForSeconds(.1f);
        buttonGatherWood.SetActive(false);
    }
}
