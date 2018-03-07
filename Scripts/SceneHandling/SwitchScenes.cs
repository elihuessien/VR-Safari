using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScenes : MonoBehaviour {

    private Sun sun;
    private float time;
    public string sceneToLoad;

	// Use this for initialization
	void Start () {
        sun = FindObjectOfType<Sun>();
        //get gameplay period in minutes
        time = sun.minutes;
        time *= 60;
	}
	
	// Update is called once per frame
	void Update () {
        //get the time for 
        time -= Time.deltaTime;
        if (time < 0)
            SwitchScene();
	}

    void SwitchScene ()
    {
        //load new scene
        SceneController sc = FindObjectOfType<SceneController>();
        sc.FadeAndLoadScene(sceneToLoad);
    }
}
