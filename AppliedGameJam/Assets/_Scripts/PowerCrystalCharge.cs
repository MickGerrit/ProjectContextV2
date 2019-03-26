using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCrystalCharge : MonoBehaviour {

    public Transform playerCamLoc;
    public Transform followTarget;

    public float offsetX;
    public float offsetY;
    public float offsetZ;


    private void Start()
    {
        offsetY = 2.5f;
        offsetZ = -2f;
    }

    // Update is called once per frame
    void Update() {

        this.transform.LookAt(playerCamLoc);
        this.transform.position = followTarget.transform.position + new Vector3(offsetX, offsetY, offsetZ);
    }
}
