using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunRandom : MonoBehaviour
{
    public float speed;
    public Transform[] movespots;
    private int randomSpot;
    public float startWaitTime;
    private float waitTime;
    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, movespots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(movespots[randomSpot]);
        transform.position = Vector3.MoveTowards(transform.position, movespots[randomSpot].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movespots[randomSpot].position) < 0.2f)
        {    
        
        
            if (waitTime <= 0)
            { randomSpot = Random.Range(0, movespots.Length);
                waitTime = startWaitTime;
            }
            else
            { waitTime = -Time.deltaTime; }
           
        }
    }
}
