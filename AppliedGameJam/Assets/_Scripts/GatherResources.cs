using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResources : MonoBehaviour {

    //Reference
    private Stats stats;
    public SelectionArrow selectionArrow;

    public Camera cam;
    private GameObject facedGameObject;
    private RaycastHit hit;
    public LayerMask layerMask;
    public Transform planet;
    public GameObject prefab;
    private bool canGatherWood;

    // Use this for initialization
    void Start () {
        stats = GetComponent<Stats>();
        canGatherWood = true;
	}
}
