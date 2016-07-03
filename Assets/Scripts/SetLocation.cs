using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetLocation : MonoBehaviour {
	public InputField startPos;
	public InputField destPos;
	string start, dest;
	DataFetcher df;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SaveText() {
		start = startPos.text;
		dest = destPos.text;
		df.origin = start;
		df.destination = dest;
	}
}
