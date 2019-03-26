using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectSelecter : MonoBehaviour {
    public Camera sceneCamera;
    public GameObject clickedGameObject;
    public LayerMask layerMask;
    // Use this for initialization
    void Start () {
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

    public GameObject GetGameObjectOnClick(LayerMask layerMask, Camera camera) {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && Input.GetButton("Fire1")) {
            GameObject facedGameObject = hit.transform.gameObject;
            Debug.Log("Shooting raycast");
            return facedGameObject;
        } else return null;
    }

    public GameObject GetGameObjectWhileHovering(LayerMask layerMask, Camera camera) {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) {
            GameObject facedGameObject = hit.transform.gameObject;
            Debug.Log("Shooting raycast");
            return facedGameObject;
        } else return null;
    }

    public bool IsSelectingAGameObjectInList(List<GameObject> gameObjectList) {
        if (EventSystem.current.IsPointerOverGameObject()) {
            if (gameObjectList.Contains(EventSystem.current.currentSelectedGameObject)) {
                return true;
            }
        }
        if (!EventSystem.current.IsPointerOverGameObject() || !gameObjectList.Contains(EventSystem.current.currentSelectedGameObject)) {
            return false;
        }
        return false;
    }
}
