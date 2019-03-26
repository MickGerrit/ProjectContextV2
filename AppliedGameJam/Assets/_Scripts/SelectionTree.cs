using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionTree : MonoBehaviour {

    private SelectionArrow selectionArrow;
    private Transform offsetLoc;
    private Transform followTarget;
    public Transform playerCamLoc;
    public Transform planet;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public Camera cam;


    // Use this for initialization
    void Start () {
        selectionArrow = FindObjectOfType<SelectionArrow>();
    }

    // Update is called once per frame
    void Update() {

        if (selectionArrow.isSelecting && selectionArrow.isSelectingTree)
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            this.transform.position = selectionArrow.hitObject.transform.position + new Vector3(offsetX, offsetY, offsetZ);
            this.transform.LookAt(playerCamLoc);
        }
        else
        {
            this.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
