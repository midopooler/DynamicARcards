using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardOpener : MonoBehaviour
{
    public GameObject playerCard;
    public TextMeshProUGUI namePlate;
    GameObject selectedPlayer;
    public string playerID;
    void Start()
    {
        playerCard = this.gameObject.GetComponentInChildren<TableTopCard>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        namePlate.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.GetComponent<Transform>().position + new Vector3(0, 0.5f, 0));
        if(playerCard.activeSelf == true)
        playerCard.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.GetComponent<Transform>().position + new Vector3(0, 1f, 0));
    
        if (Input.GetMouseButtonDown(0)) // left click or touch, works for both 
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast from center of the screen 
            RaycastHit hit; //hit information 
            if (Physics.Raycast(ray, out hit)) //if raycast hit : 
            {
                if (hit.transform.gameObject == this.gameObject)  //compares the tag of the hit with the "Player Tag"
                {
                    if (!playerCard.gameObject.activeSelf)
                    {
                        
                        playerCard.SetActive(true);
                    }
                    else
                    {
                        playerCard.SetActive(false);
                        
                    }
                }
            }
        }
    }
   
}