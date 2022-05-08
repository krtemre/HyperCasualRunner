using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Transform destination;
    public List<Transform> Targets;
    public float targetOfsets;
    private int targetCount = 0;

    private NavMeshAgent nav;
    private Rigidbody rb;
    [Space]
    public Vector3 startPos;
    [Space]
    public float speed = 5f;
    public float navSpeed = 2f;

    private Animator animator;

    [HideInInspector]
    public bool isFinished;
    private bool movable;

    void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

        movable = false;
        isFinished = false;
        nav.speed = 0f;
    }
    private void Update()
    {
        if (movable == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
            nav.speed = navSpeed;
            SetTarget();
        }
    }

    public void SetTarget()
    {
        if(transform.position.z > Targets[targetCount].position.z - 1f || targetCount >= Targets.Count)
        {
            if (targetCount + 1 >= Targets.Count)
                nav.SetDestination(destination.position);
            else
            {
                targetOfsets *= Random.Range(-1, 2);
                targetCount++;
                nav.SetDestination(Targets[targetCount].position + Vector3.right * targetOfsets);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        nav.speed = 0f;
        movable = false;
        animator.SetBool("Movable", movable);
        nav.enabled = false;
        if (other.CompareTag("End")) 
        {
            isFinished = true;
            animator.SetBool("IsFinished", isFinished);
        }
        else //obstacles
        { 
            this.transform.position += Vector3.back;
            animator.Play("Fall");
            StartCoroutine(WaitTillAnim());
        }
    }

    IEnumerator WaitTillAnim()
    {
        yield return new WaitForSeconds(2f);
        this.transform.position = startPos;
        Begin();
    }
    public void Begin()
    {
        transform.position = startPos;
        nav.enabled = true;
        targetCount = 0;
        movable = true;
        animator.SetBool("Movable", movable);

        targetOfsets *= Random.Range(-1, 2);
        nav.SetDestination(Targets[0].position + (Vector3.right * targetOfsets));
    }

    public void Push()
    {
        StartCoroutine(PushedAway());
    }
    IEnumerator PushedAway()
    {
        nav.isStopped = true;
        movable = false;
        rb.velocity = Vector3.zero;

        animator.SetBool("Movable", movable);
        animator.Play("Dizzy");

        yield return new WaitForSeconds(2f);

        movable = true;
        animator.SetBool("Movable", movable);
        nav.isStopped = false;
    }
}
