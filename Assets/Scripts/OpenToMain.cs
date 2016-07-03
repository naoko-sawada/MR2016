using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenToMain : MonoBehaviour {
	public InputField startPos;
	public InputField destPos;
	public GameObject inputError;
	public ToggleGroup wayButtons;
	GameObject canvas;
	GameObject text;
	string start, dest;
	DataFetcher df;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SceneLoad () {
		start = startPos.text;
		dest = destPos.text;
		if (start.Length == 0 || dest.Length == 0) {
			if (GameObject.Find ("InputError(Clone)") == null) {
				canvas = GameObject.Find ("Panel");
				text = Instantiate (inputError);
				text.transform.SetParent (canvas.transform, false);
			}
		} else {
			// if origin is "Here", get position of the user from his smart phone
			// if (start == "Here") {
			// 		GET USER'S POSITION AND SEND THE INFOMATION
			// 		Application.LoadLevel ("main");
			// } else {

			//Toggle tgl = wayButtons.ActiveToggles();

			// set way by tgl.name
			df.origin = start;
			df.destination = dest;
			//df.way
			//Application.LoadLevel ("main");
			// }
		}
	}
}
