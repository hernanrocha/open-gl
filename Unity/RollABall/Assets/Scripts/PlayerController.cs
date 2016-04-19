using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	// Use this for initialization
	void Start () {
		Instantiate (countText);
		Instantiate (winText);

		rb = GetComponent<Rigidbody> ();
		count = 0;
		setCount ();
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

		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
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
		countText.text = "Count: " + count.ToString ();

		if (count == 12) {
			winText.gameObject.SetActive(true);
		}
	}

	[Command]
	void CmdFire()
	{
		// create the bullet object from the bullet prefab
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position - transform.forward,
			Quaternion.identity);

		// make the bullet move away in front of the player
		bullet.GetComponent<Rigidbody>().velocity = -transform.forward*4;

		// spawn the bullet on the clients
		NetworkServer.Spawn(bullet);

		// make bullet disappear after 2 seconds
		Destroy(bullet, 2.0f);
	}
}
