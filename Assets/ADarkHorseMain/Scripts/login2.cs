using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System;

public class login2 : MonoBehaviour
{
    public GameObject password;
    public GameObject email;
    public GameObject Login;
    public GameObject errorMessage;
    public GameObject LoadingCircle;

    public string StatsJson;

    public Button Loginbutton;

    private string Email; 

    private string Password;

    private string json;

    private Text errorMessageTxt;

    private Color32 ErrorColor = new Color32(236, 17, 124, 255);
    private string url2 = "https://api.thedarkhorse.io/api/users/5f491b72069bc2125f8b5d35";
    public static string tokenfornext;

    [Serializable]
    public class SendResponseData
    {
        public string error;
        public StatusResponseData status;
        public DataResponseData data; 
    }

    [Serializable]
    public class StatusResponseData
    {
        public string code;
    }

    [Serializable]
    public class DataResponseData
    {
        public string message;
    }

    void showEmailError(string errorMsg)
    {
        errorMessageTxt = errorMessage.GetComponent<Text>();
        {
            errorMessageTxt.text = errorMsg;
            errorMessageTxt.color = ErrorColor;
        }
    }

    void Start()
    {

        Loginbutton = Login.GetComponent<Button>();
        Loginbutton.onClick.AddListener(ValidateLogin);

        //TODO: in tokened APIs check 403 case

        
    }

    private void ValidateLogin()
    {
        showEmailError(" ");
        Password = password.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;

        if (Email == null || Email == "" || !Email.Contains("@"))
        {
            showEmailError("Please enter a valid Email address");
        }
        if (Password == null || Password == "")
        {
            showEmailError("Wrong password");
        }
        if (Password.Length < 6)
        {
            showEmailError("Enter a password of atleast 6 digits");
        }
        else
        {
            LoadingCircle.SetActive(true);
            Debug.Log("the email is " + Password + Email);
            //StartCoroutine(CallSendOtp(Email));
        }
        credentials newcred = new credentials();
        newcred.email = Email;
        newcred.password = Password;
        json = JsonUtility.ToJson(newcred);
        Debug.Log(json); 

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
        
       
        //Encode the JSON string into a bytes
        byte[] postData = System.Text.Encoding.UTF8.GetBytes(json);
        //Now we call a new WWW request
        WWW www = new WWW(URL, postData, headers);
        //And we start a new co routine in Unity and wait for the response.
        StartCoroutine(WaitForRequest(www));
    }


   
   
    Dictionary<string, string> headers = new Dictionary<string, string>();
    //With the @ before the string, we can split a long string in many lines without getting errors
    
   
   
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
            // headers.Add("x-auth-token", tokenval.token);  //5f491cbd069bc2125f8b5d3f << userID  
            tokenfornext = tokenval.token;
          
            UnityWebRequest webRequest = UnityWebRequest.Get(url2);
            Debug.Log("API hi");
            Debug.Log(url2);
            webRequest.SetRequestHeader("x-auth-token", tokenval.token);

            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                StatsJson = webRequest.downloadHandler.text;
                LoadingCircle.SetActive(false); 
                
                   SceneManager.LoadScene(0);
//load scene 
            }


        }
        else
        {
            //Something goes wrong, print the error response
            Debug.Log(www.error);
            showEmailError("Incorrect Email or Password");
            LoadingCircle.SetActive(false);
            headers.Clear();
        }
    }
}


public class tokenOpener
{
    public string token;
} 
public class credentials
{
    public string email;
    public string password;
}