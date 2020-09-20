using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dateTeller : MonoBehaviour
{
    public GameObject monthTextObject;
    public GameObject dayTextObject;
    public GameObject yearTextObject;

    // Start is called before the first frame update
    void Start()
    {
    InvokeRepeating("UpdateDate", 2f, 30f);
    }

    // Update is called once per frame
    void UpdateDate()
    {
    monthTextObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString("MMMM");
    dayTextObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString("dd");
    yearTextObject.GetComponent<TextMeshPro>().text = System.DateTime.Now.ToString("yyyy");
    }
}
