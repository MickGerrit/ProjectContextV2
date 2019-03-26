using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour {

    //Reference
    private GameManager gameManager;
    private Stats stats;
    private TurnSystem turnSystem;
    private Occupance occupance;
    public GameObject minecart;
    public int mineCounter;
    private bool doOnce;
    private bool doOnce2;
    public bool minecartSent;
    private bool animationDoOnce;
    private int mineTurnCost;
    private int turnCounter;

    public Animator animController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        occupance = this.GetComponent<Occupance>();
        turnSystem = gameManager.GetComponent<TurnSystem>();
        mineCounter = occupance.maximumOccupanceAmount;
        animationDoOnce = true;
        minecartSent = false;
        mineTurnCost = 2;
        doOnce = true;
        doOnce2 = true;
    }

    private void Update()
    {
        //if (turnSystem.Turn == TurnSystem.turn.GameTurn && doOnce && occupance.occupanceAmount > 0)
        //{
        //    doOnce = false;
        //    for (int i = 0; i < occupance.occupanceAmount; i++)
        //    {
        //        mineCounter -= 1;
        //    }
        //}
        //if (turnSystem.Turn == TurnSystem.turn.PlayerTurn && mineCounter < 1 && !doOnce)
        //{
        //    StartCoroutine(MineCartReturnAnimation());
        //}
        if (gameManager.turnCount - turnCounter >= mineTurnCost && doOnce2)
        {
            StartCoroutine(MineCartReturnAnimation());
            doOnce2 = false;
        }

        if(minecartSent == false)
            turnCounter = gameManager.turnCount;

        if (turnSystem.Turn == TurnSystem.turn.PlayerTurn)
            doOnce = true;

        if (turnSystem.Turn == TurnSystem.turn.GameTurn && minecartSent == false)
        {
            if (occupance.occupanceAmount > 0)
            {
                minecartSent = true;
                doOnce2 = true;
                turnCounter = gameManager.turnCount;
            }
        }

        if (minecartSent && animationDoOnce)
        {
            StartCoroutine(MineCartGoAnimation());
            animationDoOnce = false;
        }
    }

    IEnumerator MineCartGoAnimation()
    {
        animController.Play("MineCartEnter");
        minecart.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(4.25f);
        minecart.SetActive(false);

    }

    IEnumerator MineCartReturnAnimation()
    {
        animController.Play("MinecartReturn");
        yield return new WaitForSeconds(6f);
        stats.gem = Mathf.RoundToInt(gameManager.gemsEarned);
     
        mineCounter = occupance.maximumOccupanceAmount;
        minecart.SetActive(true);
        minecartSent = false;
        animationDoOnce = true;
    }
}
