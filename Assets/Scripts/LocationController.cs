using UnityEngine;
using System.Collections;

public class LocationController : MonoBehaviour
{
	private Vector3 position;
	private GameObject step;


	private Vector3 towardsEast;
	private Vector3 towardsNorth;
	private Vector3 towardsGround;

	// For test purposes, since location not available in Unity
	public float lat;
	public float lng;
	public float alt;
	public float scale; //factor by which distances are divided (if in meters, coordinates reach values in millions which can not be easily trated.
	public bool test;

	public float rotateX = 0;
	public float rotateY = 0;
	public float rotateZ = 0;
	public float rotateForward = 0;

	void Start()
	/// Starts the location tracking from the 1st frame onwards and creates a player
	{
		StartCoroutine(GetGPS());
		step = GameObject.FindGameObjectWithTag("Step");
	}

	void Update()
	{
		step = GameObject.FindGameObjectWithTag("Step");
		position = CoordinatesHandler.LLA2ECEF(lat, lng, alt);
		towardsEast = (CoordinatesHandler.LLA2ECEF(lat, lng + 0.01f, alt) - position).normalized;
		towardsNorth = (CoordinatesHandler.LLA2ECEF(lat + 0.01f, lng, alt) - position).normalized;
		towardsGround = (CoordinatesHandler.LLA2ECEF(lat, lng, alt - 0.01f) - position).normalized;

		//Debug.Log("enable:" + Input.location.isEnabledByUser);


		// Computes the user's position on Earth given by latitude in degrees, longitude in degrees and altitude in meters, to cartesian coordinates and displays it in Unity universe
		if (test) {
			if (lat < step.GetComponent<StepController>().latDest) {
				lat += 0.1f;
			} else {
				lat -= 0.1f;
			}
			if (lng < step.GetComponent<StepController>().lngDest) {
				lng += 0.1f;
			} else {
				lng -= 0.1f;
			}
			if (alt < step.GetComponent<StepController>().altDest) {
				alt += 0.1f;
			} else {
				alt -= 0.1f;
			}
			//Debug.Log("Player position : X : " + position.x + " et Y : " + position.y + "et Z : " + position.z);   
		} else if (Input.location.status == LocationServiceStatus.Running) {
			LocationInfo currentLoc = Input.location.lastData;
			lat = currentLoc.latitude;
			lng = currentLoc.longitude;
			alt = currentLoc.altitude;
			position = CoordinatesHandler.LLA2ECEF(currentLoc.latitude, currentLoc.longitude, currentLoc.altitude);
		}
	}

	private IEnumerator GetGPS()
	/// Retrieve a user's location using his/her phone GPS.
	{
		if (!Input.location.isEnabledByUser) {
			yield break;
		}
		Input.location.Start();
		int maxWait = 30;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		if (maxWait < 1) {
			Debug.Log("Timed out");
			yield break;
		}
		if (Input.location.status == LocationServiceStatus.Failed) {
			Debug.Log("Unable to determine device location");
			yield break;
		} else {
			Debug.Log("Location: " +
				  Input.location.lastData.latitude + " " +
				  Input.location.lastData.longitude + " " +
				  Input.location.lastData.altitude + " " +
				  Input.location.lastData.horizontalAccuracy + " " +
				  Input.location.lastData.timestamp);
		}
		Input.location.Stop();
	}

	//TODO code it Unity way
	public Vector3 GetPosition()
	{
		return position;
	}

	public Vector3 getNorthDirection()
	{
		return towardsNorth;
	}

	public void rotationCorrect()
	{
		transform.RotateAround(transform.position, Vector3.up, rotateY);
		transform.RotateAround(transform.position, Vector3.right, rotateX);
		transform.RotateAround(transform.position, Vector3.forward, rotateZ);
		transform.RotateAround(transform.position, step.GetComponent<StepController>().playerToStepVec(), rotateForward);
	}

	public Vector3 getTowardsEast()
	{
		return towardsEast;
	}

	public Vector3 getTowardsNorth()
	{
		return towardsNorth;
	}

	public Vector3 getTowardsGround()
	{
		return towardsGround;
	}
}
