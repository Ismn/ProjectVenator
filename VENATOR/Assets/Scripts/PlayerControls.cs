using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

    // Physics Model
    new Rigidbody rigidbody;

    // Base Velocity
    private float velocity = 12.0f;

    // Directional Inputs
    private float thrustInput;
    private float yawInput;
    private float strafeInput;
    private float pitchInput;


    // Use this for initialization
    void Start ()
    {
        if (GetComponent<Rigidbody>())
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Ship does not have an attahed rBody!");
        }
}

    // Update is called once per frame
    void Update()
    {
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
        rigidbody.AddRelativeForce(Vector3.forward * thrustInput * velocity);
        // Yaw Physics
        rigidbody.AddRelativeTorque(0, yawInput, 0);
        // Strafe Physics
        rigidbody.AddRelativeForce(Vector3.right * strafeInput * velocity);
        // Pitch Physics
        rigidbody.AddRelativeForce(Vector3.up * pitchInput * velocity);
        rigidbody.AddRelativeTorque(Vector3.left * pitchInput);
    }

    void Tracker()
    {
        Debug.Log(rigidbody.velocity.magnitude);
    }
}
