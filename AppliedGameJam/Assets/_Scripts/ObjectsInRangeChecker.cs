using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInRangeChecker : MonoBehaviour {
    
    public float[] distanceArray;
    public float radius;
    // Use this for initialization
    void Start() {
    }
    


    public List<GameObject> GetObjectsInRange(Vector3 vector3Position, float sphereRadius, string tagString) {
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(vector3Position, sphereRadius);
        List<GameObject> gameObjects = new List<GameObject>();
        for (int i = 0; i < hitColliders.Length; i++) {
            if (hitColliders[i].gameObject.tag == tagString)
            gameObjects.Add(hitColliders[i].gameObject);
        }
        return gameObjects;
    }

    public float[] GetDistances(Vector3 vector3Position, GameObject[] gameObjects) {
        float[] distances = new float[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++) {
            distances[i] = Vector3.Distance(vector3Position, gameObjects[i].transform.position);
        }
        return distances;
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
