using UnityEngine;
using System.Collections;

public class StepController : MonoBehaviour
{

	private Vector3 position;

	private Vector3 unityPosition;

	private Vector3 relPosition; //relative position to the player;
	//private GameObject player;

	private jsonDeserializer json;

	private bool didCalculateStepdistance; // distance between a step and next step

	public Color stepColor;
	public Color goalColor;

	public int stepNum;
	public float stepDistance;

	//For test purposes
	public float latDest;
	public float lngDest;
	public float altDest;
	public float scale = 1000000;
	public bool test = true;

	void Start()
	{
		GameObject player = GameObject.Find("Player");
		transform.SetParent(player.transform);
		position = CoordinatesHandler.LLA2ECEF(latDest, lngDest, altDest);
		stepNum = 0;
		position = json.updateStep();
		didCalculateStepdistance = false;
		stepColor = new Color(48, 17, 210, 130);
		goalColor = new Color(120, 0, 0,130);

		Renderer[] render = GetComponentsInChildren<Renderer>();
		foreach (Renderer tr in render) {
			//tr.material.color = stepColor;
			tr.material.color = new Color(0, 0, 255, 0.4f);
		}
	}

void Update()
	{
		if (stepNum == 0) {
			json = GameObject.Find("GM").GetComponent<jsonDeserializer>();
			Vector3 locationInfo = json.updateStep();
			latDest = locationInfo.x;
			lngDest = locationInfo.y;
			altDest = locationInfo.z;
			position = CoordinatesHandler.LLA2ECEF(latDest, lngDest, altDest);
			if (!didCalculateStepdistance) {
				Vector3 relPositiontmp = position - GetComponentInParent<LocationController>().GetPosition();
				stepDistance = relPositiontmp.magnitude;
				didCalculateStepdistance = true;
			}
		}

		if (!test) {
			LocationController lc = GetComponentInParent<LocationController>();
            relPosition = position - lc.GetPosition();
			Vector3 relV = relPosition / scale;
            transform.position = new Vector3(Vector3.Dot(relV,lc.getTowardsEast()), Vector3.Dot(relV, lc.getTowardsNorth()), Vector3.Dot(relV, lc.getTowardsGround()));
		} else {
			LocationController lc = GetComponentInParent<LocationController>();
			relPosition = position - lc.GetPosition();
			Vector3 relV = relPosition / scale;
			transform.position = new Vector3(Vector3.Dot(relV, lc.getTowardsEast()), Vector3.Dot(relV, lc.getTowardsNorth()), Vector3.Dot(relV, lc.getTowardsGround()));
			//Debug.Log(transform.position.magnitude);
		}
		//Debug.Log("Step position : X : " + position.x + " et Y : " + position.y + "et Z : " + position.z);
		//Debug.Log("Step relPosition : X : " + relPosition.x + " et Y : " + relPosition.y + "et Z : " + relPosition.z);
	}

	void OnTriggerEnter(Collider col)
	{
		//Debug.Log("this is collision");
		if (col.name == "Player") {
			stepNumIncrement();
		}
	}

	public float achivmentRate()
	{
		float tmp = relPosition.magnitude / stepDistance;
        if (tmp > 1) {
			return 0;
		} else {
			return 1 - tmp;
		}
	}

	public Vector3 playerToStepVec()
	{
		return (position - GetComponentInParent<LocationController>().GetPosition()) / scale;
    }

	public void stepNumIncrement()
	{
		json = GameObject.Find("GM").GetComponent<jsonDeserializer>();

		if (json.isLastStep()) {
			return;
		}
		stepNum++;

		// Update latDest,lngDest, altDest
		Vector3 locationInfo = json.updateStep();
		latDest = locationInfo.x;
		lngDest = locationInfo.y;
		altDest = locationInfo.z;
		position = CoordinatesHandler.LLA2ECEF(latDest, lngDest, altDest);
		Vector3 relPositiontmp = position - GetComponentInParent<LocationController>().GetPosition();
		stepDistance = relPositiontmp.magnitude;

		if (json.isLastStep()) {
			Renderer[] render = GetComponentsInChildren<Renderer>();
			foreach (Renderer tr in render) {
				//tr.material.color = goalColor;
				tr.material.color = new Color(255,0,0,0.4f);
			}
		} else {
			Renderer[] render = GetComponentsInChildren<Renderer>();
			foreach (Renderer tr in render) {
				//tr.material.color = stepColor;
				tr.material.color = new Color(0,0,255,0.4f);
			}
		}

	}

	public void stepNumDecrement()
	{
		if (stepNum == 0) {
			return;
		}

		stepNum--;

		// Update latDest,lngDest, altDest
		json = GameObject.Find("GM").GetComponent<jsonDeserializer>();
		Vector3 locationInfo = json.updateStep();
		latDest = locationInfo.x;
		lngDest = locationInfo.y;
		altDest = locationInfo.z;
		position = CoordinatesHandler.LLA2ECEF(latDest, lngDest, altDest);
		Vector3 relPositiontmp = position - GetComponentInParent<LocationController>().GetPosition();
		stepDistance = relPositiontmp.magnitude;

		Renderer[] render = GetComponentsInChildren<Renderer>();
		foreach (Renderer tr in render) {
			//tr.material.color = stepColor;
			tr.material.color = new Color(0,0,255,0.4f);
        }
	}
}
