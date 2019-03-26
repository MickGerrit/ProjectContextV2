using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trees : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    private Stats stats;
    private TurnSystem turnSystem;
    private Occupance occupance;
    public int woodCounter;
    private bool doOnce = true;

    public Animator animController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        occupance = this.GetComponent<Occupance>();
        turnSystem = gameManager.GetComponent<TurnSystem>();
        gameManager.trees.Add(this.gameObject);
        woodCounter = occupance.maximumOccupanceAmount;
    }

    private void Update()
    {
        if(turnSystem.Turn == TurnSystem.turn.GameTurn && doOnce && occupance.occupanceAmount > 0)
        {
            doOnce = false;
            for (int i = 0; i < occupance.occupanceAmount; i++)
            {
                woodCounter -= 1;
            }
        }

        if (turnSystem.Turn == TurnSystem.turn.PlayerTurn && woodCounter < 1 && !doOnce)
        {
            for(int i = 0; i < occupance.occupanceAmount; i++)
            {
                GameObject inhabitant = gameManager.workertree[Random.Range(0, gameManager.workertree.Count)];
                gameManager.workertree.Remove(inhabitant);
                gameManager.assignedpopulation.Add(inhabitant);
            }
            StartCoroutine(ChoppedTreeAnimation());

        }

        if (turnSystem.Turn == TurnSystem.turn.PlayerTurn)
            doOnce = true;
    }

    IEnumerator ChoppedTreeAnimation()
    {
        animController.Play("ChoppedTree");
        animController.Play("ChoppedTree1");
        animController.Play("ChoppedTree2");
        animController.Play("ChoppedTree3");
        gameManager.trees.Remove(this.gameObject);
        yield return new WaitForSeconds(1.25f);
        Destroy(transform.gameObject);
    }
}
