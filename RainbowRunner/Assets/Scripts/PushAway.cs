using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAway : MonoBehaviour
{
    [Header("Push Attributes")]
    public float pushForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.Push();
            }
            else if(other.TryGetComponent(out AI ai))
            {
                ai.Push();
            }
            rb.AddForce(Vector3.back * pushForce, ForceMode.Impulse);
        }
        else
            print("No push");
    }
}
