using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [Header("Move Attributes")]
    [Range(0, 10f)]
    public float moveSpeed = 1f;
    public float range = 5f;
    public float waitSec = 2f;
    public bool leftToRight;
    public bool rightToLeft;
    private bool goTarget = true;

    private bool target;
    private Transform tr;

    Vector3 targetPos;
    Vector3 startPos;

    [Header("Rotate Speed")]
    [Range(-180f, 180f)]
    public float rotateSpeed = 50f;

    Rigidbody rb;
    Vector3 m_EulerAngleVelocity;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        m_EulerAngleVelocity = new Vector3(0, 0, 0);

        tr = this.transform;
        goTarget = true;
        if (rightToLeft == true)
        {
            range = -range;
        }
    }

    void Start()
    {
        targetPos = new Vector3(tr.position.x + range, tr.position.y, tr.position.z);
        startPos = tr.position;

        m_EulerAngleVelocity = new Vector3(0, rotateSpeed, 0);
    }

    void FixedUpdate()
    {
        Move();
        Rotate();        
    }
    private void Move()
    {
        if (goTarget == true)
        {
            StartCoroutine(GoTarget());
        }
        if (target == true)
        {
            tr.position = Vector3.Lerp(tr.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, startPos, moveSpeed * Time.fixedDeltaTime);
        }
    }
    private void Rotate()
    {
        tr.Rotate(rotateSpeed * Time.fixedDeltaTime * Vector3.up);
    }
    IEnumerator GoTarget()
    {
        goTarget = false;
        target = true;
        yield return new WaitForSeconds(waitSec);
        StartCoroutine(GoStart());
    }
    IEnumerator GoStart()
    {
        target = false;
        yield return new WaitForSeconds(waitSec);
        goTarget = true;
    }
}
