using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

    Rigidbody rbody;
    public float speed;

    /// Use this for initialization
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Projectile does not have an attached rBody!");
        }

        rbody.velocity = transform.forward * speed;        
    }
}
