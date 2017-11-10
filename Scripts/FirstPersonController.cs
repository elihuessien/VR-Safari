using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {
    //sensitivity speeds for player movements
    float speed, rotspeed;
    
    //makes sure the camera cannot rotate past a certain level.
    float minYrot = -60;
    float maxYrot = 60;
    float rawYrot = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //set basic speeds
        rotspeed = 150f;

        //checks whether walking or sneaking
        if (Input.GetButtonDown("Sneak"))
        {
            speed = 3f;
        }
        else
        {
            speed = 5f;
        }

        //sets the dirrection of movement
        float forwarddir = speed * Input.GetAxis("Horizontal");
        float sidewaydir = speed * Input.GetAxis("Vertical");
        //move player around;
        transform.Translate(forwarddir*Time.deltaTime, 0f,sidewaydir * Time.deltaTime);
        
        //rotate player left and right with mouse
        transform.Rotate(0f, rotspeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0f);


        //rotate the camera up and down with the mouse
        //get camera game object
        Transform camera = transform.GetChild(0);
        //set rotation distance
        rawYrot += rotspeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
        //clamp rotation within the range
        float Yrotation = Mathf.Clamp(rawYrot, minYrot, maxYrot);
        //set rotation
        camera.localEulerAngles = new Vector3(-Yrotation, 0f, 0f);
    }
}
