using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{

    public float speed = 2.0f;
    public float distance = 0.2f;

    private Vector3 startposition;

    void Start()
    {
        startposition = transform.position;
    }


    void Update()
    {
        float offset = Mathf.PingPong(Time.time*speed, distance *2) - distance;

        transform.position = new Vector3(startposition.x, startposition.y + offset, startposition.z);
        

        if(gameObject.name == "OldKey")
        {
            transform.Rotate(new Vector3(20,0,0)*Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0,20,0)*Time.deltaTime);
        }
    }

    

}
