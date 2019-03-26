using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        stats.wood = stats.wood - stats.house1WoodCost;
        gameManager.house1.Add(this.gameObject);
    }

    public void DestroyHouse1()
    {
        stats.wood += stats.house1WoodCost / 3;
        gameManager.house1.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }
}
