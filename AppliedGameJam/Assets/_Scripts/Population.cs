using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Population : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    private SelectionArrow selectionArrow;
    private Stats stats;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        selectionArrow = FindObjectOfType<SelectionArrow>();
        gameManager.population.Add(this.gameObject);
    }

    public void DiePerform()
    {
        selectionArrow.isSelecting = false;
        gameManager.population.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }

}
