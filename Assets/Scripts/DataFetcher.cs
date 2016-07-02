using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DataFetcher : MonoBehaviour {

    // TODO Show progress of request ???
    // TODO Construct URL using user input, hence the public value here below for origin and destination

    //public string origin;
    //public string destination;
    public string url = "https://maps.googleapis.com/maps/api/directions/json?origin=Toronto&destination=Montreal&key=AIzaSyCiGylcR4IqXsz6HJeB3Xuu24jZyXDjxog";

    public WWW www;
    private jsonDeserializer json;

    void Start ()
    ///Launched when Unity executes the script for the first time. Request the url defined in the string url.
    {
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)        {

            //www.text gives access to the content of a successful request under the form of a string.
            Debug.Log("WWW Ok!: " + www.text[1]);
        } else {
            Debug.Log("WWW Error: "+ www.error);
        }

        json = GameObject.Find("GM").GetComponent<jsonDeserializer>();
        json.Program(www);
     }

}
