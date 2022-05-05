using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent nav;
    private Rigidbody rb;
    [Space]
    public Vector3 startPos;
    [Space]
    public Vector3 speed = new Vector3(0f, 0f, 7f);
    private Animator animator;

    [HideInInspector]
    public bool isFinished;
    private bool movable;

    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        movable = false;
        isFinished = false;
        rb = this.GetComponent<Rigidbody>();
        nav.isStopped = true;
        nav.SetDestination(destination.position);
    }
    private void Update()
    {
        nav.SetDestination(destination.position);
    }
    public void Begin()
    {
        this.transform.position = startPos; 
        isFinished = false;
        movable = true;

        animator.SetBool("IsFinished", isFinished);
        animator.SetBool("Movable", movable);
        nav.isStopped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        nav.isStopped = true;
        if (other.CompareTag("End"))
        {
            isFinished = true;
            movable = false;

            animator.SetBool("IsFinished", isFinished);
            animator.SetBool("Movable", movable);
        }
        else //obstacles
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1f);
            PlayFall();
        }
    }

    public void PlayFall()
    {
        isFinished = false;
        movable = false;

        animator.SetBool("IsFinished", isFinished);
        animator.SetBool("Movable", movable);

        animator.Play("Fall");
        StartCoroutine(WaitTillAnim());
    }

    IEnumerator WaitTillAnim()
    {
        yield return new WaitForSeconds(2f);
        Begin();
    }

    public void Push()
    {
        StartCoroutine(PushedAway());
    }
    IEnumerator PushedAway()
    {
        movable = false;
        nav.isStopped = true;
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("Movable", movable);
        animator.Play("Dizzy");
        yield return new WaitForSeconds(2f);
        movable = true;
        animator.SetBool("Movable", movable);
        nav.isStopped = false;
    }
}
