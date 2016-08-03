using UnityEngine;
using System.Collections;

public abstract class Projectiles : MonoBehaviour
{
    // Physics
    Rigidbody rbody;
    protected float projectileSpeed;
    public abstract float GetSpeed();

    // Visuals

    // Audio
    public AudioSource weaponSFX;

    /// Use this for initialization
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("A projectile does not have an attached rBody!");
        }
    }

    void Awake()
    {
        projectileSpeed = GetSpeed();
        weaponSFX.Play();
    }

    void FixedUpdate()
    {
        rbody.velocity = transform.forward * projectileSpeed;
    }
}

public class RebLightBlaster : Projectiles
{
    public float speed;
    public AudioClip xWing;

    public override float GetSpeed(){ return speed; }
}
