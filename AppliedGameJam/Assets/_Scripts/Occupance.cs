using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Occupance : MonoBehaviour {
    [TextArea]
    public string informationAboutBuilding;
    public int maximumOccupanceAmount;
    public int occupanceAmount;
    public Sprite sprite;
    public bool isOverlapping;

    public bool canAssign;

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Tree" || other.tag == "TownHall" || other.tag == "House1" || other.tag == "House2" || other.tag == "House3" ||
            other.tag == "Windmill" || other.tag == "Solarflower" || other.tag == "Farm" || other.tag == "Factory" || other.tag == "Mine" ||
            other.tag == "Seed" || other.tag == "Water") {
            isOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Tree" || other.tag == "TownHall" || other.tag == "House1" || other.tag == "House2" || other.tag == "House3" ||
    other.tag == "Windmill" || other.tag == "Solarflower" || other.tag == "Farm" || other.tag == "Factory" || other.tag == "Mine" ||
    other.tag == "Seed" || other.tag == "Water") {
            isOverlapping = false;

        }
    }



}
