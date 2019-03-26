using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInformationScreen : MonoBehaviour {
    public GameObject[] objectsToToggle;
    public void Toggle() {
        for(int o = 0; o < objectsToToggle.Length; o++) {
            objectsToToggle[o].SetActive(!objectsToToggle[0].activeSelf);
        }
    }
}
