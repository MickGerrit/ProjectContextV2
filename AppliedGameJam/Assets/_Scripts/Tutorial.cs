using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    //Reference
    public List<GameObject> tut = new List<GameObject>();

    public GameObject tutorialScreen;
    public GameObject endturnButton;
    public GameObject buildButton;
    public GameObject statsUI;
    public GameObject infoButton;
    public GameObject happinessUI;
    public GameObject natureUI;
    public GameObject zoominButton;
    public GameObject zoomoutButton;
    public PlanetRotationControls rotationControls;
    public Animator animator1;
    public Animator animator2;
    public Animator animator3;

    //Declare
    private bool tutBool;
    private bool doOnce;
    private bool doThisOnce;
    private bool doOnceAnim1;
    private bool doOnceAnim2;
    private bool doOnceAnim3;
    private bool panControlsDoneBool;
    private bool zoomControlsDoneBool;
    public int currentTut;
    private int mouseClickCount;
    private bool zoomBool;
    private int zoomCount;

	// Use this for initialization
	void Start () {
        tutBool = true;
        doOnce = true;
        doThisOnce = true;
        doOnceAnim1 = true;
        doOnceAnim2 = true;
        doOnceAnim3 = true;
        currentTut = 0;
        endturnButton.SetActive(false);
        buildButton.SetActive(false);
        statsUI.SetActive(false);
        infoButton.SetActive(false);
        happinessUI.SetActive(false);
        natureUI.SetActive(false);
        rotationControls.ZoomOut();
        mouseClickCount = 0;
        zoomBool = true;
        panControlsDoneBool = false;
        zoomControlsDoneBool = false;
        zoomCount = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (tutBool && currentTut < tut.Count)
        {
            if (doThisOnce)
            {
                for (int i = 0; i < tut.Count; i++)
                {
                    tut[i].SetActive(false);
                }
                tut[currentTut].SetActive(true);
                doThisOnce = false;
            }
        }
        else
        {
            for (int i = 0; i < tut.Count; i++)
            {
                tut[i].SetActive(false);
            }
            endturnButton.SetActive(true);
            buildButton.SetActive(true);
            statsUI.SetActive(true);
            infoButton.SetActive(true);
            happinessUI.SetActive(true);
            natureUI.SetActive(true);
            zoominButton.SetActive(true);
            zoomoutButton.SetActive(true);
            rotationControls.enabled = true;
        }

        if(tutBool && currentTut < 3)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
        }
        else if (tutBool && currentTut == 3)
        {
            zoomoutButton.SetActive(true);
            zoominButton.SetActive(true);
            rotationControls.enabled = true;
            if (doOnceAnim1)
            {
                StartCoroutine(Playanimation());
                doOnceAnim1 = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                mouseClickCount += 1;
            }
            if (mouseClickCount > 3 && !panControlsDoneBool)
            {
                StartCoroutine(PanControlsDone());
                panControlsDoneBool = true;
            }
        }
        else if(tutBool && currentTut == 4)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
        }
        else if(currentTut == 5)
        {
            rotationControls.enabled = true;
            if (doOnceAnim2)
            {
                StartCoroutine(Zoomanimation());
                doOnceAnim2 = false;
            }
            if (rotationControls.zoomIn && zoomBool && Input.GetMouseButtonDown(0))
            {
                zoomBool = false;
                zoomCount += 1;
            }
            if (rotationControls.zoomOut && !zoomBool && Input.GetMouseButtonDown(0))
            {
                zoomCount += 1;
            }
            if(zoomCount >= 2 && !zoomControlsDoneBool)
            {
                StartCoroutine(ZoomControlsDone());
                zoomControlsDoneBool = true;
            }
        }
        else if (currentTut == 6)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
        }
        else if (currentTut == 7)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
            if (doOnceAnim3)
            {
                StartCoroutine(Meteoranimation());
                doOnceAnim3 = false;
            }
        }
        else if (currentTut == 9)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = true;
        }
        else if(currentTut == 10)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
        }
        else if (currentTut == 11)
        {
            zoomoutButton.SetActive(false);
            zoominButton.SetActive(false);
            rotationControls.enabled = false;
            buildButton.SetActive(true);
        }
    }


    public void NextPage()
    {
        currentTut += 1;
        doThisOnce = true;

    }

    public void Skip()
    {
        tutBool = false;
    }

    IEnumerator Playanimation()
    {
        animator1.Play("panControls");
        yield return new WaitForSeconds(1f);
        int hash = Animator.StringToHash("panControls");
        animator1.Play(hash, 0, 1f);
    }

    IEnumerator Zoomanimation()
    {
        animator2.Play("zoomControls");
        yield return new WaitForSeconds(1f);
        int hash = Animator.StringToHash("zoomControls");
        animator2.Play(hash, 0, 1f);
    }

    IEnumerator Meteoranimation()
    {
        animator3.Play("meteorControls");
        yield return new WaitForSeconds(1f);
        int hash = Animator.StringToHash("meteorControls");
        animator3.Play(hash, 0, 1f);
    }

    IEnumerator PanControlsDone()
    {
        yield return new WaitForSeconds(2f);
        NextPage();
    }

    IEnumerator ZoomControlsDone()
    {
        yield return new WaitForSeconds(1f);
        NextPage();
    }
}
