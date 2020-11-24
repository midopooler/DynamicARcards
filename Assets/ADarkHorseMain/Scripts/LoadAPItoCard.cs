using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAPItoCard : MonoBehaviour
{
    public string gitAPIurl;  //github testing API 
    public string playerData;
    public string FirstName;
    public string LastName;
    public int Number;
    public int Goals;
  

    void Start()
    {
        LoadAPI();
    }

    public void LoadAPI()
    {

        StartCoroutine(Getplayerdata());

    }

    IEnumerator Getplayerdata()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(gitAPIurl);
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
            


        }
    }
}


[Serializable]
public class PlayerData
{ 
    public string firstname;       
    public string lastname;
    public specifics specifics;

}
[Serializable]
public class specifics
{
   
    public int goals;
    public int number;
    
}



