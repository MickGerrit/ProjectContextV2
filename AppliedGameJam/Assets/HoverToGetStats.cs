using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoverToGetStats : ObjectSelecter {

    public GameObject currentGameObject;
    public float hoverMinimum;
    public float currentHoveringTime;
    private ObjectUIPositioner objectUIPositioner;
    public PlanetRotationControls planetRotationControls;
    public GameObject hoverCanvas;
    public GameManager gameManager;
    public ObjectPlacer objectPlacer;

    public Text gemsText;
    public Text woodText;
    public Text happinessText;
    public Text natureText;
    public Text powerText;
    public Text susEnergyText;
    public float timeBetweenTurns;

    // Use this for initialization
    void Start () {
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        objectUIPositioner = GetComponent<ObjectUIPositioner>();

    }
	
	// Update is called once per frame
	void Update () {
        timeBetweenTurns = gameManager.gameTurnDuration + 3;

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

        if (objectUIPositioner.hitObject != null && objectUIPositioner.isSelecting) {
            hoverCanvas.SetActive(true);
        }
        if (currentHoveringTime > hoverMinimum && objectPlacer.instantiatedPrefab == null) {
            planetRotationControls.CancelInvoke();
            hoverCanvas.SetActive(true);
            Debug.Log("Hovering");
        }
	}

    private void FixedUpdate() {
        if (objectUIPositioner.hitObject == null ) {
            currentGameObject = GetGameObjectWhileHovering(layerMask, sceneCamera);
        }
        if (currentGameObject.activeSelf) {
            ChangeProductionText();
        }
    }

    void ChangeProductionText() {
        float occupanceCurrent = currentGameObject.GetComponent<Occupance>().occupanceAmount;
        if (currentGameObject.tag == "TownHall") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Tree") {
            gemsText.text = "";
            if (((occupanceCurrent * gameManager.woodMultiplier) * timeBetweenTurns) > 0)
                woodText.text = "+ " + Mathf.RoundToInt((occupanceCurrent * gameManager.woodMultiplier) * timeBetweenTurns).ToString();
                else woodText.text = "";
            if (((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns) > 0)
                happinessText.text = "- " + System.Math.Round((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns, 1).ToString() + "%";
                else happinessText.text = "";
            if (((((gameManager.co2Force - 1) / 100f) * gameManager.co2Multiplier) * timeBetweenTurns) > 0)
                natureText.text = "+ " + System.Math.Round(((((gameManager.co2Force - 1) / 100f) * gameManager.co2Multiplier) * timeBetweenTurns / gameManager.trees.Count), 1).ToString() + "%";
                else natureText.text = "";
            powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "House1") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            powerText.text = "";
            if (((1 * gameManager.stats.house1PowerCost / 1f) * timeBetweenTurns) > 0)
                susEnergyText.text = "- " + Mathf.RoundToInt((1 * gameManager.stats.house1PowerCost / 1f) * timeBetweenTurns).ToString();
                else susEnergyText.text = "";
        }
        if (currentGameObject.tag == "House2") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            if ((((gameManager.house2Multiplier * 1) / 100f) * timeBetweenTurns) > 0)
                natureText.text = "- " + System.Math.Round((((gameManager.house2Multiplier * 1) / 100f) * timeBetweenTurns)/gameManager.house2.Count, 1).ToString() + "%";
                else natureText.text = "";
            if (((1 * gameManager.stats.house2PowerCost / 1f) * timeBetweenTurns) > 0)
                powerText.text = "- " + Mathf.RoundToInt((1 * gameManager.stats.house2PowerCost / 1f) * timeBetweenTurns).ToString();
                else powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "House3") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Windmill") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            if ((((1 * gameManager.windmillMultiplier) / 1f) * timeBetweenTurns) > 0)
                powerText.text = "+ " + Mathf.RoundToInt(((1 * gameManager.windmillMultiplier) / 1f) * timeBetweenTurns).ToString();
                else powerText.text = "";
            if (((1 * gameManager.windmillEnergy / 10f) * timeBetweenTurns) > 0)
                susEnergyText.text = "+ " + System.Math.Round((1 * gameManager.windmillEnergy / 10f) * timeBetweenTurns, 1).ToString() + "%";
                else susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Solarflower") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            if (((1 * gameManager.solarPower / 1f) * timeBetweenTurns) > 0)
                powerText.text = "+ " + Mathf.RoundToInt((1 * gameManager.solarPower / 1f) * timeBetweenTurns).ToString();
                else powerText.text = "";
            if (((1 * gameManager.solarEnergy / 10f) * timeBetweenTurns) > 0)
                susEnergyText.text = "+ " + System.Math.Round((1 * gameManager.solarEnergy / 10f) * timeBetweenTurns, 1).ToString() + "%";
                else susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Farm") {
            gemsText.text = "";
            woodText.text = "";
            if (((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns) > 0)
                happinessText.text = "+ " + System.Math.Round((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns, 1).ToString() + "%";
                else happinessText.text = "";
            natureText.text = "";
            if (((1 * gameManager.stats.farmPowerCost / 1f) * timeBetweenTurns) > 0)
                powerText.text = "- " + Mathf.RoundToInt((1 * gameManager.stats.farmPowerCost / 1f) * timeBetweenTurns).ToString();
                else powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Factory") {
            gemsText.text = "";
            woodText.text = "";
            if (((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns) > 0)
                happinessText.text = "- " + System.Math.Round((occupanceCurrent * gameManager.happinessDecreaseMultiplier) * timeBetweenTurns, 1).ToString() + "%";
                else happinessText.text = "";
            if ((((gameManager.factoriesMultiplier * 1) / 100f) * timeBetweenTurns) > 0)
                natureText.text = "- " + System.Math.Round((((gameManager.factoriesMultiplier * 1) / 100f) * timeBetweenTurns)/gameManager.factories.Count, 1).ToString() + "%";
                else natureText.text = "";
            susEnergyText.text = "";
            if (((occupanceCurrent * gameManager.factoryPower / 1f) * timeBetweenTurns) > 0)
                powerText.text = "+ " + Mathf.RoundToInt((occupanceCurrent * gameManager.factoryPower / 1f) * timeBetweenTurns).ToString();
                else powerText.text = "";
        }
        if (currentGameObject.tag == "Mine") {
            if (((occupanceCurrent * gameManager.mineMultiplier) * timeBetweenTurns) > 0)
                gemsText.text = "+ " + Mathf.RoundToInt((occupanceCurrent * gameManager.mineMultiplier) * timeBetweenTurns).ToString();
                else gemsText.text = "";
            woodText.text = "";
            if (((occupanceCurrent * gameManager.happinessDecreaseMultiplier + occupanceCurrent * gameManager.happinessDecreaseMultiplier * 1.3f) * timeBetweenTurns) > 0)
                happinessText.text = "- " + System.Math.Round((occupanceCurrent * gameManager.happinessDecreaseMultiplier + occupanceCurrent * gameManager.happinessDecreaseMultiplier*1.3f) * timeBetweenTurns, 1).ToString() + "%";
                else happinessText.text = "";
            natureText.text = "";
            powerText.text = "";
            susEnergyText.text = "";
        }
        if (currentGameObject.tag == "Seed") {
            gemsText.text = "";
            woodText.text = "";
            happinessText.text = "";
            natureText.text = "";
            powerText.text = "";
            susEnergyText.text = "";
        }
    }
}
