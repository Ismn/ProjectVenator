using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

    // Weapons Stuff    
    private Transform activeGun;
    public List<Transform> gunBattery = new List<Transform>();
    static int batteryIndexPointer;
    public float rotationSpeed;
    public float fireRate;
    private float loadRate = 0.5f;

    // Ammo Pool
    /// Primary Weapon
    private int primaryPool = 12;
    List<GameObject> primaryFire;
    public GameObject primaryAttack;
    /// Secondary Weapon
    private int secondaryPool = 12;
    List<GameObject> secondaryFire;
    public GameObject secondaryAttack;

    // Audio
    //AudioSource engineSound;
    //private float pitchMin = 0.95f;
    //private float pitchMax = 1.06f;

    // Physics Model
    Rigidbody shipRBody;

    // Base Velocity
    private float thrustVelocity = 100.0f;
    private float pitchYawVelocity = 34.0f;

    // Directional Inputs
    private float thrustInput;
    private float yawInput;
    private float strafeInput;
    private float pitchInput;

    /// Use this for initialization
    void Start ()
    {
        primaryFire = new List<GameObject>();
        for (int i = 0; i < primaryPool; i++)
        {
            GameObject o = Instantiate(primaryAttack);
            o.SetActive(false);
            primaryFire.Add(o);
        }

        secondaryFire = new List<GameObject>();
        for (int i = 0; i < secondaryPool; i++)
        {
            GameObject o = Instantiate(secondaryAttack);
            o.SetActive(false);
            secondaryFire.Add(o);
        }

        rotationSpeed = rotationSpeed * Time.deltaTime;

        //engineSound = GetComponent<AudioSource>(); // Do we have at least one audio source?

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
        activeGun = gunBattery[batteryIndexPointer];
        foreach (Transform gun in gunBattery)
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation, Camera.main.transform.rotation, rotationSpeed);

            if (Time.time > loadRate)
            {
                loadRate = Time.time + fireRate;

                if (Input.GetButton("LMB"))
                {
                    FirePrimary();
                    batteryIndexPointer++;
                }
                else {
                batteryIndexPointer = 0;
                }

                if (Input.GetButton("RMB"))
                {
                    FireSecondary();
                    batteryIndexPointer++;
                }
                else {
                    batteryIndexPointer = 0;
                }

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
        /// Thrust Physics
        shipRBody.AddRelativeForce(Vector3.forward * thrustInput * thrustVelocity);
        /// Yaw Physics
        shipRBody.AddRelativeTorque(0, pitchYawVelocity * yawInput, 0);
        /// Strafe Physics
        shipRBody.AddRelativeForce(Vector3.right * strafeInput * thrustVelocity);
        /// Pitch Physics
        shipRBody.AddRelativeForce(Vector3.up * pitchInput * pitchYawVelocity);
    }

    void FirePrimary()
    {
        for (int i = 0; i < primaryFire.Count; i++)
        {
            if (!primaryFire[i].activeInHierarchy)
            {
                primaryFire[i].transform.position = activeGun.position;
                primaryFire[i].transform.rotation = activeGun.rotation;
                primaryFire[i].SetActive(true);
                break;
            }
        }
    }

    void FireSecondary()
    {
        for (int i = 0; i < secondaryFire.Count; i++)
        {
            if (!secondaryFire[i].activeInHierarchy)
            {
                secondaryFire[i].transform.position = activeGun.position;
                secondaryFire[i].transform.rotation = activeGun.rotation;
                secondaryFire[i].SetActive(true);
                break;
            }
        }
    }
}
