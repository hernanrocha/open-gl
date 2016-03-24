using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		//setCount ();
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
	{
		if (!isLocalPlayer)
		return;
		
		float moveHorizontal = 0.0f;
		float moveVertical = 0.0f;

		if (Input.GetKey (KeyCode.A)) {
			moveHorizontal -= 1.0f;
		} else if (Input.GetKey (KeyCode.D)) {
			moveHorizontal += 1.0f;
		}

		if (Input.GetKey (KeyCode.W)) {
			moveVertical += 1.0f;
		} else if (Input.GetKey (KeyCode.S)) {
			moveVertical -= 1.0f;
		}

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("pickup"))
		{
			other.gameObject.SetActive (false);
			count++;
			setCount ();
		}
	}

	void setCount()
	{
		//countText.text = "Count: " + count.ToString ();

		//if (count == 12) {
			//winText.gameObject.SetActive(true);
		//}
	}
}
