using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bush : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    private SelectionArrow selectionArrow;
    private Stats stats;
    public GameObject mushCaps;
    public bool hasMushCaps;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        selectionArrow = FindObjectOfType<SelectionArrow>();
        //gameManager.trees.Add(this.gameObject);
        hasMushCaps = true;
    }

    public void GatherTreePerform()
    {
        selectionArrow.isSelecting = false;
        //gameManager.trees.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }

    public void GatherBushPerform()
    {
        stats.food += 5f;
        selectionArrow.isSelecting = false;
        mushCaps.SetActive(false);
        hasMushCaps = false;
    }

}
