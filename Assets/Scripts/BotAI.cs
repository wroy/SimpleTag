using UnityEngine;
using System.Collections;

// Require these components when using this script
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]

public class BotAI : MonoBehaviour {
	
	public float animSpeed = 1.5f; // a public setting for overall animator animation speed
	public float lookSmoother = 3f; // a smoothing setting for camera motion 
	
	public Transform player; //the player
	public GUIText status; //the red gui text telling the player if they are it
	private int botIt = -1;	//-1 means not it, 1 means it
	
	private Animator anim; // a reference to the animator on the character
	private AnimatorStateInfo currentBaseState; // a reference to the current state of the animator, used for base layer
	private AnimatorStateInfo layer2CurrentState; // a reference to the current state of the animator, used for layer 2

	void Start ()
	{
		anim = GetComponent<Animator>(); // sets the bots animations					  
	}
	
	
	void Update ()
	{
		GetComponent <NavMeshAgent>().destination = player.position * botIt; // makes the bot follow or avoid player
		float h = Input.GetAxis("Horizontal");// setup h variable as our horizontal input axis
		float v = 1.0f;	// setup v variables as our vertical input axis
		anim.SetFloat("Speed", v);// set our animator's float parameter 'Speed' equal to the vertical input axis				
		anim.SetFloat("Direction", h); // set our animator's float parameter 'Direction' equal to the horizontal input axis		
		anim.speed = animSpeed;	// set the speed of our animator to the public variable 'animSpeed'
	}
	
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == "Player") {
			//Debug.Log ("tagged player");
			if (botIt < 0){
				botIt = 1; // bot is now it
				status.enabled = false; // remove gui text telling player they are it
				
			}
			else {
				botIt = -1; //bot is not not it
				status.enabled = true; //tell player they are it
			}	
		}
	}
}
