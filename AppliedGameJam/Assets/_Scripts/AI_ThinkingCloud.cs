﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ThinkingCloud : MonoBehaviour {

    private Transform offsetLoc;
    public Transform followTarget;
    public Transform playerCamLoc;
    public PlanetRotationControls planetRotController;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public bool thinkingCloudBool;

    //public Animator animController;

    private int thinkingCloudTimer;
    private int thinkingCloudCounter;

    public List<Sprite> thinkingCloud = new List<Sprite>();

    // Use this for initialization
    void Start () {
        offsetX = 0.09f;
        offsetY = 0.08f;
        offsetZ = -0.03f;
        thinkingCloudBool = false;
        thinkingCloudTimer = 400;
        thinkingCloudCounter = 0;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = followTarget.transform.position + new Vector3(offsetX, offsetY, offsetZ);
        this.transform.LookAt(playerCamLoc);

        //Debug Play Fade Animation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.GetComponent<SpriteRenderer>().sprite = thinkingCloud[1];
            thinkingCloudBool = true;
        }

        if(thinkingCloudCounter > thinkingCloudTimer)
        {
            thinkingCloudBool = true;
            thinkingCloudCounter = 0;
        }

        ////Animator Controller
        //if (thinkingCloudBool)
        //{
        //    animController.SetBool("isThinkingCloud", true);
        //    thinkingCloudBool = false;
        //}
        //else
        //    animController.SetBool("isThinkingCloud", false);

        thinkingCloudCounter += 1;

        if(planetRotController.hit.collider.gameObject.tag == "ThinkingCloud" && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Open Menu");
        }
    }




}
