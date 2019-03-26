using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        stats.wood = stats.wood - stats.farmWoodCost;
    }

    public void DestroyFarm()
    {
        stats.wood += Mathf.RoundToInt(stats.farmWoodCost/3);
        Destroy(transform.gameObject, .1f);
    }
}
