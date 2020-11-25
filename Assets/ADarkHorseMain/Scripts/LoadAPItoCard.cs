using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoadAPItoCard : MonoBehaviour
{
    public string gitAPIurlMiles;
    public string gitAPIurlPlayer2;  //github testing API 
    public string playerData;
    public string FirstName;
    public string LastName;
    public int Number;
    public int Goals;
    public string AvatarURL;
    public string clubLogoURL;

    public Image profilepic;
    public Image clubLogo;

    public TextMeshProUGUI playerNumber;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI GoalsScored;
    public TextMeshProUGUI place;

    public string mainURL;
    
  

    void Start()
    {

        mainURL = gitAPIurlMiles;
        LoadAPI();// LoadAPI();
    }
    public void button1()
    {
        mainURL = gitAPIurlMiles;
        LoadAPI();
    } 

    public void button2()
    {
        mainURL = gitAPIurlPlayer2;
        LoadAPI();
    }
    public void LoadAPI()
    {

        StartCoroutine(Getplayerdata());

    }

    IEnumerator Getplayerdata()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(mainURL);
        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError)
        {
            Debug.Log(": Error: " + webRequest.error);
        }
        else
        {
            Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
            playerData = webRequest.downloadHandler.text;
            Debug.Log(playerData);

            PlayerData data = JsonUtility.FromJson<PlayerData>(playerData); 
          
            FirstName = data.firstname;
            LastName = data.lastname;
            Debug.Log(data.firstname + data.lastname);
            Number = data.specifics.number;
            Goals = data.specifics.goals;
            Debug.Log(Number);
            Debug.Log(Goals);
            AvatarURL = data.avatar;
           
            PlayerName.text = FirstName + " " + LastName;
            GoalsScored.text = "Goals Scored : "+ Goals.ToString();
            place.text = data.city +","+ data.state;
            playerNumber.text = "#"+data.specifics.number.ToString();
            clubLogoURL = data.specifics.club.logo;
            Debug.Log(clubLogoURL);




            StartCoroutine(GetTexture());
       }
    }
    IEnumerator GetTexture()
    {
       UnityWebRequest www = UnityWebRequestTexture.GetTexture(AvatarURL);
        UnityWebRequest www1 = UnityWebRequestTexture.GetTexture(clubLogoURL);

        var uwr = www.SendWebRequest();
        var uwr1 = www1.SendWebRequest();
        if (!uwr.isDone)
        {
            profilepic.color = new Color(profilepic.color.r, profilepic.color.g, profilepic.color.b,0);
        }

        if (!uwr1.isDone)
        {
            clubLogo.color = new Color(clubLogo.color.r, clubLogo.color.g, clubLogo.color.b, 0);
        }

        yield return uwr; 

        
        yield return uwr1; 
       


        if (www1.isNetworkError || www1.isHttpError)
        {
            Debug.Log(www1.error);
            
        } 

        else if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {   
            Texture2D myTexture = ((DownloadHandlerTexture)www1.downloadHandler).texture;
            clubLogo.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.one / 2);
            clubLogo.color = new Color(clubLogo.color.r, clubLogo.color.g, clubLogo.color.b, 100);

            Texture2D myTexture1 = ((DownloadHandlerTexture)www.downloadHandler).texture;
            profilepic.sprite = Sprite.Create(myTexture1, new Rect(0, 0, myTexture1.width, myTexture1.height), Vector2.one / 2);
            profilepic.color = new Color(profilepic.color.r, profilepic.color.g, profilepic.color.b, 100);
        }
    }
}


[Serializable]
public class PlayerData
{ 
    public string firstname;       
    public string lastname;
    public specifics specifics;
    public string avatar;
    public string city;
    public string state;

}
[Serializable]
public class specifics
{
   
    public int goals;
    public int number;
    public int position;
    public club club;
    
} 
[Serializable]

public class club
{
    public string name;
    public string logo;
}





