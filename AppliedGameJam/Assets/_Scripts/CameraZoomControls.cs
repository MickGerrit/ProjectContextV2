using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControls : MonoBehaviour {

    private Vector3 addToPosition;

    [SerializeField]
    private float zoomSensitivity = 10;

    [SerializeField]
    private float smoothing = 4;

    [SerializeField]
    public float FOV { get {
            return fov;
        } set {
            fov = value;
            if (fov >= maxZoom) fov = maxZoom;
            if (fov <= minZoom) fov = minZoom;
        }
    }
    public float fov;

    private Camera cam;
    
    public float minZoom = 10f;
    
    public float maxZoom = 60f;


    private void Start() {
        addToPosition = Vector3.zero;
        cam = GetComponent<Camera>();
        FOV = cam.fieldOfView;
    }

    private void Update() {
        //float addToPosition = Input.mouseScrollDelta.y * zoomSensitivity;
        //FOV -= addToPosition * Time.deltaTime;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FOV, Time.deltaTime * smoothing);
    }
}
