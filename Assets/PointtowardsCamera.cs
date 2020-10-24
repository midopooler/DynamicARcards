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
        transform.LookAt(PlayerCard);
        transform.LookAt(PlayerCard2);
        transform.LookAt(PlayerCard3);
        // Same as above, but setting the worldUp parameter to Vector3.left in this example turns the camera on its side
        transform.LookAt(PlayerCard, Vector3.left);
        transform.LookAt(PlayerCard2, Vector3.left);
        transform.LookAt(PlayerCard3, Vector3.left);
    }
}
