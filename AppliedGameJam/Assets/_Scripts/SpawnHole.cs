using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnHole : MonoBehaviour
{

    private GameManager gamemanager;
    private Camera playerCamLoc;
    private Transform followTarget;
    public Text populationLeftText;

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    private void Start()
    {
        gamemanager = FindObjectOfType<GameManager>();
        followTarget = transform.parent.GetComponent<Transform>().transform;
        playerCamLoc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        populationLeftText.GetComponent<Outline>().enabled = false;
        offsetY = -1f;
        offsetZ = -2f;

    }

    // Update is called once per frame
    void Update()
    {
        int populationLeftTextAmount = gamemanager.population.Count;
        if (populationLeftTextAmount>=1)
            populationLeftText.GetComponent<Text>().color = Color.yellow;
        else
            populationLeftText.GetComponent<Text>().color = Color.green;
        populationLeftText.text = populationLeftTextAmount.ToString("F0");

        if (1==2)
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
        }
        this.transform.LookAt(playerCamLoc.transform);
        this.transform.position = followTarget.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}
