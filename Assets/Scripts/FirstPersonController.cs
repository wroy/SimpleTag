using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class FirstPersonController : MonoBehaviour {
	
	public float movementSpeed = 8;
	public float lookSpeed = 4;
	float verticalRotation = 0;
	public float verticalLookRange = 60.0f;
	float verticalVelocity = 0.0f;
	public float jumpSpeed = 10;
	CharacterController cc;
	public GUIText itStatusText;
	public bool itStatus = true;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		//It status 
		//itStatus = cc.isGrounded;
		//itStatusText.enabled = itStatus;
		
		//camera 
		
		float rotX = lookSpeed * Input.GetAxis("Mouse X");
		transform.Rotate(0, rotX, 0);
		
		verticalRotation -= lookSpeed * Input.GetAxis ("Mouse Y");
		verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookRange, verticalLookRange);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation,0,0);
		
		//movement
		float forwardSpeed = movementSpeed * Input.GetAxis("Vertical");
		float sideSpeed = movementSpeed * Input.GetAxis("Horizontal");
		
		verticalVelocity += 2 * Physics.gravity.y * Time.deltaTime;
		
		if (cc.isGrounded && Input.GetButton("Jump")){ //GetButtonDown is the anti bunny hop
			verticalVelocity = jumpSpeed;
		}
		
		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity,forwardSpeed);
		
		speed = transform.rotation * speed;
		
		cc.Move(speed * Time.deltaTime);
		Debug.Log("I hit something");
	
	}
	
	/*void OnCollisionEnter(Collision col) {
		Debug.Log("I hit something");
		if(col.gameObject.tag == "Enemy") {
			Debug.Log(col.gameObject.tag);
			if (itStatus) {
				itStatus = false;
				itStatusText.enabled = itStatus;
			}
			else {
				itStatus = true;
				itStatusText.enabled = itStatus;
			}
		}
	} */
	
	void OnTriggerEnter( Collider col){
		Debug.Log("Collided");	
		if(col.gameObject.tag == "Enemy") {
			Debug.Log(col.gameObject.tag);
			if (itStatus) {
				itStatus = false;
				itStatusText.enabled = false;
			}
			else {
				itStatus = true;
				itStatusText.enabled = true;
			}
		}
	}
}
