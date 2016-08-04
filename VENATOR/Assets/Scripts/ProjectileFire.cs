using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileFire : MonoBehaviour
{
    // Audio
    public AudioSource shotSound;
    private float pitchMin = 0.95f;
    private float pitchMax = 1.06f;

    // Shot Physics
    Rigidbody rBody;
    public float speed;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Projectile does not have an attached rBody!");
        }

        shotSound = GetComponent<AudioSource>(); // Do we have at least one audio source?
    }

    void Awake()
    {
        shotSound.pitch = Random.Range(pitchMin, pitchMax); // Allow for a small variance in pitch every time we fire a shot shot.
        shotSound.Play();
    }

    void FixedUpdate()
    {
        rBody.velocity = transform.forward * speed;
    }
}