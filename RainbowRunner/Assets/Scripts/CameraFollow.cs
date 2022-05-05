using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target; // camera will follow this object
    public Transform camTransform;
    public Vector3 Offset; // Ofseet

    public float SmoothTime = 0.3f; //Smooth Follow

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Offset = camTransform.position - Target.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = Target.position + Offset;
        camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        //transform.LookAt(Target);
    }
}
