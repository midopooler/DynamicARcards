using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class LoadAPItoCard : MonoBehaviour
{
    public string userID;
    public string playerData;
    public string FirstName;
    public string LastName;
    public string CoachFirstName;
    public string CoachLastName;
    public int Number;
    public int Goals;
    public int Assits;
    
    public string clubLogoURL;
    public int sprint;

   
    public Image clubLogo; 

    public RawImage HighlightVideo;
    public VideoPlayer highvidplaya;

    public TextMeshProUGUI playerNumber; 
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI  CoachName;
    public TextMeshProUGUI GoalsScored;
    public TextMeshProUGUI AssitsScored;
    public TextMeshProUGUI place;
    public TextMeshProUGUI sprints;

    public string mainURL;

    public static string VideoURL; //= "https://darkhorse-static-data.s3-us-west-2.amazonaws.com/g4bwu8kgo1cjx1_videoplayback.mp4";
    
  

    void Start()
    {
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
          
            FirstName = data.firstname;                         //playername
            LastName = data.lastname;
            CoachFirstName = data.specifics.team.coach.firstname;  //coachname
            CoachLastName = data.specifics.team.coach.lastname;
            Debug.Log(data.firstname + data.lastname);
            Number = data.specifics.number;
            Goals = data.specifics.goals;
            sprint = data.specifics.sprintPerGame;
            Assits = data.specifics.assists;
            Debug.Log(Number);
            Debug.Log(Goals);
            
            Debug.Log(clubLogoURL);
           
            PlayerName.text = FirstName + " " + LastName;
            CoachName.text = "Coach Name :- " + CoachFirstName + " " + CoachLastName;
            Debug.Log("Coach Name :- " + CoachFirstName + " " + CoachLastName);
            GoalsScored.text = "Goals Scored : "+ Goals.ToString();
            AssitsScored.text = "Assits : " + Assits.ToString();
            place.text = data.specifics.position;
            sprints.text = "Sprints per game : " + sprint.ToString();
            playerNumber.text = "#"+data.specifics.number.ToString();
            clubLogoURL = data.specifics.club.logo;
            if (data.specifics.matchHighlight.Count != 0)
            {
                VideoURL = data.specifics.matchHighlight[0];
            }
            Debug.Log(clubLogoURL);




            StartCoroutine(GetTexture());
       }
    }
    IEnumerator GetTexture()
    {

      
        UnityWebRequest www1 = UnityWebRequestTexture.GetTexture(clubLogoURL);

       
        var uwr1 = www1.SendWebRequest();
        
        if (!uwr1.isDone)
        {
            clubLogo.color = new Color(clubLogo.color.r, clubLogo.color.g, clubLogo.color.b, 0);
        }

       

        
        yield return uwr1; 
       


        if (www1.isNetworkError || www1.isHttpError)
        {
            Debug.Log(www1.error);
            
        } 

        
        else
        {   
            Texture2D myTexture = ((DownloadHandlerTexture)www1.downloadHandler).texture;
            clubLogo.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), Vector2.one / 2);
            clubLogo.color = new Color(clubLogo.color.r, clubLogo.color.g, clubLogo.color.b, 100);

            

            //VideoPlayerBelow
           
              var vp = HighlightVideo.gameObject.GetComponent<VideoPlayer>();
            if (VideoURL == null)
            {
                Debug.Log("video null");
            }
            else
            {
                vp.url = VideoURL;
                Debug.Log(VideoURL);
            
        vp.isLooping = true;
            vp.renderMode = VideoRenderMode.RenderTexture;
          //vp.targetTexture = HighlightVideo.texture;
        vp.targetMaterialProperty = "_MainTex";

        vp.Play();
        }
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
    public string position;
    public club club;
    public List<string> matchHighlight; 
    public int sprintPerGame;
    public int assists;
    public team team;
} 

[Serializable]
public class club
{
    public string name;
    public string logo;
}


[Serializable]
public class team
{
    public coach coach;
}

[Serializable]
public class coach
{
    public string firstname;
    public string lastname;
}