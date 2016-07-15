using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    TestFlight testFlight;

    // Weapons Stuff
    public GameObject laserBolt;
    public Transform blasterSpawn;
    private float loadRate = 0.5f;
    public float fireRate;

    // Audio
    public AudioSource blasterSound;
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;

    // Physics Model
    Rigidbody shipRBody;

    // Base Velocity
    private float thrustVelocity = 100.0f;
    private float pitchVelocity = 10.0f;

    // Directional Inputs
    private float thrustInput;
    private float yawInput;
    private float strafeInput;
    private float pitchInput;

    /// Use this for initialization
    void Start ()
    {
        testFlight = GetComponent<TestFlight>();

        blasterSound = GetComponent<AudioSource>(); // Do we have at least one audio source?

        if (GetComponent<Rigidbody>()) // Check we have a rigidbody assigned to the ship.
        {
            shipRBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Ship does not have an attached rBody!"); // Else, throw out an error.
        }
    }

    /// Update is called once per frame
    void Update()
    {
        blasterSound.pitch = Random.Range(pitchMin, pitchMax); // Allow for a small variance in pitch every time we fire a blaster shot.

        // Weapon Controls
        if (Input.GetButton("LMB") && Time.time > loadRate)
        {
            loadRate = Time.time + fireRate;
            Instantiate(laserBolt, blasterSpawn.position, blasterSpawn.rotation);
            blasterSound.Play();
        }

        // Find the corresponding inputs - See Input Manager for the keybinds to the above axes.
        thrustInput = (Input.GetAxis("Thrust"));
        yawInput = (Input.GetAxis("Yaw"));
        strafeInput = (Input.GetAxis("Strafe"));
        pitchInput = (Input.GetAxis("Pitch"));
    }

    /// FixedUpdate is called once per physics check
    void FixedUpdate ()
    {
        testFlight.Move(0, pitchInput);
        //// Thrust Physics
        shipRBody.AddRelativeForce(Vector3.forward * thrustInput * thrustVelocity);
        //// Yaw Physics
        //shipRBody.AddRelativeTorque(0, yawInput, 0);
        //// Strafe Physics
        //shipRBody.AddRelativeForce(Vector3.right * strafeInput * thrustVelocity);
        //// Pitch Physics
        //shipRBody.AddRelativeForce(Vector3.up * pitchInput * pitchVelocity);
        //shipRBody.AddRelativeTorque(Vector3.left * pitchInput * Time.deltaTime * pitchVelocity);
    }

    void Tracker()
    {
        //Debug.Log(shipRBody.velocity.magnitude);
        //Debug.Log(thrustInput);
        //Debug.Log(yawInput);
        //Debug.Log(strafeInput);
        //Debug.Log(pitchInput);
    }
}
