using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("Start Position")]
    public Vector3 strtPos;

    [Header("Movement")]
    public float velocity = 7f;

    [Header("Slide")]
    public float sensivity = 0.01f;
    private Vector3 mousePos;
    private Touch touchPos;

    public Rigidbody rb;
    public Animator animator;

    private bool mouseMoving = false;
    private float mouseDrag;
    [HideInInspector]
    public bool isFinished = false;
    private bool movable = false;
    private bool changeCamDir = false;
    public float border = 7f;

    public GameObject paintable;
    public Canvas paintCanvas;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();
        paintable.SetActive(false);
    }

    private void Start()
    {
        rb.freezeRotation = true;
        paintCanvas.enabled = false;
        animator.SetBool("IsFinished", isFinished);
        animator.SetBool("Movable", movable);
    }

    private void Update()
    {     
        if (movable == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocity);
            HoldAndDrag();
        }
        if (changeCamDir == true)
        {
            Camera.main.transform.eulerAngles = Vector3.Lerp(Camera.main.transform.eulerAngles, new Vector3(15, 0, 0), Time.deltaTime);
        }
    }

    private void HoldAndDrag()
    {
        #region Mouse Input
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Input.mousePosition;
            mouseMoving = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseMoving = false;
        }
        if (mouseMoving == true)
        {
            mouseDrag = Input.mousePosition.x - mousePos.x;

            if (transform.position.x > -border && transform.position.x < border)
            {
                transform.position = new Vector3(
                        transform.position.x + (mouseDrag * sensivity * Time.deltaTime),
                        transform.position.y,
                        transform.position.z);
            }
            else if (transform.position.x <= -border)
            {
                if (mouseDrag > 0)
                {
                    transform.position = new Vector3(
                        transform.position.x + (mouseDrag * sensivity * Time.deltaTime),
                        transform.position.y,
                        transform.position.z);
                }
            }
            else if (transform.position.x >= border)
            {
                if (mouseDrag < 0)
                {
                    transform.position = new Vector3(
                        transform.position.x + (mouseDrag * sensivity * Time.deltaTime),
                        transform.position.y,
                        transform.position.z);
                }
            }
        }
        #endregion
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            isFinished = true;
            movable = false;
            changeCamDir = true;

            animator.SetBool("IsFinished", isFinished);
            animator.SetBool("Movable", movable);

            paintCanvas.enabled = true;
            paintable.SetActive(true);
        }
        else //obstacles
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1f);
            PlayFall();
        }
        rb.velocity = Vector3.zero;
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

    public void Begin()
    {
        transform.position = strtPos;
        isFinished = false;
        movable = true;

        animator.SetBool("IsFinished", isFinished);
        animator.SetBool("Movable", movable);

        rb.velocity = new Vector3(0, 0, velocity);
    }

    public void Push()
    {
        StartCoroutine(PushedAway());
    }
    IEnumerator PushedAway()
    {
        movable = false;
        rb.velocity = new Vector3(0, 0, 0);
        animator.SetBool("Movable", movable);
        animator.Play("Dizzy");
        yield return new WaitForSeconds(2f);
        movable = true;
        animator.SetBool("Movable", movable);
    }

}
