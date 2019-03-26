using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    //Reference
    public Slider co2Slider;
    public Slider happinessSlider;
    public Slider powerSlider;

    public Slider co2UISlider;
    public Slider happinessUISlider;
    public Slider powerUISlider;
    public Text populationAmount;
    public Text assignedPopulationAmount;
    public Text workersAmount;
    public Text happinessAmount;
    public Text woodAmount;
    public Text powerAmount;
    public Text gemAmount;
    public Text co2Amount;
    public Slider energySlider;
    public Text seedGemCostText;
    public Text house1WoodCostText;
    public Text house1PowerReqText;
    public Text house2WoodCostText;
    public Text house2PowerReqText;
    public Text house2GemCostText;
    public Text house3WoodCostText;
    public Text house3GemCostText;
    public Text townhallWoodCostText;
    public Text townhallGemCostText;
    public Text windmillWoodCostText;
    public Text factoryWoodCostText;
    public Text factoryGemCostText;
    public Text farmWoodCostText;
    public Text farmPowerReqText;
    public Text solarflowerWoodCostText;
    public Text solarflowerGemCostText;
    private GameManager gameManager;

    public float co2;

    //Resources
    public float food;
    public float power;
    public float wood;
    public int population;
    public float happiness;
    public float gem;
    public float energy;

    //Building Cost
    public int seedGemCost = 1;
    public int windmillWoodCost;
    public int house1WoodCost;
    public int house1PowerReqCost = 20;
    public float house1PowerCost = 1f;
    public int house2WoodCost;
    public float house2PowerCost;
    public int house2GemCost;
    public int house2PowerReqCost;
    public int house3WoodCost;
    public int house3GemCost;
    public int farmWoodCost;
    public float farmPowerCost;
    public int farmPowerReqCost = 60;
    public int factoryWoodCost = 30;
    public int factoryGemCost;
    public int solarflowerWoodCost;
    public int solarflowerGemCost;
    public int townhallWoodCost;
    public int townhallGemCost;
    public bool townhallStarter = true;

    public float recycleMultiplier;

    // Use this for initialization
    void Start () {
        co2 = 100f;
        food = 0f;
        power = 0f;
        wood = 0f;
        happiness = 100f;
        gem = 0f;
        energy = 0f;
        recycleMultiplier = .33f;

        house1WoodCost = 30;
        house1PowerCost = 1f;
        house2WoodCost = 65;
        house2PowerCost = 3f;
        house2GemCost = 3;
        house3WoodCost = 75;
        house3GemCost = 10;
        windmillWoodCost = 20;
        townhallWoodCost = 150;
        townhallGemCost = 0;
        farmWoodCost = 50;
        farmPowerCost = 2f;
        factoryWoodCost = 90;
        factoryGemCost = 5;
        solarflowerWoodCost = 70;
        solarflowerGemCost = 15;

        house1PowerReqCost = 1*8;
        house2PowerReqCost = 3*8;
        farmPowerReqCost = 2*8;

        //house1PowerReqCost = 1;
        //house2PowerReqCost = 3;
        //farmPowerReqCost = 2;





        seedGemCostText.text = seedGemCost.ToString("F0");
        house1WoodCostText.text = house1WoodCost.ToString("F0");
        house1PowerReqText.text = house1PowerReqCost.ToString("F0");
        house2WoodCostText.text = house2WoodCost.ToString("F0");
        house2PowerReqText.text = house2PowerReqCost.ToString("F0");
        house2GemCostText.text = house2GemCost.ToString("F0");
        house3WoodCostText.text = house3WoodCost.ToString("F0");
        house3GemCostText.text = house3GemCost.ToString("F0");
        townhallWoodCostText.text = townhallWoodCost.ToString("F0");
        townhallGemCostText.text = townhallGemCost.ToString("F0");
        windmillWoodCostText.text = windmillWoodCost.ToString("F0");
        solarflowerWoodCostText.text = solarflowerWoodCost.ToString("F0");
        solarflowerGemCostText.text = solarflowerGemCost.ToString("F0");
        factoryWoodCostText.text = factoryWoodCost.ToString("F0");
        factoryGemCostText.text = factoryGemCost.ToString("F0");
        farmWoodCostText.text = farmWoodCost.ToString("F0");
        farmPowerReqText.text = farmPowerReqCost.ToString("F0");
        //Initialize
        gameManager = FindObjectOfType<GameManager>();
	}

    void Update()
    {
        energySlider.value = energy / 100;
        co2Slider.value = co2/100;
        happinessSlider.value = happiness/100;
        powerSlider.value = power/100;

        co2UISlider.value = co2 / 100;
        happinessUISlider.value = happiness / 100;
        powerUISlider.value = power / 999 ;
        woodAmount.text = wood.ToString("F0");
        powerAmount.text = power.ToString("F0");
        happinessAmount.text = happiness.ToString("F0");
        populationAmount.text = (gameManager.population.Count + gameManager.assignedpopulation.Count + gameManager.workertree.Count + gameManager.workerfactory.Count + gameManager.workermine.Count + gameManager.workerfarm.Count).ToString();
        assignedPopulationAmount.text = (gameManager.assignedpopulation.Count+ gameManager.workertree.Count + gameManager.workerfactory.Count + gameManager.workermine.Count + gameManager.workerfarm.Count).ToString();
        workersAmount.text = (gameManager.workertree.Count + gameManager.workerfactory.Count + gameManager.workermine.Count + gameManager.workerfarm.Count).ToString();
        gemAmount.text = gem.ToString("F0");
        co2Amount.text = co2.ToString("F0");

        if (co2 < 0)
            co2 = 0;
        if (co2 > 100)
            co2 = 100;
        if (happiness > 100)
            happiness = 100;
        if (power > 999)
            power = 999;
    }
}
