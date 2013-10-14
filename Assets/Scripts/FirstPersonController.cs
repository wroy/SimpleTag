using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	
	public float movementSpeed = 8;
	public float lookSpeed = 4;
	float verticalRotation = 0;
	public float verticalLookRange = 60.0f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		//camera 
		
		float rotX = lookSpeed * Input.GetAxis("Mouse X");
		transform.Rotate(0, rotX, 0);
		
		verticalRotation -= lookSpeed * Input.GetAxis ("Mouse Y");
		//float currentVerticalAngle = Camera.main.transform.rotation.eulerAngles.x;
		//float desiredVeritcalAngle = currentVerticalAngle - rotY;
		verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookRange, verticalLookRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
		
		//movement
		float forwardSpeed = movementSpeed * Input.GetAxis("Vertical");
		float sideSpeed = movementSpeed * Input.GetAxis("Horizontal");
		
		Vector3 speed = new Vector3 (sideSpeed, 0,forwardSpeed);
		
		speed = transform.rotation * speed;
		
		CharacterController cc = GetComponent<CharacterController>();
		
		cc.SimpleMove(speed);
	
	}
}
