using UnityEngine;
using System.Collections;

public class GPSLocationFetcher : MonoBehaviour {

    //https://docs.unity3d.com/ScriptReference/LocationService.Start.html
    //http://hro.hatenablog.jp/entry/2015/08/08/005930
    //http://forum.unity3d.com/threads/gps-input-location-accuracy-question.132587/

    // TO DO Properly display error message

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(GetGPS());
    }

    void Update()
    {
        Debug.Log(Input.location.status.ToString());
    }

    public float GetLat()
    {
        return Input.location.lastData.latitude;
    }

    public float GetLng()
    {
        return Input.location.lastData.longitude;
    }
    public float GetAlt()
    {
        return Input.location.lastData.altitude;
    }


    private IEnumerator GetGPS()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }
        Input.location.Start();
        int maxWait = 30;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            Debug.Log("Location: " +
                  Input.location.lastData.latitude + " " +
                  Input.location.lastData.longitude + " " +
                  Input.location.lastData.altitude + " " +
                  Input.location.lastData.horizontalAccuracy + " " +
                  Input.location.lastData.timestamp);
        }
        Input.location.Stop();
    }
}
