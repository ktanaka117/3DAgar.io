using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float movementSpeed;
	public float rotateSpeed;

	private int score = 0;
	public Text scoreText;

	private bool shouldDivide = false;
	public GameObject playerClonePrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.W)) {
			transform.position += transform.forward * movementSpeed;
		}

		if (Input.GetKey (KeyCode.S)) {
			transform.position -= transform.forward * movementSpeed;
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.up, -rotateSpeed);
		}

		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.up, rotateSpeed);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (transform.localScale.x / 2 > 1.0f) {
				shouldDivide = true;
			}
		}
	}

	void FixedUpdate() {
		if (shouldDivide) {
			Divide ();
		}
	}

	private void Divide() {
		GameObject clone = Instantiate (playerClonePrefab, transform.position + transform.forward*2f, transform.localRotation) as GameObject;
		clone.transform.localScale = gameObject.transform.localScale / 2;

		ToHalfScale (clone);
		ToHalfScale (gameObject);

		FireClone (clone);

		shouldDivide = false;

		DeleteCamera (clone);
	}

	private void ToHalfScale(GameObject obj) {
		obj.transform.localScale /= 2;
	}

	private void FireClone(GameObject clone) {
		clone.GetComponent<Rigidbody> ().AddForce (transform.position + transform.forward*100f , ForceMode.Impulse);
	}

	void DeleteCamera(GameObject obj) {
		Camera camera = obj.transform.GetComponentInChildren<Camera> ();
		if (camera != null) {
			Destroy (camera.gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		assimilateOther (other);
		score++;
		scoreText.text = "Score: " + score;

		Destroy (other.gameObject);
	}

	void assimilateOther(Collider other) {
		transform.localScale += other.transform.localScale;
	}
}
