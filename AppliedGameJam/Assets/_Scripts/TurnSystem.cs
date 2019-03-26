using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {

    public enum turn {PlayerTurn, GameTurn, EventTurn};
    public turn Turn;

    //Reference
    private GameManager gameManager;
    private GatherResources gatherResources;
    public PlanetRotationControls planetRotationControls;
    public GameObject playButton;

	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
        gatherResources = GetComponent<GatherResources>();
	}
	
	// Update is called once per frame
	void Update () {
        switch (Turn)
        {
            // The idle playerState
            case turn.PlayerTurn:
                playButton.SetActive(true);
                planetRotationControls.staticRotationInvokeTime = 6f;
                planetRotationControls.staticRotationSpeed = 15f;
                gameManager.planetControls.isControllingRotating = true;
                break;

            // The wandering playerState
            case turn.GameTurn:
                planetRotationControls.staticRotationInvokeTime = 0f;
                playButton.SetActive(false);
                gameManager.CalculateC02();
                gameManager.CalculatePower();
                gameManager.CalculateEnergy();
                gameManager.CalculateWood();
                gameManager.CalculateGem();
                gameManager.CalculateHappiness();
                gameManager.LoseConditions();
                gameManager.planetControls.isControllingRotating = false;
                break;

            // The roaming playerState
            case turn.EventTurn:

                break;
        }
    }
}
