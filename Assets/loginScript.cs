using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loginScript : MonoBehaviour
{
    public GameObject password;
    public GameObject email;
    public GameObject sendOtp;
    public GameObject errorMessage;
    public GameObject LoadingCircle;
    //for displaying mobile number on canvas UI

    //public InputField emailid;
    //public InputField DHpassword;

    public Button sendOtpBtn;

    private string Email;

    public static string Password;

    private Text errorMessageTxt; 

    private Color32 ErrorColor = new Color32(236, 17, 124, 255);

    [Serializable]
    public class SendOtpResponseData
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

    void showPhoneNumberError(string errorMsg)
    {
        errorMessageTxt = errorMessage.GetComponent<Text>();
        {
            errorMessageTxt.text = errorMsg;
            errorMessageTxt.color = ErrorColor;
        }
    }

    void Start()
    {
       
        sendOtpBtn = sendOtp.GetComponent<Button>();
        sendOtpBtn.onClick.AddListener(ValidateSendotp);

        //TODO: in tokened APIs check 403 case
    }

    private void ValidateSendotp()
    {
       Password = password.GetComponent<InputField>().text;
        Email = email.GetComponent<InputField>().text;

        if (Email == null || Email == "" || !Email.Contains("@"))
        {
            showPhoneNumberError("Please enter a valid Email address");
        }
        if (Password == null || Password == "")
        {
            showPhoneNumberError("Wrong password");
        }
          if (Password.Length < 6)
        {
            showPhoneNumberError("Enter a password of atleast 6 digits");
        }
        else
        {
            LoadingCircle.SetActive(true);
            Debug.Log("the email is " + Password + Email);
            StartCoroutine(CallSendOtp(Email));
        }
    }

    public IEnumerator CallSendOtp(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", Email);
        form.AddField("password", Password);

        string url = StaticVars.serverBaseUrl + "/api/auth";
        //string bodyJsonString = "{\"data\":{ \"phoneNumber\" : \"" + email + "\",\"countryCode\" : \"" + Password + "\"}}";

      //  var www = new UnityWebRequest(url, "POST");

        UnityWebRequest www = UnityWebRequest.Post("https://api.thedarkhorse.io/api/auth", form);
       yield return www.SendWebRequest();
        Debug.Log(www);
        // var www = new UnityWebRequest(url, "POST");
        // byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        // www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        // www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //  www.SetRequestHeader("Content-Type", "application/json");
        //UnityWebRequest.SendWebRequest
        //using (www)
        //{
            // yield return www.SendWebRequest();
            LoadingCircle.SetActive(false);
            if (www.isNetworkError || www.isHttpError)
            {
                showPhoneNumberError("Error connecting to server. Please check your internet connection or come back later.");
                Debug.Log("Error connecting to server. Please check your internet connection or come back later.");
            }
            else
            {
                SendOtpResponseData responseData = JsonUtility.FromJson<SendOtpResponseData>(www.downloadHandler.text);
                if (responseData.status.code == "200")
                {
                    // StaticValue.Email = email;
                    // StaticValue.Password = password;
                    Debug.Log("response data successful");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    showPhoneNumberError("Error connecting to server: ");
                    Debug.Log("Error connecting to server: ");
                }
            }
       // }

    }

    //IEnumerator Upload()
    //{
    //    WWWForm form = new WWWForm();
    //    form.AddField(Email, Password);

    //    UnityWebRequest www = UnityWebRequest.Post("https://api.thedarkhorse.io/api/auth", form);
    //   // yield return www.Send();
    //   // var www = new UnityWebRequest(url, "POST");

    //    if (www.isError)
    //    {
    //        Debug.Log(www.error);
    //    }
    //    else
    //    {
    //        Debug.Log("Form upload complete!");
    //    }
    //}
    //void Awake()
    //{
    //    DontDestroyOnLoad(password);
    //}


}
