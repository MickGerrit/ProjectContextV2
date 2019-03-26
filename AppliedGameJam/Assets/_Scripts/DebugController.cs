using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour {

    private GameManager gameManager;
    private Stats stats;
    public GameObject debugScreen;

    private bool debugBool;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponent<Stats>();
        debugBool = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha0) && !debugBool)
        {
            Debug.Log("1");
            debugScreen.SetActive(true);
            debugBool = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0) && debugBool)
        {
            Debug.Log("2");
            debugScreen.SetActive(false);
            debugBool = false;
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {
            stats.co2 += 10f;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            stats.happiness += 10f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            stats.power += 10f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            stats.wood += 10;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            stats.gem += 10;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            stats.energy += 10f;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gameManager.turnCount += 1;
            gameManager.turnCountText.text = gameManager.turnCount.ToString();
        }
    }
}
