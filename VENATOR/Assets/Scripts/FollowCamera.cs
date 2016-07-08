using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    private float turnSpeed = 5.0f;
    public Transform playerShip;
    private Vector3 offset;
    private Vector3 offX;
    private Vector3 offY;

    // Use this for initialization
    void Start () {
        offset = new Vector3(playerShip.position.x, playerShip.position.y + 3.0f, playerShip.position.z - 10.0f);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        offX = (Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset);
        offY = (Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset);
        //offset = (Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset);
        transform.position = playerShip.position + offX;
        transform.LookAt(playerShip.position);
	}
}
