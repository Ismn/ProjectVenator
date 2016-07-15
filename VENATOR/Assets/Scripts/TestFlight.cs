using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TestFlight : MonoBehaviour
{
    public float RollEffect = 50f; // The strength of the effect for rolling.
    public float PitchEffect = 50f; // The strength of the effect for climbing/diving.
    public float AutoTurnPitch = 0.5f; // Value to correct pitch when not climbing/diving.
    public float AutoRollLevel = 0.1f; // Value to correct roll when not rolling.
    public float AutoPitchLevel = 0.1f;

    private float RollAngle;
    private float PitchAngle;
    private float RollInput;
    private float PitchInput;

    private float AeroFactor = 1f;
    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); // Get  reference to our rigidbody.
    }

    public void Move(float rollInput, float pitchInput)
    {
        RollInput = rollInput;
        PitchInput = pitchInput;

        ClampInput();
        CalculateRollAndPitchAngles();
        AutoLevel();
        CalculateTorque();
    }

    void ClampInput()
    {
        RollInput = Mathf.Clamp(RollInput, -1, 1);
        //PitchInput = Mathf.Clamp(PitchInput, -1, 1);
    }

    void CalculateRollAndPitchAngles()
    {
        Vector3 flatForward = transform.forward;
        flatForward.y = 0;
        if (flatForward.sqrMagnitude > 0)
        {
            flatForward.Normalize();
            Vector3 localFlatForward = transform.TransformDirection(flatForward);
            PitchAngle = Mathf.Atan2(localFlatForward.y, localFlatForward.x);

            Vector3 flatRight = Vector3.Cross(Vector3.up, flatForward);

            Vector3 localFlatRight = transform.InverseTransformDirection(flatRight);

            RollAngle = Mathf.Atan2(localFlatRight.y, localFlatRight.z);
        }
    }
    void AutoLevel()
    {
        if (RollInput == 0f)
        {
            RollInput = -RollAngle * AutoRollLevel;
        }
        if (PitchInput == 0f && PitchAngle != 0)
        {
            PitchInput = PitchAngle * AutoPitchLevel;
            PitchInput += Mathf.Abs(AutoTurnPitch);
        }
    }

    void CalculateTorque()
    {
        Vector3 torque = Vector3.zero;
        torque += PitchInput * PitchEffect * - transform.right;
        torque += -RollInput * RollEffect * transform.forward;

        rigidBody.AddRelativeTorque(torque * AeroFactor);
    }
}
