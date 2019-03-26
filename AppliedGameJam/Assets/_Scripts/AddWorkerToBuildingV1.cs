using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWorkerToBuildingV1 : ObjectSelecter {

    public bool canSelectWorkerToAdd = false;

    public LayerMask buildingLayer;
    public LayerMask workerLayer;
    public GameObject newWorker;
    public GameObject building;

    public GameObject addWorkerButton;

    public GameObject selectedBuilding;

    public Color disabledButtonColor;
    private Color enabledButtonColor;

    private Transform workerNewTransformPosition;
	// Use this for initialization
	private void Start() {
        enabledButtonColor = addWorkerButton.GetComponent<Image>().color;
    }

	// Update is called once per frame
	void Update () {
        clickedGameObject = GetGameObjectOnClick(layerMask, sceneCamera);

        EnableDisableButton();

        if (Input.GetKeyDown(KeyCode.Space)) {
            SetWorkerPosition(GetGameObjectOnClick(layerMask, sceneCamera), workerNewTransformPosition);
        }

        if (canSelectWorkerToAdd) {
            addWorkerButton.GetComponent<Image>().color = disabledButtonColor;
            if (((1 << clickedGameObject.gameObject.layer) & workerLayer) != 0 && newWorker != clickedGameObject) {
                newWorker = clickedGameObject;
            }
            if (newWorker != null && workerNewTransformPosition != null) {
                Debug.Log("Go to transform");
                SetWorkerPosition(newWorker, workerNewTransformPosition);
                selectedBuilding.GetComponent<Occupance>().occupanceAmount += 1;
                canSelectWorkerToAdd = false;
            }
        } else {
            addWorkerButton.GetComponent<Image>().color = enabledButtonColor;
            newWorker = null;
        }

	}

    public void EnableDisableButton() {
        if (((1 << clickedGameObject.gameObject.layer) & buildingLayer) != 0 && clickedGameObject != null) {
            selectedBuilding = clickedGameObject;
            Debug.Log("WrokerLayer");
        } else {
            newWorker = null;
        }
    }

    public void ButtonToggle() {
        canSelectWorkerToAdd = !canSelectWorkerToAdd;
    }

    public void SetWorkerPosition(GameObject selectableWorker, Transform workerPosition) {
        selectableWorker.transform.position = workerPosition.position;
        selectableWorker.transform.rotation = workerPosition.rotation;
        selectableWorker.GetComponent<Character>().StopAllCoroutines();
        selectableWorker.GetComponent<Animator>().StopPlayback();   
        selectableWorker.GetComponent<Character>().enabled = false;
    }
}
