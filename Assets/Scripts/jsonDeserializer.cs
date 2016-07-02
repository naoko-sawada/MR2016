using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;




public class jsonDeserializer : MonoBehaviour
{
	RootObject obj;
	private GameObject step;

	public static readonly float ALT = 1.0f; 


	void Start()
	{
		step = GameObject.FindGameObjectWithTag("Step");
	}
	void Update()
	{
		step = GameObject.FindGameObjectWithTag("Step");
	}

	public void Program(WWW www)
	{
		obj = JsonConvert.DeserializeObject<RootObject>(www.text);

		//Example of a value, could get and use anything
		//position = CoordinatesHandler.LLA2ECEF(obj.Routes[0].legs[0].steps[1].start_location.lat, obj.Routes[0].legs[0].steps[1].start_location.lng, 0);

		//Debug.Log("Distance: " + obj.Routes[0].legs[0].steps[0].start_location.lat);
	}

	public Vector3 updateStep()
	{
		StepController sc = step.GetComponent<StepController>();
		return new Vector3(float.Parse(obj.Routes[0].legs[0].steps[sc.stepNum].end_location.lat),
			float.Parse(obj.Routes[0].legs[0].steps[sc.stepNum].end_location.lng), ALT);
	}

	public string getAllDistanceText()
	{
		return obj.Routes[0].legs[0].distance.text;
    }

	public float getAllDistance()
	{
		return float.Parse(obj.Routes[0].legs[0].distance.val);
	}

	public string getAllDureationText()
	{
		return obj.Routes[0].legs[0].duration.text;
	}

	public float getAllDuration()
	{
		return float.Parse(obj.Routes[0].legs[0].duration.val);
	}

	public string getNextInfo()
	{
		StepController sc = step.GetComponent<StepController>();
		return obj.Routes[0].legs[0].steps[sc.stepNum].html_instructions + "\n"
			+ obj.Routes[0].legs[0].steps[sc.stepNum].distance.text + " "
			+ obj.Routes[0].legs[0].steps[sc.stepNum].duration.text;
    }

	public string getInstructions(int stepNum)
	{
		return obj.Routes[0].legs[0].steps[stepNum].html_instructions;
	}

	public Vector3 getStartLocation(int stepNum)
	{
		return new Vector3(float.Parse(obj.Routes[0].legs[0].steps[stepNum].start_location.lat),
			float.Parse(obj.Routes[0].legs[0].steps[stepNum].start_location.lng),ALT);
	}

	public Vector3 getEndLocation(int stepNum)
	{
		return new Vector3(float.Parse(obj.Routes[0].legs[0].steps[stepNum].end_location.lat),
			float.Parse(obj.Routes[0].legs[0].steps[stepNum].start_location.lng), ALT);
	}

	public bool isLastStep()
	{
		StepController sc = step.GetComponent<StepController>();
		return (sc.stepNum == obj.Routes[0].legs[0].steps.Count - 1);
	}

	public string getDistanceText(int stepNum)
	{
		return obj.Routes[0].legs[0].steps[stepNum].distance.text;
	}

	public float getDistance(int stepNum)
	{
		return float.Parse(obj.Routes[0].legs[0].steps[stepNum].distance.val);
	}

	public string getDurationText(int stepNum)
	{
		return obj.Routes[0].legs[0].steps[stepNum].duration.text;
	}

	public float getDuration(int stepNum)
	{
		return float.Parse(obj.Routes[0].legs[0].steps[stepNum].duration.val);
	}

	//It works like a tree of belongings, easily recognizable in the output, but I will draw it

	class RootObject
	{
		[JsonProperty("geocoded_waypoints")]
		public List<Geocoded_Waypoints> Geocoded_Waypoints { get; set; }

		[JsonProperty("routes")]
		public List<Routes> Routes { get; set; }

		[JsonProperty("status")]
		public string status { get; set; }

	}



	class Geocoded_Waypoints
	{
		[JsonProperty("geocoder_status")]
		public string Geocoder_Status { get; set; }

		[JsonProperty("place_id")]
		public string place_id { get; set; }



	}

	class Routes
	{
		[JsonProperty("bounds")]
		Bounds bounds { get; set; }

		[JsonProperty("copyrights")]
		public string copyrights { get; set; }

		[JsonProperty("legs")]
		public List<Legs> legs { get; set; }

		[JsonProperty("summary")]
		public string summary { get; set; }

	}


	class Bounds
	{

		//Bounds of the route, not relevant

		[JsonProperty("northeast")]
		public Northeast northeast { get; set; }

		[JsonProperty("southwest")]
		public Southwest southwest { get; set; }

	}

	class Northeast
	{

		[JsonProperty("lat")]
		public string lat { get; set; }

		[JsonProperty("lng")]
		public string lng { get; set; }

	}

	class Southwest
	{
		[JsonProperty("lat")]
		public string lat { get; set; }

		[JsonProperty("lng")]
		public string lng { get; set; }

	}

	class Legs
	{
		//This is important, establishes the route and the checkpoints

		[JsonProperty("distance")]
		public Distance distance { get; set; }

		[JsonProperty("duration")]
		public Duration duration { get; set; }

		[JsonProperty("end_address")]
		public string end_address { get; set; }

		[JsonProperty("start_address")]
		public string start_address { get; set; }

		[JsonProperty("end_location")]
		public End_Location end_location { get; set; }

		[JsonProperty("start_location")]
		public Start_Location start_location { get; set; }

		//This is the important part

		[JsonProperty("steps")]
		public List<Steps> steps { get; set; }
	}

	class Distance
	{
		[JsonProperty("text")]
		public string text { get; set; }

		//The value is the same thing but in meters, in case it's more useful

		[JsonProperty("value")]
		public string val { get; set; }

	}

	class Duration
	{
		[JsonProperty("text")]
		public string text { get; set; }

		//The value is the same thing but in seconds, in case it's more useful

		[JsonProperty("value")]
		public string val { get; set; }

	}

	class End_Location
	{
		[JsonProperty("lat")]
		public string lat { get; set; }

		//The value is the same thing but in meters, in case it's more useful

		[JsonProperty("lng")]
		public string lng { get; set; }

	}

	class Start_Location
	{
		[JsonProperty("lat")]
		public string lat { get; set; }

		//The value is the same thing but in meters, in case it's more useful

		[JsonProperty("lng")]
		public string lng { get; set; }

	}


	class Steps
	{
		[JsonProperty("distance")]
		public Distance distance { get; set; }

		[JsonProperty("duration")]
		public Duration duration { get; set; }

		[JsonProperty("end_location")]
		public End_Location end_location { get; set; }

		[JsonProperty("start_location")]
		public Start_Location start_location { get; set; }

		//I omit the polyline because I find it useless for our purpose

		[JsonProperty("html_instructions")]
		public string html_instructions { get; set; }

		[JsonProperty("travel_mode")]
		public string travel_mode { get; set; }

		[JsonProperty("maneuver")]
		public string maneuver { get; set; }
	}


}
