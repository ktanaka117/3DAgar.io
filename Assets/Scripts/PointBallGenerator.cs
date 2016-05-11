using UnityEngine;
using System.Collections;

public class PointBallGenerator : MonoBehaviour {

	public GameObject pointBallPrefab;

	public float waitForSeconds;
	public float radius;

	// Use this for initialization
	void Start () {
		StartCoroutine (GenerateByInterval ());
	}
	
	private void Generate() {
		Vector3 randomPosition = Random.insideUnitSphere * radius;

		Vector3 itemPosition = 
			new Vector3 (
				randomPosition.x,
				randomPosition.z,
				0.6f);

		GameObject pointBallInstance = Instantiate (pointBallPrefab);
		Vector3 tempPosition = pointBallInstance.transform.position;
		tempPosition.x = itemPosition.x;
		tempPosition.z = itemPosition.z;
		tempPosition.z = itemPosition.y;

		pointBallInstance.transform.position = tempPosition;
	}

	IEnumerator GenerateByInterval() {
		while (true) {
			Generate ();
			yield return new WaitForSeconds (waitForSeconds);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = new Color (1.0f, 0.0f, 0.0f, 0.5f);
		Gizmos.DrawSphere (transform.position, radius);
	}

}
