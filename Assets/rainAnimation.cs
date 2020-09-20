using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainAnimation : MonoBehaviour
{
    public GameObject rainObject;

    private Vector3 start = new Vector3(.0024f, .0434f, 0f);
    private Vector3 increment = new Vector3(.0012f, .0042f, 0f);
    private int threes = 3;

    // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("rainUpdate", 1f, .5f);
    }

    void rainUpdate()
    {
        if (threes == 3) {
          rainObject.transform.localPosition = start;
          threes = 1;
        }
        else {
          rainObject.transform.localPosition -= increment;
          threes += 1;
        }
    }
}
