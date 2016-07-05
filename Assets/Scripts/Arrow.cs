using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private GameObject target;
	private Renderer[] renders;

	public float disapperLength;
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Step");
		renders = GetComponentsInChildren<Renderer>(true);
	}

	// Update is called once per frame
	void Update () {
		target = GameObject.FindGameObjectWithTag("Step");
		Vector3 targetTmp = new Vector3 (target.transform.position.x, 0, target.transform.position.z);
		transform.LookAt(targetTmp);

		// disapper when the distance between arrow and step is close. 
		if ((transform.position-target.transform.position).magnitude < disapperLength) {
			foreach (Renderer rend in renders) {
				rend.enabled = false;
			}
		} else {
			foreach (Renderer rend in renders) {
				rend.enabled = true;
			}
		}
	}
}
