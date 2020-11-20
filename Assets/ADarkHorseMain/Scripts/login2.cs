using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class login2 : MonoBehaviour
{
    public string url2 = "https://api.thedarkhorse.io/api/metrics/5f491cbd069bc2125f8b5d3f";
    Dictionary<string, string> headers = new Dictionary<string, string>();
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
            tokenOpener tokenval = JsonUtility.FromJson<tokenOpener>(www.text);  // This line parses the whole JSON (token)
            Debug.Log(tokenval.token);
            headers.Add("x-auth-token", tokenval.token);  //5f491cbd069bc2125f8b5d3f << userID 

          
            UnityWebRequest webRequest = UnityWebRequest.Get(url2);
            webRequest.SetRequestHeader("x-auth-token", tokenval.token);

            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            }


        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
        }
    }
}


public class tokenOpener
{
    public string token;
}