using UnityEngine;
using System.Collections;

public class BeforeStepButton : MonoBehaviour {

	public void OnClick()
	{
		//Debug.Log("Click!");
		GameObject.FindGameObjectWithTag("Step").GetComponent<StepController>().stepNumDecrement();
	}
}
