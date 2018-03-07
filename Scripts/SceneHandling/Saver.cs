﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour {
    public string uniqueIdentifier;
    public SaveData saveData;


    protected string key;

    private SceneController sceneController;
	// Use this for initialization
	private void Awake () {
        sceneController = FindObjectOfType<SceneController>();

        //if doesn't exist
        if (!sceneController)
            throw new UnityException(
                "Scene Controller could not be found, Please make sure it exists");
        key = SetKey();
    }
	

	private void OnEnable () {
        sceneController.BeforeSceneUnload += Save;
        sceneController.AfterSceneLoad += Load;
	}
    private void OnDisable()
    {
        sceneController.BeforeSceneUnload -= Save;
        sceneController.AfterSceneLoad -= Load;
    }


    protected abstract string SetKey();
    protected abstract void Save();
    protected abstract void Load();
}
