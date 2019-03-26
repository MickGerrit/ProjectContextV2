using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenteredObjectChecker : MonoBehaviour {
    private Camera cam;
    [SerializeField]
    private GameObject facedGameObject;
    public LayerMask layerMask;
    private SoundChanger soundChanger;
    public float rayLength;
    private CameraZoomControls camZoom;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        soundChanger = GetComponent<SoundChanger>();
        camZoom = FindObjectOfType<CameraZoomControls>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;

        rayLength = (camZoom.maxZoom - camZoom.fov) * 10;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayLength, layerMask)) {
            facedGameObject = hit.transform.gameObject;
        } else facedGameObject = null;

        //change sound
        if (facedGameObject != null) {
            if (facedGameObject.tag == "Woods") {
                soundChanger.biome = 1;
            } else if (facedGameObject.tag == "Desert") {
                soundChanger.biome = 2;
            } else if (facedGameObject.tag == "Ice") {
                soundChanger.biome = 3;
            }
        } else if (facedGameObject == null) {
            soundChanger.biome = 0;
        }
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.yellow);
    }
}
