using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointtowardsCamera : MonoBehaviour
{
    public Transform PlayerCard;
    public Transform PlayerCard2;
    public Transform PlayerCard3;


    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        PlayerCard.transform.LookAt(this.gameObject.transform);
        //transform.LookAt(PlayerCard2);
        //transform.LookAt(PlayerCard3);
       
    }
}
