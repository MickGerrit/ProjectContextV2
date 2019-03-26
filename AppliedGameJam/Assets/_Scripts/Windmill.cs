using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        gameManager.windmill.Add(this.gameObject);
        stats.wood = stats.wood - stats.windmillWoodCost;
    }

    public void DestroyWindmill()
    {
        stats.wood += Mathf.RoundToInt(stats.windmillWoodCost/2);
        gameManager.windmill.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }
}
