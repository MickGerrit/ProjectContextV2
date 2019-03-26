using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour {

    public GameObject TownHallWoodCost;
    private GameManager gameManager;
    private Stats stats;

    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
	}

    private void OnMouseEnter()
    {
        Debug.Log("1");
        TownHallWoodCost.SetActive(true);
    }

    private void OnMouseOver()
    {
        Debug.Log("2");
        TownHallWoodCost.SetActive(true);
    }
    private void OnMouseExit()
    {
        Debug.Log("3");
        TownHallWoodCost.SetActive(false);
    }
}
