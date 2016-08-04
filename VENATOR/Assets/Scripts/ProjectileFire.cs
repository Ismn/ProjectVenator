using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileFire : MonoBehaviour
{
    Rigidbody rBody;
    public float speed;

    // Use this for initialization
    void Awake()
    {
        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Projectile does not have an attached rBody!");
        }
    }

    void FixedUpdate()
    {
        rBody.velocity = transform.forward * speed;
    }
}