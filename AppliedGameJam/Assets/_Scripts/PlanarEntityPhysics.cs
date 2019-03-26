using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarEntityPhysics : MonoBehaviour {
    private Camera cam;
    [SerializeField]
    private GameObject facedGameObject;
    public LayerMask layerMask;
    [SerializeField]
    private float downwardRayOffset;
    private Transform planetTransform;
    private void Start() {
        planetTransform = GameObject.FindGameObjectWithTag("Planet").transform;
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        transform.rotation = Quaternion.FromToRotation(transform.up, (transform.position - planetTransform.position).normalized) * transform.rotation;
        RaycastHit hit;
        // Does the ray intersect any objects ex\cluding the player layer
        if (Physics.Raycast(transform.position + downwardRayOffset * transform.up, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask)) {
            facedGameObject = hit.transform.gameObject;
            transform.position = hit.point;
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal)*transform.rotation;
        }
        Debug.DrawRay(transform.position + downwardRayOffset * transform.up, transform.TransformDirection(Vector3.down) * 1, Color.blue);
    }
}
