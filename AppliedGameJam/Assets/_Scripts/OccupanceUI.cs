using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OccupanceUI : MonoBehaviour {

    private Occupance occupance;
    private Camera playerCamLoc;
    private Transform followTarget;
    public GameObject occupanceAmountUIPrefab;
    public Text occupanceAmountText;
    public Text occupanceMaxAmountText;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private void Start()
    {
        occupance = GetComponentInParent<Occupance>();
        followTarget = occupance.gameObject.transform;
        playerCamLoc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        occupanceAmountText.GetComponent<Outline>().enabled = false;
        occupanceMaxAmountText.GetComponent<Outline>().enabled = false;
        if (occupance.gameObject.tag == "TownHall")
        {
            offsetY = 1.8f;
            offsetZ = -5f;
        }
        else if (occupance.gameObject.tag == "Tree")
        {
            offsetY = 1.2f;
            offsetZ = -5f;
        }
        else if (occupance.gameObject.tag == "House1")
        {
            offsetY = 2f;
            offsetZ = -5f;
        }
        else if (occupance.gameObject.tag == "House2")
        {
            offsetY = 2f;
            offsetZ = -5f;
        }
        else if (occupance.gameObject.tag == "Mine")
        {
            offsetY = 1f;
            offsetZ = -5f;
        }
        else
        {
            offsetY = 1f;
            offsetZ = -3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(occupance.gameObject.tag == "TownHall" || occupance.gameObject.tag == "House1" || occupance.gameObject.tag == "House2" || occupance.gameObject.tag == "House3")
        {
            occupanceMaxAmountText.GetComponent<Text>().color = Color.green;
            occupanceAmountText.GetComponent<Text>().color = Color.green;
        }
        if (occupance.gameObject.tag == "Mine")
        {
            occupanceMaxAmountText.GetComponent<Text>().color = Color.magenta;
            occupanceAmountText.GetComponent<Text>().color = Color.magenta;
        }

        if (occupance.occupanceAmount > 0)
        {
            this.gameObject.GetComponent<Canvas>().enabled = true;
            occupanceAmountText.text = occupance.occupanceAmount.ToString("F0");
            occupanceMaxAmountText.text = occupance.maximumOccupanceAmount.ToString("F0");
        }

        if(occupance.occupanceAmount < 1)
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
        }
        this.transform.LookAt(playerCamLoc.transform);
        this.transform.position = followTarget.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}
