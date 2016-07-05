using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextInfoDisplay : MonoBehaviour
{
	private GameObject step;
	private jsonDeserializer json;
	private RectTransform rect;

	// Use this for initialization
	void Start()
	{
		step = GameObject.FindGameObjectWithTag("Step");
		rect = GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		step = GameObject.FindGameObjectWithTag("Step");
		StepController sc = step.GetComponent<StepController>();
		json = GameObject.Find("GM").GetComponent<jsonDeserializer>();

		float achiveRate = sc.achivmentRate();
		//Debug.Log("NextInfo; achiveRate " + achiveRate.ToString());

		float distance = json.getDistance(sc.stepNum) * (1 - achiveRate);   // distance (meter)
		int duration = (int)(json.getDuration(sc.stepNum) * (1 - achiveRate)); // duration (min)

		string distanceText, durationText;
		if (distance >= 1000) {
			distanceText = (distance / 1000).ToString() + "km, ";   // distance (km)
		} else {
			distanceText = distance.ToString() + "m, ";   // distance (km)
		}

		if (duration >= 60) {
			durationText = (duration / 60).ToString() + "min";   // duration (min)
		} else {
			durationText = duration.ToString() + "seconds";   // duration (second)
		}

		this.GetComponent<Text>().text = json.getInstructions(sc.stepNum) + ", " + distanceText + durationText;
		//Debug.Log("NextInfo: distance between steps" + json.getDistanceText(sc.stepNum));
	}
}
