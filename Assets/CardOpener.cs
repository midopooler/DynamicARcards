using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOpener : MonoBehaviour
{
    public GameObject playerCard;
    GameObject selectedPlayer;
    void Start()
    {
        playerCard.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {  
        if(selectedPlayer != null)
        playerCard.transform.position = Camera.main.WorldToScreenPoint(selectedPlayer.transform.position + new Vector3(0, 1.5f, 0));
    
        if (Input.GetMouseButtonDown(0)) // left click or touch, works for both 
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast from center of the screen 
            RaycastHit hit; //hit information 
            if (Physics.Raycast(ray, out hit)) //if raycast hit : 
            {
                if (hit.transform.CompareTag("Player"))  //compares the tag of the hit with the "Player Tag"
                {
                    if (!playerCard.gameObject.activeSelf)
                    {
                        selectedPlayer = hit.transform.gameObject;
                        playerCard.SetActive(true);
                    }
                    else
                    {
                        playerCard.SetActive(false);
                        selectedPlayer = null;
                    }
                }
            }
        }
    }
   
}