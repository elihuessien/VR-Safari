using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Main code comes from:
 * https://www.youtube.com/watch?v=DmhSWEJjphQ&t=71s*/
public class Sun : MonoBehaviour {
    Transform player;
    public float DayLightMinutes;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;

    }
	
	// Update is called once per frame
	void LateUpdate () {
        //reinitialize position to follow the player
       this.transform.position = player.transform.position;


        //I need to allow the sunlight time last as long as the minutes
        //meaning I need the full orbit to be twice as long as the minutes
        float t = 2 * DayLightMinutes * 60;
        transform.rotation *= Quaternion.AngleAxis((360/t) * Time.deltaTime, Vector3.right);
	}
}
