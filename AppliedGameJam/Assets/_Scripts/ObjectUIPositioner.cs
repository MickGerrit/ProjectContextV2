using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ObjectUIPositioner : ObjectSelecter {

    private Transform offsetLoc;
    private Transform followTarget;
    public Transform playerCamLoc;
    public Transform planet;

    public float offsetX;
    public float offsetY;
    public float zPosition;

    public bool canSelect;
    public bool isSelecting;
    private bool doOnce;

    public Camera cam;
    public LayerMask buildingLayerMask;
    public string UITag;
    public string deselectTag;
    public RaycastHit hit;
    public GameObject hitObject;
    public GameObject prevObject;

    public List <GameObject> uiGameObjectsToBlockClicks;
    public bool blockOtherRayCasts;
    public bool hittingRaycast;

    public BuildingOnUIHandler buildingOnUIHandler;
    public PlanetRotationControls planetRotationControls;

    public GameObject buildingCanvas;
    public GameObject hoverCanvas;


    // Use this for initialization
    void Start() {
        planetRotationControls = GameObject.Find("Planet").GetComponent<PlanetRotationControls>();
        hittingRaycast = false;
        canSelect = true;
        doOnce = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        planet = GameObject.FindGameObjectWithTag("Planet").transform;
    }
    // Update is called once per frame
    void Update() {
        //Check for those ui elements
        blockOtherRayCasts = IsSelectingAGameObjectInList(uiGameObjectsToBlockClicks);

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) && isSelecting)
            ExitWindow();

        if (isSelecting && hitObject != null) {
            buildingCanvas.SetActive(true);
            buildingCanvas.SetActive(true);
            this.transform.position = new Vector3(hitObject.transform.position.x + offsetX, hitObject.transform.position.y + offsetY, zPosition);
            this.transform.LookAt(playerCamLoc);
        }
        if (!isSelecting || blockOtherRayCasts) {
            hitObject = null;
            buildingCanvas.SetActive(false);
        }
        if (hittingRaycast && Input.GetButtonDown("Fire1")) {
                    isSelecting = true;
                    if (hit.transform.gameObject.tag != UITag) {
                        prevObject = hit.transform.gameObject;
                    }

        }
        if (hittingRaycast && Input.GetButtonUp("Fire1")) {
            if (hit.transform.gameObject == prevObject) {
                buildingOnUIHandler.clickedGameObject = null;
                if (hit.transform.gameObject.tag == deselectTag) {
                    isSelecting = false;
                }
                hitObject = hit.transform.gameObject;
                planetRotationControls.rotX = 0;
                planetRotationControls.rotY = 0;
            }
        }
    }

    private void FixedUpdate() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        hittingRaycast = Physics.Raycast(ray, out hit, Mathf.Infinity, buildingLayerMask);
    }

    public void ExitWindow() {
        isSelecting = false;
    }
}
