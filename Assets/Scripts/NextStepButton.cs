using UnityEngine;
using System.Collections;

public class NextStepButton : MonoBehaviour {

	public void OnClick()
	{
		//Debug.Log("Click!");
		GameObject.FindGameObjectWithTag("Step").GetComponent<StepController>().stepNumIncrement();
	}
}
