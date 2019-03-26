using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    public Stats stats;

    public void OnAwake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        gameManager.factories.Add(this.gameObject);
        stats.wood = stats.wood - stats.factoryWoodCost;
    }
    
    public void DestoryFactory()
    {
        stats.wood += Mathf.RoundToInt(stats.factoryWoodCost/2f);
         gameManager.factories.Remove(this.gameObject);
        Destroy(transform.gameObject, .1f);
    }
}
