using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetRotationControls : ObjectSelecter {
    public LayerMask layerMask;
    public LayerMask ignoranceMask;
    public TurnSystem turnSystem;
    [SerializeField]
    private float rotSpeed = 20;

    [SerializeField]
    private float smoothingAfterRot = 7;

    [SerializeField]
    public float rotX;

    [SerializeField]
    public float rotY;

    public Camera cam;
    public bool isControllingRotating;

    [SerializeField]
    public float staticRotationSpeed = 15f;

    [SerializeField]
    public float staticRotationInvokeTime = 6f;

    private Quaternion zoomRotDest;

    [SerializeField]
    private float staticRotationInvokeSmooth = 5f;

    private float staticRotationSpeedCur;

    private float inbetweenClicks;

    [SerializeField]
    private float inbetweenClicksMaxTime;

    private bool doubleClick;
    private bool firstClick;
    public RaycastHit hit;

    public bool canRotateTowardsDestPoint;
    private CameraZoomControls camZoomControls;

    public Vector2 relativeCursorPosition;

    public float yDestinationDirectionValue = 0.4f;


    [SerializeField]
    private float middleScreenZoomOutRadius = 0.05f; // Default: 0.05f

    public bool zoomIn;
    public bool zoomOut;

    public GameObject zoomOutButton;
    public GameObject zoomInButton;

    public List<GameObject> uiGameObjectsToBlockClicks;
    public bool isSelectingUIObject;

    private void Start() {
        isControllingRotating = false;
        staticRotationSpeedCur = 0;
        doubleClick = false;
        firstClick = false;
        canRotateTowardsDestPoint = false;
        camZoomControls = (CameraZoomControls)FindObjectOfType(typeof(CameraZoomControls));
        ZoomOut();
    }


    private void Update() {
        isSelectingUIObject = IsSelectingAGameObjectInList(uiGameObjectsToBlockClicks);
        SphereMouseControl();
        if (isSelectingUIObject) {
            return;
        }
            relativeCursorPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            //if (Vector2.Distance(relativeCursorPosition - new Vector2(0.5f, 0.5f), Vector2.zero) <= middleScreenZoomOutRadius && doubleClick && zoomIn) ZoomOut();
            
        
        DoubleClick();
        if (Input.GetButton("Fire1")) {
            CancelInvoke();
        }
    }
    


    private void FixedUpdate() {
        if (!isControllingRotating) {
            Invoke("StaticRotation", staticRotationInvokeTime);
        } else {
            staticRotationSpeedCur = 0;
            CancelInvoke();
        }
        if (isSelectingUIObject) {
            staticRotationSpeedCur = 0;
            CancelInvoke();
        }
    }


    public void StaticRotation() {
        staticRotationSpeedCur = Mathf.Lerp(staticRotationSpeedCur, staticRotationSpeed, staticRotationInvokeSmooth*Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * staticRotationSpeedCur);
        canRotateTowardsDestPoint = false;
    }

    public void EndTurnRotation()
    {
        staticRotationSpeedCur = Mathf.Lerp(staticRotationSpeedCur, staticRotationSpeed, staticRotationInvokeSmooth * Time.deltaTime);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * staticRotationSpeedCur);
        canRotateTowardsDestPoint = false;
    }


    private void SphereMouseControl() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Drag controls of planet
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && Input.GetButtonDown("Fire1") && !Physics.Raycast(ray, Mathf.Infinity, ignoranceMask) && !isSelectingUIObject) {
            if (hit.transform != null && turnSystem.Turn == TurnSystem.turn.PlayerTurn) {
                isControllingRotating = true;
            }
        }
        if (!Input.GetButton("Fire1")) {
            isControllingRotating = false;
        }
        
        if (isControllingRotating) {
            CancelInvoke();
            rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
            canRotateTowardsDestPoint = false;
        } else{
            if (doubleClick) {
                canRotateTowardsDestPoint = true;
            }
            rotX = Mathf.Lerp(rotX, 0, Time.deltaTime * smoothingAfterRot);
            rotY = Mathf.Lerp(rotY, 0, Time.deltaTime * smoothingAfterRot);
        }
        transform.RotateAround(transform.position, Vector3.up, -rotX);
        transform.RotateAround(transform.position, Vector3.right, rotY);
        
        

        if (hit.transform == null && doubleClick) {
            canRotateTowardsDestPoint = false;
            zoomRotDest = transform.rotation;
            ZoomOut();
        }
        
        //Rotation towards double click point
        if (canRotateTowardsDestPoint) {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && doubleClick && !Physics.Raycast(ray, Mathf.Infinity, ignoranceMask)) {
                Debug.Log(hit.transform.tag);
                    zoomRotDest = Quaternion.FromToRotation(hit.point, Vector3.back + new Vector3 (0, yDestinationDirectionValue, 0)) * transform.rotation;
                    if (zoomOut) {
                        ZoomIn();
                    }
                    CancelInvoke();
                doubleClick = false;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, zoomRotDest, Time.deltaTime * 5f);
            if (doubleClick) {
                ZoomOut();
            }
        }
    }


    private void DoubleClick() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (inbetweenClicks < inbetweenClicksMaxTime && firstClick) {
            if (Input.GetButtonDown("Fire1") && !Physics.Raycast(ray, Mathf.Infinity, ignoranceMask)) {
                doubleClick = true;
            }
        }

        if (Input.GetButtonDown("Fire1") && !Physics.Raycast(ray, Mathf.Infinity, ignoranceMask)) {
            firstClick = true;
        }

        if (firstClick) {
            inbetweenClicks += Time.deltaTime;
        }
        if (inbetweenClicks >= inbetweenClicksMaxTime) {
            inbetweenClicks = 0;
            firstClick = false;
            doubleClick = false;
        }

    }

    public void ZoomOut() {
        camZoomControls.fov = camZoomControls.maxZoom;
        zoomOut = true;
        zoomIn = false;
        zoomOutButton.SetActive(false);
        zoomInButton.SetActive(true);
        doubleClick = false;
    }
    public void ZoomIn() {
        camZoomControls.fov = camZoomControls.minZoom;
        zoomOut = false;
        zoomIn = true;

        zoomOutButton.SetActive(true);
        zoomInButton.SetActive(false);
        doubleClick = false;
    }
}
