using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveWorkerFromBuildingV1 : ObjectSelecter {

    public bool canSelectWorkerToRemove = false;

    public LayerMask buildingLayer;
    public LayerMask workerLayer;


    public GameObject unassignWorkerFromBuildingButton;

    public GameObject selectedBuilding;
    public GameObject workerToUnassign;

    public Color disabledButtonColor;
    private Color enabledButtonColor;

    // Use this for initialization
    void Start () {
        enabledButtonColor = unassignWorkerFromBuildingButton.GetComponent<Image>().color;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        clickedGameObject = GetGameObjectOnClick(layerMask, sceneCamera);
        if (((1 << clickedGameObject.gameObject.layer) & buildingLayer) != 0) {
            selectedBuilding = clickedGameObject;
        }

        if (canSelectWorkerToRemove) {
            unassignWorkerFromBuildingButton.GetComponent<Image>().color = disabledButtonColor;
            if (((1 << clickedGameObject.gameObject.layer) & workerLayer) != 0) {
                workerToUnassign = clickedGameObject;
                MakeInhabitantWalk(workerToUnassign);
            }
        } else {
            unassignWorkerFromBuildingButton.GetComponent<Image>().color = enabledButtonColor;
        }
    }

    public void CanRemoveWorker() {
        canSelectWorkerToRemove = !canSelectWorkerToRemove;
    }

    private void MakeInhabitantWalk(GameObject inhabitant) {
        inhabitant.GetComponent<Character>().enabled = true;
        inhabitant.GetComponent<Animator>().StartPlayback();
    }
}
