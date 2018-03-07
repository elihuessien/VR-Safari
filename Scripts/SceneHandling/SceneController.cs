using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/*All scene managment scripts come from: 
 https://unity3d.com/learn/tutorials/projects/adventure-game-tutorial/game-state?playlist=44381*/
public class SceneController : MonoBehaviour{

    public event Action BeforeSceneUnload;
    public event Action AfterSceneLoad;
    public CanvasGroup faderCanvasGroup;
    public float fadeDuration = 1f;
    public string startingSceneName = "Main";
    public SaveData playerSaveData;

    private bool isFading;



    // Use this for initialization
    private IEnumerator Start () {
        //start by loading in the main scene
        faderCanvasGroup.alpha = 1f;

        //load the first scene
        yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

        StartCoroutine(Fade(0f));
	}

    // Update is called once per frame
    public void FadeAndLoadScene(String sceneName)
    {
        if(!isFading)
        {
            StartCoroutine(FadeAndSwitchScenes(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScenes (String sceneName)
    {
        //fade to black
        yield return StartCoroutine(Fade(1f));
        //if we have subscribers to this event then tell them
        if(BeforeSceneUnload != null)
        {
            BeforeSceneUnload();
        }


        //Unload current Scene and Load new scene
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

        //if we have subscribers to this event then tell them
        if (AfterSceneLoad != null)
        {
            AfterSceneLoad();
        }

        //fade back in
        yield return StartCoroutine(Fade(0f));
    }


    private IEnumerator LoadSceneAndSetActive (String sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newlyLoadedScene);
    }


    //fade to white while changing scenes
    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;

        float fadeSpeed = Mathf.Abs (faderCanvasGroup.alpha - finalAlpha) / fadeDuration;


        while(!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = 
                Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        isFading = false;
    }
}
