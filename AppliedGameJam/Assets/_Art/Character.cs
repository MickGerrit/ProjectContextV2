using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Animator CharacterAnimator;
    public int Action;
    public int ColliderAction;
    bool jump;
    public float speed = 0.2f;
    public LayerMask layerMask;
    public float rayInbetweenOffset;
    public float rayXPosition;
    public float sideRayLength = 0.1f;
    public float forwardRayLength = 0.1f;

    private Vector3 velocity;
    private Quaternion wantedRot;

    protected RaycastHit fHit;

    private Vector3 rayXPositionNew;

    // Use this for initialization
    void OnEnable()
    {
        jump = false;
        ChooseAction();
        wantedRot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        transform.rotation = wantedRot;
    }

    // Update is called once per frame
    void ChooseAction()
    {
        if (jump == false)
        {
            Action = Mathf.RoundToInt(Random.Range(0, 3));
            ChangeAction();
        }
        if (jump == true)
        {
            Action = 3;
            ChangeAction();
        }
    }

    void ChangeAction()
    {

        if (Action == 0)
        {
            //Debug.Log("Standing");
            CharacterAnimator.SetBool("Standing", true);
            StartCoroutine(StandTimer());
        }
        if (Action == 1)
        {
            //Debug.Log("Walking");
            CharacterAnimator.SetBool("Walking", true);
            transform.rotation = wantedRot;
            StartCoroutine(WalkTimer());

        }
        if (Action == 2)
        {
            //Debug.Log("Waving");
            CharacterAnimator.SetBool("Waving", true);
            StartCoroutine(WaveTimer());
        }
        if (Action == 3)
        {
            //Debug.Log("Jumping");
            CharacterAnimator.SetBool("Jumping", true);
            StartCoroutine(JumpTimer());
        }
    }

    IEnumerator StandTimer()
    {
        yield return new WaitForSeconds(3f);
        CharacterAnimator.SetBool("Standing", false);
        ChooseAction();
    }

    IEnumerator WalkTimer()
    {
        yield return new WaitForSeconds(3f);
        CharacterAnimator.SetBool("Walking", false);
        ChooseAction();
    }

    IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(3f);
        CharacterAnimator.SetBool("Waving", false);
        ChooseAction();
    }

    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(2f);
        CharacterAnimator.SetBool("Jumping", false);
        jump = false;
        ChooseAction();
    }

    private void Update() {
        velocity = transform.forward * Time.deltaTime * speed;
    }

    private void FixedUpdate()
    {
        if (CharacterAnimator.GetBool("Walking"))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }

        //Debug.Log(WallCheck());
        // Does the ray intersect any objects ex\cluding the player layer
        if (WallCheck() && !jump) {
            HitWall();
            Vector3 newDir;
            newDir = Vector3.Reflect(velocity, fHit.normal);
            wantedRot = Quaternion.LookRotation(newDir);
        }
    }

    void HitWall() {
        Debug.Log("Hit Collider");
        CharacterAnimator.SetBool("Walking", false);
        //transform.position -= transform.forward * 0.01f
        jump = true;
    }

    private bool WallCheck() {
        RaycastHit lHit;
        RaycastHit rHit;
        rayXPositionNew = rayXPosition * transform.up;
        //Check if the forward and one of the side rays hit something at the same time
        /*if (Physics.Raycast(transform.position + rayInbetweenOffset * transform.forward + rayXPositionNew, transform.TransformDirection(Vector3.forward), out fHit, Mathf.Infinity, layerMask)) {
            if (Physics.Raycast(transform.position + rayInbetweenOffset * -transform.right + rayXPositionNew, transform.TransformDirection(Vector3.left), out lHit, sideRayLength, layerMask)) {
                return true;
            } else if (Physics.Raycast(transform.position + rayInbetweenOffset * transform.right + rayXPositionNew, transform.TransformDirection(Vector3.right), out rHit, sideRayLength, layerMask)) {
                return true;
            }
        } */
        if (Physics.Raycast(transform.position + rayInbetweenOffset * -transform.right + rayXPositionNew, transform.TransformDirection(Vector3.forward), out fHit, forwardRayLength, layerMask)) {
            return true;
        } else if (Physics.Raycast(transform.position + rayInbetweenOffset * transform.right + rayXPositionNew, transform.TransformDirection(Vector3.forward), out fHit, forwardRayLength, layerMask)) {
            return true;
        } else return false;
    }

    private void OnDrawGizmos() {
        //Debug.DrawLine(transform.position + rayInbetweenOffset * -transform.right + rayXPositionNew, transform.TransformDirection(Vector3.left) * sideRayLength + transform.position + rayInbetweenOffset*-transform.right + rayXPositionNew, Color.red);
        //Debug.DrawLine(transform.position + rayInbetweenOffset * transform.right + rayXPositionNew, transform.TransformDirection(Vector3.right) * sideRayLength + transform.position + rayInbetweenOffset * transform.right + rayXPositionNew, Color.red);
        Debug.DrawLine(transform.position + rayInbetweenOffset * -transform.right + rayXPositionNew, 
            transform.TransformDirection(Vector3.forward) * forwardRayLength + transform.position + rayInbetweenOffset * transform.forward + rayXPositionNew + rayInbetweenOffset * -transform.right, Color.red);
        Debug.DrawLine(transform.position + rayInbetweenOffset * transform.right + rayXPositionNew, 
            transform.TransformDirection(Vector3.forward) * forwardRayLength + transform.position + rayInbetweenOffset * transform.forward + rayXPositionNew + rayInbetweenOffset * transform.right, Color.red);
    }

}