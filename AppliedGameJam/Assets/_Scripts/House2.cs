using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House2 : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        stats.wood = stats.wood - stats.house2WoodCost;
        stats.gem = stats.gem - stats.house2GemCost;
        gameManager.house2.Add(this.gameObject);
    }

    public void DestroyHouse2()
    {
        stats.wood += stats.house2WoodCost/3;
        gameManager.house2.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }
}
