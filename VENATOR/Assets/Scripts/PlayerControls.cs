using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

    // Weapons Stuff
    public GameObject laserBolt;    
    private Transform activeGun;
    public List<Transform> gunBattery = new List<Transform>();
    static int batteryIndexPointer;
    public float rotationSpeed;
    public float fireRate;
    private float loadRate = 0.5f;

    // Ammo Pool
    private int pooledAmount = 12;
    List<GameObject> shots;

    // Audio
    AudioSource shotSound;
    private float pitchMin = 0.95f;
    private float pitchMax = 1.06f;

    // Physics Model
    Rigidbody shipRBody;

    // Base Velocity
    private float thrustVelocity = 100.0f;
    private float pitchYawVelocity = 10.0f;

    // Directional Inputs
    private float thrustInput;
    private float yawInput;
    private float strafeInput;
    private float pitchInput;

    /// Use this for initialization
    void Start ()
    {
        shots = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject o = Instantiate(laserBolt);
            o.SetActive(false);
            shots.Add(o);
        }

        rotationSpeed = rotationSpeed * Time.deltaTime;

        shotSound = GetComponent<AudioSource>(); // Do we have at least one audio source?

        if (GetComponent<Rigidbody>()) // Check we have a rigidbody assigned to the ship.
        {
            shipRBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Ship does not have an attached rBody!"); // Else, throw out an error.
        }
        batteryIndexPointer = 0;
    }

    /// Update is called once per frame
    void Update()
    {
        // Weapon Controls 
        shotSound.pitch = Random.Range(pitchMin, pitchMax); // Allow for a small variance in pitch every time we fire a shot shot.   
        activeGun = gunBattery[batteryIndexPointer];
        foreach (Transform gun in gunBattery)
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Camera.main.transform.rotation, rotationSpeed);

            if (Time.time > loadRate)
            {
               loadRate = Time.time + fireRate;

                if (Input.GetButton("LMB"))
                {
                    FireWeapon();
                    batteryIndexPointer++;
                }
                else
                    batteryIndexPointer = 0;

                if (batteryIndexPointer >= gunBattery.Count)
                {
                    batteryIndexPointer = 0;
                }
            }
        }
        // Debug.Log(batteryIndexPointer);

        // Find the corresponding inputs - See Input Manager for the keybinds to the above axes.
        thrustInput = (Input.GetAxis("Thrust"));
        yawInput = (Input.GetAxis("Yaw"));
        strafeInput = (Input.GetAxis("Strafe"));
        pitchInput = (Input.GetAxis("Pitch"));
    }

    /// FixedUpdate is called once per physics check
    void FixedUpdate ()
    {
        //// Thrust Physics
        shipRBody.AddRelativeForce(Vector3.forward * thrustInput * thrustVelocity);
        //// Yaw Physics
        shipRBody.AddRelativeTorque(0, pitchYawVelocity * yawInput, 0);
        //// Strafe Physics
        shipRBody.AddRelativeForce(Vector3.right * strafeInput * thrustVelocity);
        //// Pitch Physics
        shipRBody.AddRelativeForce(Vector3.up * pitchInput * pitchYawVelocity);
    }

    void FireWeapon()
    {
        for (int i = 0; i < shots.Count; i++)
        {
            if (!shots[i].activeInHierarchy)
            {
                shots[i].transform.position = activeGun.position;
                shots[i].transform.rotation = activeGun.rotation;
                shots[i].SetActive(true);
                shotSound.Play();
                break;
            }
        }
    }
}
