using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverToGetStats : ObjectSelecter {

    public GameObject currentGameObject;
    public float hoverMinimum;
    public float currentHoveringTime;
    private ObjectUIPositioner objectUIPositioner;
    public PlanetRotationControls planetRotationControls;
    public GameObject hoverCanvas;

	// Use this for initialization
	void Awake () {
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        objectUIPositioner = GetComponent<ObjectUIPositioner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!Input.GetButton("Fire1") && !objectUIPositioner.blockOtherRayCasts) {
            currentHoveringTime += Time.deltaTime;
        }
        if ((Input.GetButton("Fire1") && objectUIPositioner.blockOtherRayCasts || currentGameObject == null) && objectUIPositioner.hitObject == null) {
            currentHoveringTime = 0;
            hoverCanvas.SetActive(false);
        }
        this.transform.position = new Vector3(currentGameObject.transform.position.x + objectUIPositioner.offsetX, 
            currentGameObject.transform.position.y + objectUIPositioner.offsetY, objectUIPositioner.zPosition);
        this.transform.LookAt(objectUIPositioner.playerCamLoc);

        if (currentHoveringTime > hoverMinimum) {
            planetRotationControls.CancelInvoke();
            hoverCanvas.SetActive(true);
            Debug.Log("Hovering");
        }
	}

    private void FixedUpdate() {
        currentGameObject = GetGameObjectWhileHovering(layerMask, sceneCamera);
    }
}
