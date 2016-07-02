using UnityEngine;
using System.Collections;

public class NavigationController : MonoBehaviour {

    private Vector3[] steps;
    //Prefab to use
    public GameObject step;
    
    // Use this for initialization
	void Start ()
    {
        Instantiate(step, new Vector3(0, 0, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
