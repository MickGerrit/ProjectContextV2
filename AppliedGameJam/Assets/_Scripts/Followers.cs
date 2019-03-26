using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followers : MonoBehaviour {

    //Reference
    public Transform followerSpawn;
    public GameObject prefab;
    public Transform planet;
    public GameManager gameManager;
    private bool doOnce;

    private float followerAmount;

	// Use this for initialization
	void Start () {
        doOnce = true;
        followerAmount = 3;
    }
	
	// Update is called once per frame
	void Update () {
        //Max followers on 100% happiness = 6
        //followerAmount = Random.Range(Mathf.RoundToInt(gameManager.GetComponent<Stats>().co2 / 16.7f);

        //Followers are random taking CO2 into consideration
        //followerAmount = Random.Range(Mathf.RoundToInt(gameManager.GetComponent<Stats>().co2 / 50f), Mathf.RoundToInt(gameManager.GetComponent<Stats>().co2 / 12.5f)) ;



        if (gameManager.GetComponent<TurnSystem>().Turn == TurnSystem.turn.GameTurn)
        {
            //Followers are completely random between
            int followerMultiplier = Random.Range(gameManager.turnCount+1, 3+gameManager.turnCount*2);
            followerAmount = Random.Range((gameManager.turnCount+2), (Mathf.RoundToInt(gameManager.turnCount*followerMultiplier/10))+6);
            doOnce = true;
        }


		if(gameManager.GetComponent<TurnSystem>().Turn == TurnSystem.turn.PlayerTurn && doOnce)
        {
            for(int i = 0; i < followerAmount; i++)
            {
                GameObject instantiatedPrefab;
                instantiatedPrefab = Instantiate(prefab, followerSpawn.transform.position, Quaternion.identity);
                instantiatedPrefab.transform.SetParent(planet);
                instantiatedPrefab.transform.localScale = Vector3.one;
            }

            doOnce = false;
        }
	}
}
