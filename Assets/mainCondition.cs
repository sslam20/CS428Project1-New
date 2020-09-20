using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class mainCondition : MonoBehaviour
{
    public GameObject sunObject;
    public GameObject cloudsObject;
    public GameObject rainObject;
    public GameObject snowObject;
    public GameObject lightningObject;
    public GameObject mistObject;

    public AudioSource clearSkyAudio;
    public AudioSource fewCloudsAudio;
    public AudioSource scatteredCloudsAudio;
    public AudioSource brokenCloudsAudio;
    public AudioSource showerRainAudio;
    public AudioSource rainAudio;
    public AudioSource thunderstormAudio;
    public AudioSource snowAudio;
    public AudioSource mistAudio;

    private Vector3 showVector = new Vector3(1f, 1f, 1f);
    private Vector3 hideVector = new Vector3(0f, 0f, 0f);
    private Vector3 biggerVector = new Vector3(1.5f, 1.5f, 1.5f);
    private int state = 0;

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

                // var lengthOfMainCondition = json.IndexOf("description") - json.IndexOf("main") - 10;
                // var mainCon = json.Substring(json.IndexOf("main") + 7, lengthOfMainCondition);
                var lengthOfDescription = json.IndexOf("icon") - json.IndexOf("description") - 17;
                var description = json.Substring(json.IndexOf("description") + 14, lengthOfDescription);
                Debug.Log("description is " + description);

                if (description.Equals("clear sky")) {
                    // everything but the sun hidden
                    state = 0;
                }
                else if (description.Equals("few clouds")) {
                    // sun and small clouds
                    state = 1;
                }
                else if (description.Equals("scattered clouds")) {
                    // just small clouds
                    state = 2;
                }
                else if (description.Equals("broken clouds")) {
                    // big clouds
                    state = 3;
                }
                else if (description.Equals("shower rain")) {
                    // big clouds and rain
                    state = 4;
                }
                else if (description.Equals("rain")) {
                    // small clouds, sun, and rain
                    state = 5;

                }
                else if (description.Equals("thunderstorm")) {
                    // big clouds, lightning, rain
                    state = 6;

                }
                else if (description.Equals("snow")) {
                    // small clouds, snow
                    state = 7;

                }
                else if (description.Equals("mist")) {
                    // just mist
                    state = 8;

                }
                else {
                    Debug.Log("Error: Weather description not understood.");
                }
                goToState(state);
                Update();
            }
        }
    }

    void Update()
    {
      if (Input.GetKeyDown("right")) {
        Debug.Log("Right arrow was pressed");
        if (state == 8) {
          state = 0;
        }
        else {
          state += 1;
        }
        goToState(state);
      }
      if (Input.GetKeyDown("left")) {
        Debug.Log("Left arrow was pressed");
        if (state == 0) {
          state = 8;
        }
        else {
          state -= 1;
        }
        goToState(state);
      }
    }

    void goToState(int i)
    {
      AudioSource[] audios = {clearSkyAudio, fewCloudsAudio, scatteredCloudsAudio, brokenCloudsAudio, showerRainAudio, rainAudio, thunderstormAudio, snowAudio, mistAudio};
      if (i == 0) {
          // everything but the sun hidden
          sunObject.transform.localScale = showVector;
          cloudsObject.transform.localScale = hideVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 1) {
          // sun and small clouds
          sunObject.transform.localScale = showVector;
          cloudsObject.transform.localScale = showVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 2) {
          // just small clouds
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = showVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 3) {
          // big clouds
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = biggerVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 4) {
          // big clouds and rain
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = biggerVector;
          rainObject.transform.localScale = biggerVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 5) {
          // small clouds, sun, and rain
          sunObject.transform.localScale = showVector;
          cloudsObject.transform.localScale = showVector;
          rainObject.transform.localScale = showVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 6) {
          // big clouds, lightning, rain
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = biggerVector;
          rainObject.transform.localScale = biggerVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = showVector;
          mistObject.transform.localScale = hideVector;

          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {
              audios[j].Pause();
            }
          }
      }
      else if (i == 7) {
          // small clouds, snow
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = showVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = showVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = hideVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else if (i == 8) {
          // just mist
          sunObject.transform.localScale = hideVector;
          cloudsObject.transform.localScale = hideVector;
          rainObject.transform.localScale = hideVector;
          snowObject.transform.localScale = hideVector;
          lightningObject.transform.localScale = hideVector;
          mistObject.transform.localScale = showVector;
          for (int j = 0; j < 9; j++) {
            if (j == i) {
              audios[j].Play();
            }
            else {

              audios[j].Pause();
            }
          }
      }
      else {
          Debug.Log("Error: Weather description not understood.");
      }
    }
}
