using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : ObjectSelecter {
    public Camera cam;
    private GameObject facedGameObject;
    public GameObject prefab;
    public Transform planet;
    private bool canInstantiateObject;
    private bool canPlaceTheObject;
    private RaycastHit hit;

    public Animator animController;

    private GameManager gameManager;
    private Stats stats;
    GameObject instantiatedPrefab;
    private float randomYRotation;
    //Declare
    private bool doOnce;
    private bool hitPlanet;

    [SerializeField] private List<GameObject> buildingPlaceButtons;
    [SerializeField] private bool isUsingButton;

    private PlanetRotationControls planetRotationControls;
    private ObjectsInRangeChecker objectsInrangeChecker;
    public List<GameObject> townHalls;
    public bool townHallInRange;

    private void Start() {
        townHallInRange = false;
        planetRotationControls = GameObject.Find("Planet").GetComponent<PlanetRotationControls>();
        townHalls = new List<GameObject>();
        hitPlanet = false;
        isUsingButton = false;
        canPlaceTheObject = false;
        canInstantiateObject = false;
        gameManager = FindObjectOfType<GameManager>();
        stats = gameManager.GetComponent<Stats>();
        doOnce = true;
        objectsInrangeChecker = gameManager.GetComponent<ObjectsInRangeChecker>();
    }

    private void Update() {
        isUsingButton = IsSelectingAGameObjectInList(buildingPlaceButtons);
        if (instantiatedPrefab != null) {
            townHalls = objectsInrangeChecker.GetObjectsInRange(instantiatedPrefab.transform.position, objectsInrangeChecker.radius-2f, "TownHall");
        }
        

        if (Input.GetButtonUp("Fire1") && !isUsingButton && !canInstantiateObject && hitPlanet && townHalls.Count > 0) { // when placed inside a town hall radius
            Debug.Log("Placed");
            if (instantiatedPrefab.tag == "Windmill")
                instantiatedPrefab.GetComponent<Windmill>().OnAwake();
            else if (instantiatedPrefab.tag == "Seed")
                instantiatedPrefab.GetComponent<Seed>().OnAwake();
            else if (instantiatedPrefab.tag == "House1")
                instantiatedPrefab.GetComponent<House>().OnAwake();
            else if (instantiatedPrefab.tag == "House2")
                instantiatedPrefab.GetComponent<House2>().OnAwake();
            else if (instantiatedPrefab.tag == "House3")
                instantiatedPrefab.GetComponent<House3>().OnAwake();
            else if (instantiatedPrefab.tag == "Farm")
                instantiatedPrefab.GetComponent<Farm>().OnAwake();
            else if (instantiatedPrefab.tag == "Factory")
                instantiatedPrefab.GetComponent<Factory>().OnAwake();
            else if (instantiatedPrefab.tag == "Solarflower")
                instantiatedPrefab.GetComponent<Solarflower>().OnAwake();
            else if (instantiatedPrefab.tag == "TownHall") {
                if ((townHalls.Count > 1) || gameManager.townhall.Count == 0) { //an exception for the first townhall placed and because it needs to ignore its own townhall
                    instantiatedPrefab.GetComponent<TownHall>().OnAwake();
                } else {
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;
                }
            }

            instantiatedPrefab = null;
        } else if ((townHalls.Count <= 0 || (instantiatedPrefab.tag == "TownHall" && townHalls.Count <= 1)) && Input.GetButtonUp("Fire1")) { //When placed ouside a townhall radius
            Destroy(instantiatedPrefab);
            instantiatedPrefab = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        hitPlanet = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        if (hitPlanet) {
            if (canInstantiateObject) {
                facedGameObject = hit.transform.gameObject;
                randomYRotation = Random.Range(0, 360);
                instantiatedPrefab = Instantiate(prefab, hit.point, Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation);
                instantiatedPrefab.transform.SetParent(planet);

                canInstantiateObject = false;
            }
            if (!canPlaceTheObject && instantiatedPrefab != null) {
                planetRotationControls.CancelInvoke();
                //materialController.SetAlpha(instantiatedPrefab, alphaValue, materialWhenPlacing);
                planetRotationControls.rotX = 0;
                planetRotationControls.rotY = 0;
                instantiatedPrefab.transform.position = hit.point;
                instantiatedPrefab.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * Quaternion.Euler(new Vector3(transform.rotation.x, randomYRotation, transform.rotation.z));
            }
        } 
    }

    public void CanPlaceAnObject(GameObject chosenObject) {
        canInstantiateObject = true;
        if (chosenObject.tag == "Windmill" && stats.wood >= stats.windmillWoodCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "Seed" && stats.gem >= stats.seedGemCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "House1" && stats.wood >= stats.house1WoodCost && stats.power >= stats.house1PowerReqCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "House2" && stats.wood >= stats.house2WoodCost && stats.gem >= stats.house2GemCost && stats.power >= stats.house2PowerReqCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "House3" && stats.wood >= stats.house3WoodCost && stats.gem >= stats.house3GemCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "Farm" && stats.wood >= stats.farmWoodCost && stats.power >= stats.farmPowerReqCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "Factory" && stats.wood >= stats.factoryWoodCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "Solarflower" && stats.wood >= stats.solarflowerWoodCost && stats.gem >= stats.solarflowerGemCost)
            prefab = chosenObject;
        else if (chosenObject.tag == "TownHall" && (stats.wood >= stats.townhallWoodCost && stats.gem >= stats.townhallGemCost) || chosenObject.tag == "TownHall" && stats.townhallStarter)
            prefab = chosenObject;
        else
        {
            canInstantiateObject = false;
            if (doOnce)
            {
                StartCoroutine(NoResourcesFadeAnimation());
                doOnce = false;
            }
        }
    }

    public void StopPlacingAnyObject() {
        if (!gameManager.buildFarmButton.activeSelf) {

            //if (instantiatedPrefab.tag == "Windmill")
            //    instantiatedPrefab.GetComponent<Windmill>().DestroyWindmill();
            //else if (instantiatedPrefab.tag == "Seed") {
            //    gameManager = FindObjectOfType<GameManager>();
            //    stats.wood = stats.wood + stats.seedWoodCost;
            //    Destroy(instantiatedPrefab);
            //} else if (instantiatedPrefab.tag == "House1")
            //    instantiatedPrefab.GetComponent<House>().DestroyHouse1();
            //else if (instantiatedPrefab.tag == "House2")
            //    instantiatedPrefab.GetComponent<House2>().DestroyHouse2();
            //else if (instantiatedPrefab.tag == "House3")
            //    instantiatedPrefab.GetComponent<House3>().DestroyHouse3();
            //else if (instantiatedPrefab.tag == "Farm")
            //    instantiatedPrefab.GetComponent<Farm>().DestroyFarm();
            //else if (instantiatedPrefab.tag == "Factory")
            //    instantiatedPrefab.GetComponent<Factory>().DestoryFactory();
            //else if (instantiatedPrefab.tag == "Solarflower")
            //    instantiatedPrefab.GetComponent<Solarflower>().DestroySolarFlower();
            //else if (instantiatedPrefab.tag == "TownHall") {
            //    gameManager.townhall.Remove(instantiatedPrefab);
            //    stats.wood = stats.wood + stats.townhallWoodCost;
            //    stats.gem = stats.gem + stats.townhallGemCost;
            //}

            instantiatedPrefab = null;
            prefab = null;
            GameObject.Destroy(instantiatedPrefab);
        }
    }

    IEnumerator NoResourcesFadeAnimation()
    {
        animController.Play("NoResourcesFade");
        yield return new WaitForSeconds(1.25f);
        doOnce = true;
    }
}
