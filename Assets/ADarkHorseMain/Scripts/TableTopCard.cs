﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class TableTopCard : MonoBehaviour
{
    public string userID;
    public string playerData;
    public string FirstName;
    public string LastName;
    public int Number;
    public int Goals;
    public string AvatarURL;
    public string clubLogoURL;

    public Image profilepic;
    public Image clubLogo; 

    public RawImage HighlightVideo;
    public VideoPlayer highvidplaya;

    public TextMeshProUGUI playerNumber;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI GoalsScored;
    public TextMeshProUGUI place; 

    public string mainURL;

    public static string VideoURL = "https://darkhorse-static-data.s3-us-west-2.amazonaws.com/g4bwu8kgo1cjx1_videoplayback.mp4";

    public GameObject PlayerCard;
    public CardOpener namePlate;
    
  

    void Start()
    {
        PlayerCard.SetActive(false);
        mainURL = "https://api.thedarkhorse.io/api/users/" + userID;
        
        LoadAPI();
    }
   
    public void LoadAPI()
    {

        StartCoroutine(Getplayerdata());
        Dictionary<string, string> headers = new Dictionary<string, string>();

    }

    IEnumerator Getplayerdata()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(mainURL);
        webRequest.SetRequestHeader("x-auth-token", login2.tokenfornext);
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
            namePlate.namePlate.text = FirstName +" "+ LastName;
            Debug.Log(data.firstname + data.lastname);
            Number = data.specifics.number;
            Goals = data.specifics.goals;
            Debug.Log(Number);
            Debug.Log(Goals);
            AvatarURL = data.avatar;
            Debug.Log(AvatarURL);
            Debug.Log(clubLogoURL);
           
            PlayerName.text = FirstName + " " + LastName;
            GoalsScored.text = "Goals Scored : "+ Goals.ToString();
            place.text = data.specifics.position;
            playerNumber.text = "#"+data.specifics.number.ToString();
            clubLogoURL = data.specifics.club.logo;
           // VideoURL = data.specifics.matchHighlight[0];
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
            

            //VideoPlayerBelow
           
        //      var vp = HighlightVideo.gameObject.GetComponent<VideoPlayer>();
        //     vp.url = VideoURL;
        //    Debug.Log(VideoURL);

        //vp.isLooping = true;
        //    vp.renderMode = VideoRenderMode.RenderTexture;
        //  //vp.targetTexture = HighlightVideo.texture;
        //vp.targetMaterialProperty = "_MainTex";

        //vp.Play();
        }
    }
}


