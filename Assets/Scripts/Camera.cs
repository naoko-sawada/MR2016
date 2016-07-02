using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	private bool captureOK;
	private int cameraNum;

	// Use this for initialization
	void Start () {
		captureOK = false;
		cameraNum = 0;
		//WebCamDevice[] devices = WebCamTexture.devices;
		//// display all cameras
		//for (var i = 0; i < devices.Length; i++) {
		//	Debug.Log(devices[i].name);
		//}

		//WebCamTexture webcamTexture = new WebCamTexture(devices[0].name,Screen.width,Screen.height,30);
		//GetComponent<Renderer>().material.mainTexture = webcamTexture;
		//webcamTexture.Play();
		//transform.localScale = new Vector3(transform.localScale.y * Screen.width / Screen.height, transform.localScale.y, transform.localScale.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (!captureOK) {
			WebCamDevice[] devices = WebCamTexture.devices;
			// display all cameras

			WebCamTexture webcamTexture = new WebCamTexture(devices[cameraNum % devices.Length].name, Screen.width, Screen.height, 30);
			GetComponent<Renderer>().material.mainTexture = webcamTexture;
			webcamTexture.Play();
			transform.localScale = new Vector3(transform.localScale.y * Screen.width / Screen.height, transform.localScale.y, transform.localScale.y);
			captureOK = true;
		}
	}

	public void CameraChange()
	{
		cameraNum++;
		captureOK = false;
	}
}