using UnityEngine;
using System.Collections;

public class CameraChangeButton : MonoBehaviour {

	public void OnClick()
	{
		Debug.Log("Click!");
		GameObject.Find("Quad").GetComponent<Camera>().CameraChange();
	}
}
