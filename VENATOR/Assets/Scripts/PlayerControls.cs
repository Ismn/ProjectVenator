using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControls : MonoBehaviour {

    /*******************
     * Weapons Stuff
     *******************/
    // Primary Weapon  
    private Transform activePrimaryGun;
    public List<Transform> primaryGunBattery = new List<Transform>();
    static int primaryBatteryIndex;
    public float primaryRotationSpeed;
    public float primaryFireRate;
    private float primaryLoadRate = 0.5f;

    // Secondary Weapon
    private Transform activeSecondaryGun;
    public List<Transform> secondaryGunBattery = new List<Transform>();
    static int secondaryBatteryIndex;
    public float secondaryRotationSpeed;
    public float secondaryFireRate;
    private float secondaryLoadRate = 0.5f;

    /*******************
     * Ammo Pool
     *******************/
    // Primary Weapon
    private int primaryPool = 12;
    List<GameObject> primaryFire;
    public GameObject primaryAttack;

    // Secondary Weapon
    private int secondaryPool = 12;
    List<GameObject> secondaryFire;
    public GameObject secondaryAttack;

    /*******************
     * Audio
     *******************/
    //AudioSource engineSound;
    //private float pitchMin = 0.95f;
    //private float pitchMax = 1.06f;

    /*******************
     * Physics Model
     *******************/
    Rigidbody shipRBody;

    /*******************
     * Base Velocity
     *******************/
    private float thrustVelocity = 100.0f;
    private float pitchYawVelocity = 34.0f;

    /*******************
    * Directional Inputs
    *******************/
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

        primaryRotationSpeed = primaryRotationSpeed * Time.deltaTime;
        secondaryRotationSpeed = secondaryRotationSpeed * Time.deltaTime;

        //engineSound = GetComponent<AudioSource>(); // Do we have at least one audio source?

        if (GetComponent<Rigidbody>()) // Check we have a rigidbody assigned to the ship.
        {
            shipRBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Ship does not have an attached rBody!"); // Else, throw out an error.
        }
        primaryBatteryIndex = 0;
        secondaryBatteryIndex = 0;
    }

    /// Update is called once per frame
    void Update()
    {
        // Primary Weapon Controls 
        activePrimaryGun = primaryGunBattery[primaryBatteryIndex];
        foreach (Transform pGun in primaryGunBattery)
        {
            pGun.transform.rotation = Quaternion.Slerp(pGun.transform.rotation, Camera.main.transform.rotation, primaryRotationSpeed);

            if (Time.time > primaryLoadRate)
            {
                primaryLoadRate = Time.time + primaryFireRate;

                if (Input.GetButton("LMB"))
                {
                    FirePrimary();
                    primaryBatteryIndex++;
                }
                else
                    primaryBatteryIndex = 0;

                if (primaryBatteryIndex >= primaryGunBattery.Count)
                {
                    primaryBatteryIndex = 0;
                }
            }
        }

        // Secondary Weapon Controls 
        activeSecondaryGun = secondaryGunBattery[secondaryBatteryIndex];
        foreach (Transform sGun in secondaryGunBattery)
        {
            sGun.transform.rotation = Quaternion.Slerp(sGun.transform.rotation, Camera.main.transform.rotation, secondaryRotationSpeed);

            if (Time.time > secondaryLoadRate)
            {
                secondaryLoadRate = Time.time + secondaryFireRate;

                if (Input.GetButton("R"))
                {
                    FireSecondary();
                    secondaryBatteryIndex++;
                }
                else
                    secondaryBatteryIndex = 0;

                if (secondaryBatteryIndex >= secondaryGunBattery.Count)
                {
                    secondaryBatteryIndex = 0;
                }
            }
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
                primaryFire[i].transform.position = activePrimaryGun.position;
                primaryFire[i].transform.rotation = activePrimaryGun.rotation;
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
                secondaryFire[i].transform.position = activeSecondaryGun.position;
                secondaryFire[i].transform.rotation = activeSecondaryGun.rotation;
                secondaryFire[i].SetActive(true);
                break;
            }
        }
    }
}
