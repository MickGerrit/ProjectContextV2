using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReignSystem : MonoBehaviour {

    //Reference
    private Stats stats;
    public SelectionArrow selectionArrow;

    public Camera cam;
    private GameObject facedGameObject;
    private RaycastHit hit;
    public LayerMask layerMask;
    public Transform planet;
    public GameObject prefab;

    // Use this for initialization
    void Start () {
        stats = GetComponent<Stats>();
	}
	


}
