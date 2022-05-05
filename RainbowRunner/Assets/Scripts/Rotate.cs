using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private enum RotateDir { x, y, z }

    [Header("Rotate Around (x,y,z)")]
    [SerializeField]
    private RotateDir rotateDir;

    [Header("Rotate Speed")]
    [Range(-180f, 180f)]
    public float rotateSpeed = 1f;

    Rigidbody rb;
    Vector3 m_EulerAngleVelocity;
    Transform tr;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();

        m_EulerAngleVelocity = new Vector3(0, 0, 0);
    }

    void Start()
    {
        tr = this.GetComponent<Transform>();
        switch (rotateDir)
        {
            case RotateDir.x:
                m_EulerAngleVelocity = new Vector3(rotateSpeed, 0, 0);
                break;
            case RotateDir.y:
                m_EulerAngleVelocity = new Vector3(0, rotateSpeed, 0);
                break;
            case RotateDir.z:
                m_EulerAngleVelocity = new Vector3(0, 0, rotateSpeed);
                break;
        }
    }

    void FixedUpdate()
    {
        if(rb != null)
        {
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        else
        {
            tr.Rotate(m_EulerAngleVelocity * Time.deltaTime);
        }
    }
}