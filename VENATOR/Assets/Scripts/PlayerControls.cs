using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    public GameObject laserBolt;
    public Transform blasterSpawn;
    private float loadRate = 0.5f;
    public float fireRate;

    public AudioSource blasterSound;
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;

    // Physics Model
    Rigidbody shipRBody;

    // Base Velocity
    private float velocity = 100.0f;

    // Directional Inputs
    private float thrustInput;
    private float yawInput;
    private float strafeInput;
    private float pitchInput;


    // Use this for initialization
    void Start ()
    {
        blasterSound = GetComponent<AudioSource>();

        if (GetComponent<Rigidbody>())
        {
            shipRBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Ship does not have an attached rBody!");
        }

        //audio.pitch = Random.Range(pitchMin, pitchMax);
    }

    // Update is called once per frame
    void Update()
    {
        blasterSound.pitch = Random.Range(pitchMin, pitchMax);

        // Weapon Controls
        if (Input.GetButton("LMB") && Time.time > loadRate)
        {
            loadRate = Time.time + fireRate;
            Instantiate(laserBolt, blasterSpawn.position, blasterSpawn.rotation);
            blasterSound.Play();
        }

        // Find the corresponding inputs
        thrustInput = (Input.GetAxis("Thrust"));
        yawInput = (Input.GetAxis("Yaw"));
        strafeInput = (Input.GetAxis("Strafe"));
        pitchInput = (Input.GetAxis("Pitch"));
    }

    // FixedUpdate is called once per physics check
    void FixedUpdate ()
    {
        // Thrust Physics
        shipRBody.AddRelativeForce(Vector3.forward * thrustInput * velocity);
        // Yaw Physics
        shipRBody.AddRelativeTorque(0, yawInput, 0);
        // Strafe Physics
        shipRBody.AddRelativeForce(Vector3.right * strafeInput * velocity);
        // Pitch Physics
        shipRBody.AddRelativeForce(Vector3.up * pitchInput * velocity);
        shipRBody.AddRelativeTorque(Vector3.left * pitchInput);
    }

    void Tracker()
    {
        Debug.Log(shipRBody.velocity.magnitude);
    }
}
