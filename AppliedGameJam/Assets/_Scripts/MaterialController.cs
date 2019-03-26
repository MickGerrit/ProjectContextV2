using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour {
	
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    SetAlpha(gameobject, Random.Range(0f, 1f), mat);
        //}
    

    //Use unity's standard shader with rendering mode on Fade or Transparent
    public void SetAlpha(GameObject parentGameObject, float alphaValue, Material requestedMaterial) {
        //variables
        bool initialize;
        initialize = true;
        int count;
        float alpha = alphaValue;
        GameObject[] gameObjectsWithRenderer;
        Material[] materials;

        if (alphaValue < 0) {
            alphaValue = 0;
        } else if (alphaValue > 1) {
            alphaValue = 1;
        }

        if (initialize) {
            count = 0;
            foreach (Transform i in parentGameObject.transform) {
                if (i.GetComponent<Renderer>() != null) {
                    count++;
                }
            }

            gameObjectsWithRenderer = new GameObject[count + 1];
            materials = new Material[count + 1];
            count = 0;
            gameObjectsWithRenderer[count] = parentGameObject;

            foreach (Transform i in parentGameObject.transform) {
                if (i.GetComponent<Renderer>() != null) {
                    gameObjectsWithRenderer[count + 1] = i.gameObject;
                }
                count++;
            }

            for (int i = 0; i < materials.Length; i++) {
                if (gameObjectsWithRenderer[i].GetComponent<Renderer>() != null) {
                    materials[i] = new Material(requestedMaterial);
                    gameObjectsWithRenderer[i].GetComponent<Renderer>().material = materials[i];
                    Color color = materials[i].color;
                    color.a = alphaValue;
                    materials[i].color = color;
                    //materials[i].name = color.ToString();
                    materials[i].name = "New Material";
                } else return;
            }

            initialize = false;
        }
    }
}
