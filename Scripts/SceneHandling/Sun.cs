using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Main code comes from:
 * https://www.youtube.com/watch?v=DmhSWEJjphQ&t=71s*/
public class Sun : MonoBehaviour {
    Transform player;
    public float minutes;
    Vector3 center;
    Vector3 position;
    float orbitDistance = 500;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;

        position.x = player.position.x;
        position.y = player.position.y;
        position.z = player.position.z - orbitDistance;
        transform.Translate(position);
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //reinitialize position to follow the player
        //Handles the x axis
        Vector3 position = transform.position;
        position.x = player.position.x;
        transform.position = position;

        //keep sun the orbit distance away from the player
        //Handles following on the z axis
        transform.position = player.position + (transform.position - player.position).normalized * orbitDistance;


        //get time in minutes
        //since I would like the game to go on durring the day,
        //I need to allow the sunlight time last as long as the minutes
        //meaning I need the full orbit to be twice as long as the minutes
        float t = 2 * minutes * 60;
        transform.RotateAround(player.position, Vector3.right, (360/t)* Time.deltaTime);


        transform.LookAt(player.position);
	}
}
