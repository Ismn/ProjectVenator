using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    public Transform target;
    public float distance = 4.0f;
    private float xSpeed = 50.0f;
    private float ySpeed = 50.0f;

    private float zSpeed = 40.0f;    

    private float yMinLimit = -75.0f;
    private float yMaxLimit = 68.0f;

    private float distanceMin = 2.0f;
    private float distanceMax = 10.0f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 2.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                //Debug.Log("Ray Hit!");
                distance = Mathf.Lerp(distance, hit.distance, Time.deltaTime * zSpeed);
            }            

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}