using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House3 : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        stats.wood = stats.wood - stats.house3WoodCost;
        stats.gem = stats.gem - stats.house3GemCost;
    }

    public void DestroyHouse3()
    {
        stats.wood += stats.house3WoodCost/3;
        Destroy(transform.gameObject, .1f);
    }
}
