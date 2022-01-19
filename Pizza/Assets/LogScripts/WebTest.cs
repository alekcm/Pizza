using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        WWW request = new WWW("http://localhost/sqlconnect/webtest.php");
        yield return request;
        string[] webResults = request.text.Split('\t');
        Debug.Log(webResults[0]);
        int webnumber = int.Parse(webResults[1]);
        webnumber *= 2;
        Debug.Log(webnumber);
    }

}
