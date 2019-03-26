using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public List<GameObject> trees = new List<GameObject>();
    public List<GameObject> factories = new List<GameObject>();
    public List<GameObject> population = new List<GameObject>();
    public List<GameObject> assignedpopulation = new List<GameObject>();
    public List<GameObject> workertree = new List<GameObject>();
    public List<GameObject> workerfarm = new List<GameObject>();
    public List<GameObject> workerfactory = new List<GameObject>();
    public List<GameObject> workermine = new List<GameObject>();
    public List<GameObject> selection = new List<GameObject>();
    public List<GameObject> windmill = new List<GameObject>();
    public List<GameObject> farm = new List<GameObject>();
    public List<GameObject> house1 = new List<GameObject>();
    public List<GameObject> house2 = new List<GameObject>();
    public List<GameObject> house3 = new List<GameObject>();
    public List<GameObject> solarflower = new List<GameObject>();
    public List<GameObject> townhall = new List<GameObject>();

    //Reference
    private Trees treeList;
    private Stats stats;
    private TurnSystem turnSystem;
    public Text turnCountText;
    public Text maxTurnText;
    public ObjectUIPositioner uiPositioner;
    public PlanetRotationControls planetControls;
    public GameObject buildTreeButton;
    public GameObject buildHouseButton;
    public GameObject buildHouse2Button;
    public GameObject buildHouse3Button;
    public GameObject buildFactoryButton;
    public GameObject buildTownHallButton;
    public GameObject buildWindmillButton;
    public GameObject buildSolarflowerButton;
    public GameObject buildFarmButton;
    public GameObject victoryText;
    public GameObject failText;
    public GameObject gameUI;
    public GameObject tutBuildTownHallUI;
    public GameObject tutBuildStartUI;
    public Mine mine;
    private bool buildButtonBool;
    private bool buildButtonDoOnce;

    public int turnCount;
    public float maxTurns;
    private bool doOnce;
    private bool doOnce2;
    
    [SerializeField]
    private float co2Force;
    public float gameTurnDuration;
    public float gemsEarned = 0f;

    //public Camera cam;
    //public LayerMask layerMask;
    //public RaycastHit hit;
    //public GameObject hitObject;
    //public GameObject prevObject;
    //public GameObject selectionArrow;

    //Notitie
    bool test = (1 == 1);

    //Declare
    public float windmillEnergy;
    public float windmillMultiplier;
    public float solarEnergy;
    public float solarPower;
    public float factoryPower;
    public float co2Multiplier;
    public float woodMultiplier;
    public float factoriesMultiplier;
    public float mineMultiplier;
    public float house2Multiplier;
    public float happinessDecreaseMultiplier;
    public float happinessIncreaseMultiplier;

    public float house2PowerCost;
    // Use this for initialization
    void Start () {
        stats = GetComponent<Stats>();
        treeList = GetComponent<Trees>();
        turnSystem = GetComponent<TurnSystem>();
        maxTurnText.text = maxTurns.ToString();
        co2Force = trees.Count;
        gameTurnDuration = 5f;
        turnCount = 0;
        doOnce = true;
        doOnce2 = true;
        buildButtonBool = true;
        mineMultiplier = 160f;
        woodMultiplier = 1.1f;
        happinessIncreaseMultiplier = .25f;
        happinessDecreaseMultiplier = .1f;

        //Resource Variables
        windmillEnergy = .2f;
        windmillMultiplier = 2.1f;
        solarEnergy = 1f;
        solarPower = .6f;
        factoryPower = .5f;
        factoriesMultiplier = 1f;
        house2Multiplier = 1f;
        co2Multiplier = 1.5f;
}
	
	// Update is called once per frame
	void Update () {

        if (turnSystem.Turn == TurnSystem.turn.GameTurn && doOnce)
        {
            planetControls.ZoomOut();
            uiPositioner.ExitWindow();
            if(buildButtonBool == false)
                OpenBuildButton();
            StartCoroutine(GameTurnCycle());
            doOnce = false;
        }
        if (townhall.Count > 0)
            stats.townhallStarter = false;

        stats.population = population.Count;

        //Tutorial
        if(stats.townhallStarter)
            tutBuildTownHallUI.SetActive(true);
        else
            tutBuildTownHallUI.SetActive(false);

        if (mine.minecartSent == false)
        {

        }
    }

    //Calculate C02
    public void CalculateC02()
    {
        stats.co2 = stats.co2 - (((co2Force - trees.Count) /100f)*co2Multiplier) * Time.deltaTime;
        stats.co2 = stats.co2 - ((factories.Count * factoriesMultiplier) / 100f) * Time.deltaTime;
        stats.co2 = stats.co2 - ((house2.Count * house2Multiplier) / 100f) * Time.deltaTime;
    }

    //Calculate Happiness
    public void CalculateHappiness()
    {
        stats.happiness = stats.happiness - (population.Count * happinessDecreaseMultiplier) * Time.deltaTime;
        stats.happiness = stats.happiness - ((workertree.Count + workerfactory.Count + workermine.Count) * happinessDecreaseMultiplier) * Time.deltaTime;
        stats.happiness = stats.happiness - (workermine.Count * (happinessDecreaseMultiplier*1.3f)) * Time.deltaTime;
        stats.happiness = stats.happiness + (workerfarm.Count * happinessIncreaseMultiplier) * Time.deltaTime;
    }

    //Calculate Wood
    public void CalculateWood()
    {
        stats.wood = stats.wood + (workertree.Count * woodMultiplier)*Time.deltaTime;
    }

    //Calculate Power
    public void CalculatePower()
    {
        stats.power = stats.power + (workerfactory.Count*factoryPower/1f) * Time.deltaTime;
        stats.power = stats.power + ((windmill.Count * windmillMultiplier) / 1f) * Time.deltaTime;
        stats.power = stats.power + (solarflower.Count * solarPower / 1f) * Time.deltaTime;

        if ((stats.power <= stats.house2PowerCost) && house2.Count > 0)
        {
            stats.power = stats.house2PowerCost;
            GameObject DestroyHouse = house2[Random.Range(0, (house2.Count - 1))];
            for (int i = 0; i < DestroyHouse.GetComponentInChildren<Occupance>().occupanceAmount; i++)
            {
                GameObject inhabitant = assignedpopulation[Random.Range(0, assignedpopulation.Count)];
                assignedpopulation.Remove(inhabitant);
                population.Add(inhabitant);
            }
            house1.Remove(DestroyHouse);
            uiPositioner.ExitWindow();
            Destroy(DestroyHouse);
        }
        else
            stats.power = stats.power - (house2.Count * stats.house2PowerCost / 1f) * Time.deltaTime;

        if((stats.power <= stats.house1PowerCost) && house1.Count > 0)
        {
            stats.power = stats.house1PowerCost;
            GameObject DestroyHouse = house1[Random.Range(0, (house1.Count-1))];
            for (int i = 0; i < DestroyHouse.GetComponentInChildren<Occupance>().occupanceAmount; i++)
            {
                GameObject inhabitant = assignedpopulation[Random.Range(0, assignedpopulation.Count)];
                assignedpopulation.Remove(inhabitant);
                population.Add(inhabitant);
            }
            house1.Remove(DestroyHouse);
            uiPositioner.ExitWindow();
            Destroy(DestroyHouse);
        }
        else
            stats.power = stats.power - (house1.Count * stats.house1PowerCost / 1f) * Time.deltaTime;

        if ((stats.power <= stats.farmPowerCost) && farm.Count > 0)
        {
            stats.power = stats.farmPowerCost;
            GameObject DestroyHouse = farm[Random.Range(0, (farm.Count - 1))];
            for (int i = 0; i < DestroyHouse.GetComponentInChildren<Occupance>().occupanceAmount; i++)
            {
                GameObject inhabitant = assignedpopulation[Random.Range(0, assignedpopulation.Count)];
                workerfarm.Remove(inhabitant);
                assignedpopulation.Add(inhabitant);
            }
            farm.Remove(DestroyHouse);
            uiPositioner.ExitWindow();
            Destroy(DestroyHouse);
        }
        else
            stats.power = stats.power - (farm.Count * stats.farmPowerCost / 1f) * Time.deltaTime;
    }

    //Calculate Gem
    public void CalculateGem()
    {
        gemsEarned = stats.gem + (workermine.Count * mineMultiplier) * Time.deltaTime;
    }

    //Calculate Energy
    public void CalculateEnergy()
    {
        stats.energy = stats.energy + (windmill.Count*windmillEnergy / 10f) * Time.deltaTime;
        stats.energy = stats.energy + (solarflower.Count * solarEnergy / 10f) * Time.deltaTime;
    }

    //End Player Turn
    public void EndPlayerTurn()
    {
        turnSystem.Turn = TurnSystem.turn.GameTurn;
    }

    public void OpenBuildButton()
    {
        if (!buildButtonBool && stats.townhallStarter)
            tutBuildStartUI.SetActive(true);

        if (buildButtonBool)
        {
            buildHouseButton.SetActive(true);
            buildHouse2Button.SetActive(true);
            buildHouse3Button.SetActive(true);
            buildTreeButton.SetActive(true);
            buildFactoryButton.SetActive(true);
            buildWindmillButton.SetActive(true);
            buildSolarflowerButton.SetActive(true);
            buildFarmButton.SetActive(true);
            buildTownHallButton.SetActive(true);
            for(int i = 0; i < townhall.Count; i++)
            {
                townhall[i].GetComponentInChildren<Projector>().enabled = true;
            }
            tutBuildStartUI.SetActive(false);
            buildButtonBool = false;          
        }

        else if (!buildButtonBool)
        {
            buildHouseButton.SetActive(false);
            buildHouse2Button.SetActive(false);
            buildHouse3Button.SetActive(false);
            buildTreeButton.SetActive(false);
            buildFactoryButton.SetActive(false);
            buildWindmillButton.SetActive(false);
            buildSolarflowerButton.SetActive(false);
            buildFarmButton.SetActive(false);
            buildTownHallButton.SetActive(false);
            for (int i = 0; i < townhall.Count; i++)
            {
                townhall[i].GetComponentInChildren<Projector>().enabled = false;
            }
            buildButtonBool = true;
        }
    }

    //Game Turn: Fastforward Duration
    IEnumerator GameTurnCycle()
    {
        turnSystem.planetRotationControls.staticRotationSpeed = 300f;
        yield return new WaitForSeconds(gameTurnDuration);
        turnCount += 1;
        turnCountText.text = turnCount.ToString();
        turnSystem.planetRotationControls.staticRotationSpeed = 0;
        yield return new WaitForSeconds(3f);
        turnSystem.Turn = TurnSystem.turn.PlayerTurn;
        stats.wood = Mathf.RoundToInt(stats.wood);
        stats.power = Mathf.RoundToInt(stats.power);
        stats.gem = Mathf.RoundToInt(stats.gem);
        doOnce = true;
    }

    //Game Ending Condition
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Meteor")
        {
            Time.timeScale = 0;
            gameUI.SetActive(false);
            if (stats.energy >= 100)
            {
                victoryText.SetActive(true);
            }
            else
            {
                failText.SetActive(true);
            }
        }
    }

    public void LoseConditions()
    {
        if(stats.happiness < 0 || stats.co2 < 0)
        {
            Time.timeScale = 0;
            gameUI.SetActive(false);
            failText.SetActive(true);
            if (stats.happiness < 0)
                Debug.Log("Happiness Too Low!");
            else if(stats.co2 < 0)
                Debug.Log("CO2 Too Low!");
        }
    }
}
