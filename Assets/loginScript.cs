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
    public GameObject phoneNumber;
    public GameObject sendOtp;
    public GameObject errorMessage;
    public GameObject LoadingCircle;
    //for displaying mobile number on canvas UI

    public Button sendOtpBtn;

    private string PhoneNumber;

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
        PhoneNumber = phoneNumber.GetComponent<InputField>().text;

        if (PhoneNumber == null || PhoneNumber == "")
        {
            showPhoneNumberError("Please enter a valid Phone Number");
        }
        if (Password == null || Password == "")
        {
            showPhoneNumberError("Please enter a valid country code");
        }
        else if (PhoneNumber.Length < 7)
        {
            showPhoneNumberError("Phone number should be 10 digits long.");         //what should be the error message here?
        }
        else
        {
            LoadingCircle.SetActive(true);
            Debug.Log("the mobile number is " + Password + PhoneNumber);
            StartCoroutine(CallSendOtp(PhoneNumber));
        }
    }

    public IEnumerator CallSendOtp(string phoneNumber)
    {
        string url = StaticVars.serverBaseUrl + "/v2/auth/sendOtp";
        string bodyJsonString = "{\"data\":{ \"phoneNumber\" : \"" + phoneNumber + "\",\"countryCode\" : \"" + Password + "\"}}";

        var www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        using (www)
        {
            yield return www.SendWebRequest();
            LoadingCircle.SetActive(false);
            if (www.isNetworkError || www.isHttpError)
            {
                showPhoneNumberError("Error connecting to server. Please check your internet connection or come back later.");
            }
            else
            {
                SendOtpResponseData responseData = JsonUtility.FromJson<SendOtpResponseData>(www.downloadHandler.text);
                if (responseData.status.code == "200")
                {
                    StaticValue.phoneNumber = phoneNumber;
                    StaticValue.CountryCode = password;

                    SceneManager.LoadScene(2);
                }
                else
                {
                    showPhoneNumberError("Error connecting to server: " + responseData.data.message);
                }
            }
        }

    }
    void Awake()
    {
        DontDestroyOnLoad(password);
    }


}