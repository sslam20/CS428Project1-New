using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class windSpeed : MonoBehaviour
{
    public GameObject windSpeedTextObject;
    public GameObject arrowObject;
    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=932d74499c47e103d1b3924c88ebcf85&units=imperial";

    void Start()
    {

        // wait a couple seconds to start and then refresh every 30 seconds
        InvokeRepeating("GetDataFromWeb", 2f, 30f);
    }

    void GetDataFromWeb()
    {
        StartCoroutine(GetRequest(url));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // print out the weather data to make sure it makes sense
                var json = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + ":\nReceived: " + json);

                var lengthOfSpeed = json.IndexOf("deg") - json.IndexOf("speed") - 9;

                var lengthOfDeg = json.Substring(json.IndexOf("deg")).IndexOf("clouds") - 8;
                var degree = double.Parse(json.Substring(json.IndexOf("deg") + 5, lengthOfDeg));

                var direction = "N";
                if (degree > 27.5 && degree <= 72.5) {   // NE
                  direction = "NE";
                }
                else if (degree > 72.5 && degree <= 117.5) {   // E
                  direction = "E";
                }
                else if (degree > 117.5 && degree <= 162.5) {   // SE
                  direction = "SE";
                }
                else if (degree > 162.5 && degree <= 207.5) {   // S
                  direction = "S";
                }
                else if (degree > 207.5 && degree <= 252.5) {   // SW
                  direction = "SW";
                }
                else if (degree > 252.5 && degree <= 297.5) {   // W
                  direction = "W";
                }
                else if (degree > 297.5 && degree <= 342.5) {   // NW
                  direction = "NW";
                }
                else {    // N
                  direction = "N";
                }

                windSpeedTextObject.GetComponent<TextMeshPro>().text = json.Substring(json.IndexOf("speed") + 7, lengthOfSpeed) + " mph " + direction;
                arrowObject.transform.localRotation = Quaternion.Euler(90f, (float)(degree - 90.0), 0f);
            }
        }
    }
}
