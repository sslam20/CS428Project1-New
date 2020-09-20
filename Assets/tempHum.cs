using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class tempHum : MonoBehaviour
{
    public GameObject tempTextObject;
    public GameObject tempCylinderRedObject;
    public GameObject tempCylinderObject;
    private Vector3 tempScaleChangeRed;
    private Vector3 tempScaleChangeBlank;
    private Vector3 tempPositionChangeRed;
    private Vector3 tempPositionChangeBlank;

    public GameObject humTextObject;
    public GameObject humCylinderBlueObject;
    public GameObject humCylinderObject;
    private Vector3 humScaleChangeBlue;
    private Vector3 humScaleChangeBlank;
    private Vector3 humPositionChangeBlue;
    private Vector3 humPositionChangeBlank;

    string url = "http://api.openweathermap.org/data/2.5/weather?lat=41.88&lon=-87.6&APPID=932d74499c47e103d1b3924c88ebcf85&units=imperial";

    void Start()
    {

    // wait a couple seconds to start and then refresh every 900 seconds

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

                var baseHeight = 0.0238;
                var totalHeight = 0.0144;

                var lengthOfTemp = json.IndexOf("feels_like") - json.IndexOf("temp") - 8;
                var temperature = json.Substring(json.IndexOf("temp") + 6, lengthOfTemp);
                var tempPercentage = (double.Parse(temperature) + 40.0) / (160.0);
                var sizeRed = (tempPercentage * totalHeight) / 2.0;
                var sizeBlankT = (1.0 - tempPercentage) * totalHeight / 2.0;
                var placementRed = baseHeight + sizeRed;
                var placementBlankT = baseHeight + (2.0 * sizeRed) + sizeBlankT;
                Debug.Log("tempPercentage is " + tempPercentage);
                Debug.Log("sizeRed is " + sizeRed + " and placementRed is " + placementRed);
                Debug.Log("sizeBlankT is " + sizeBlankT + " and placementBlankT is " + placementBlankT);

                tempScaleChangeRed = new Vector3(0.0013f, (float)sizeRed, 0.0013f);
                tempScaleChangeBlank = new Vector3(0.0013f, (float)sizeBlankT, 0.0013f);
                tempPositionChangeRed = new Vector3(-0.01f, (float)placementRed, -0.005f);
                tempPositionChangeBlank = new Vector3(-0.01f, (float)placementBlankT, -0.005f);

                tempTextObject.GetComponent<TextMeshPro>().text = temperature + " F";
                tempCylinderRedObject.transform.localScale += (tempScaleChangeRed - tempCylinderRedObject.transform.localScale);
                tempCylinderObject.transform.localScale += (tempScaleChangeBlank - tempCylinderObject.transform.localScale);
                tempCylinderRedObject.transform.localPosition += (tempPositionChangeRed - tempCylinderRedObject.transform.localPosition);
                tempCylinderObject.transform.localPosition += (tempPositionChangeBlank - tempCylinderObject.transform.localPosition);

                var lengthOfHumidity = json.IndexOf("visibility") - json.IndexOf("humidity") - 13;
                var humidity = json.Substring(json.IndexOf("humidity") + 10, lengthOfHumidity);
                var sizeBlue = (double.Parse(humidity) * totalHeight) / 2.0 / 100.0;
                var sizeBlankH = (100.0 - double.Parse(humidity)) * totalHeight / 2.0 / 100.0;
                var placementBlue = baseHeight + sizeBlue;
                var placementBlankH = baseHeight + (2.0 * sizeBlue) + sizeBlankH;
                Debug.Log("humidity percentage is " + double.Parse(humidity) / 100.0);
                Debug.Log("sizeBlue is " + sizeRed + " and placementBlue is " + placementRed);
                Debug.Log("sizeBlankH is " + sizeBlankT + " and placementBlankH is " + placementBlankT);

                humScaleChangeBlue = new Vector3(0.0013f, (float)sizeBlue, 0.0013f);
                humScaleChangeBlank = new Vector3(0.0013f, (float)sizeBlankH, 0.0013f);
                humPositionChangeBlue = new Vector3(0.01f, (float)placementBlue, -0.005f);
                humPositionChangeBlank = new Vector3(0.01f, (float)placementBlankH, -0.005f);
                
                humTextObject.GetComponent<TextMeshPro>().text = humidity + " %";
                humCylinderBlueObject.transform.localScale += (humScaleChangeBlue - humCylinderBlueObject.transform.localScale);
                humCylinderObject.transform.localScale += (humScaleChangeBlank - humCylinderObject.transform.localScale);
                humCylinderBlueObject.transform.localPosition += (humPositionChangeBlue - humCylinderBlueObject.transform.localPosition);
                humCylinderObject.transform.localPosition += (humPositionChangeBlank - humCylinderObject.transform.localPosition);
             }
          }
      }
}
