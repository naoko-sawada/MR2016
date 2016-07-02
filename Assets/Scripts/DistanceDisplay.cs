using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistanceDisplay : MonoBehaviour {

	private jsonDeserializer json;
	private GameObject step;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		step = GameObject.FindGameObjectWithTag("Step");
		StepController sc = step.GetComponent<StepController>();
		json = GameObject.Find("GM").GetComponent<jsonDeserializer>();

		float achiveRate = sc.achivmentRate();
		float distance = json.getAllDistance(); // distance (meter)
		float duration = json.getAllDuration();

		for (int i = 0;i < sc.stepNum - 1 ;i++) {
			distance -= json.getDistance(i);
			duration -= json.getDuration(i);
		}
		distance -= json.getDistance(sc.stepNum) * achiveRate;
		duration -= json.getDuration(sc.stepNum) * achiveRate;

		string distanceText, durationText;
		if (distance >= 1000) {
			distanceText = (distance / 1000).ToString() + "km\n";   // distance (km)
		} else {
			distanceText = distance.ToString() + "m\n";   // distance (km)
		}
		if (duration >= 60) {
			durationText = ((int)duration / 60).ToString() + "min\n";   // duration (min)
		} else {
			durationText = duration.ToString() + "seconds\n";   // duration (second)
		}
		this.GetComponent<Text>().text = distanceText + durationText;
	}
}
