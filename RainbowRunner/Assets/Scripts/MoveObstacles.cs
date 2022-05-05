using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacles : MonoBehaviour
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

    private void Awake()
    {
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
    }

    void FixedUpdate()
    {
        if (goTarget == true)
        {
            StartCoroutine(GoTarget());
        }
        if(target == true)
        {
            tr.position = Vector3.Lerp(tr.position, targetPos, moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, startPos, moveSpeed * Time.fixedDeltaTime);
        }
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