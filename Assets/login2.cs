using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class login2 : MonoBehaviour
{
    //With the @ before the string, we can split a long string in many lines without getting errors
    private string json = @"{
		'email':'ramiroarredondo@smcearthquakes.com', 
		'password':'Password1234' 
		
	}";
    private void Start()
    {
        doPost();
    }
    void doPost()
    {
        string URL = "https://api.thedarkhorse.io/api/auth";
        string myAccessKey = "myAccessKey";
        string mySecretKey = "mySecretKey";

        //Auth token for http request
        string accessToken;
        //Our custom Headers
        Dictionary<string, string> headers = new Dictionary<string, string>();
        //Encode the access and secret keys
        accessToken = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(myAccessKey + ":" + mySecretKey));
        //Add the custom headers
        headers.Add("Authorization", "Basic " + accessToken);
        headers.Add("Content-Type", "application/json");
        headers.Add("AnotherHeader", "AnotherData");
        headers.Add("Content-Length", json.Length.ToString());
        //Replace single ' for double " 
        //This is usefull if we have a big json object, is more easy to replace in another editor the double quote by singles one
        json = json.Replace("'", "\"");
        //Encode the JSON string into a bytes
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        //Now we call a new WWW request
        WWW www = new WWW(URL, postData, headers);
        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForRequest(www));
    }
    //Wait for the www Request
    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if (www.error == null)
        {
            //Print server response
            Debug.Log(www.text);
        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
        }
    }
}