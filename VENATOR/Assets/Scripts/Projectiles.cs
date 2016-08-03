using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

    Rigidbody rbody;
    public float speed;

    /// Use this for initialization
    void Awake()
    {
        if (GetComponent<Rigidbody>())
        {
            rbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Projectile does not have an attached rBody!");
        }       
    }

    void FixedUpdate()
    {
        rbody.velocity = transform.forward * speed;
    }
}
