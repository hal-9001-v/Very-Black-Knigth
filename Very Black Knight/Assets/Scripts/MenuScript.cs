using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    bool free = true;
    Image myImage;

    void Start()
    {
        myImage = gameObject.GetComponent<Image>();
    }


    public void fadeOutGameObject(GameObject go)
    {

        float seconds = 0.01f;


        if (go.GetComponent<CanvasRenderer>() != null)
        {
            StartCoroutine(FadeOutGameObject(go, seconds));
        }
        else
        {
            Debug.LogError("CanvasRenderer not set in GameObject");
        }

    }


    public void loadScene(GameObject hoi)
    {

        if (free)
        {

            StartCoroutine(LoadSceneWithFade("HEY"));
            free = false;
        }
    }

    public void fadeInGameObject(GameObject go) {

        float seconds = 0.05f;

        if (go.GetComponent<CanvasRenderer>() != null) {
            StartCoroutine(FadeInGameObject(go, seconds));
        }


    }

    private IEnumerator FadeInGameObject(GameObject go, float seconds) {


        CanvasRenderer myCanvasRenderer = go.GetComponent<CanvasRenderer>();
        float alpha = 0;

        while (alpha < 1) {
            
            myCanvasRenderer.SetAlpha(alpha);
            alpha += 0.01f;

            yield return new WaitForSeconds(seconds);
        }

        yield return 0;


    }

    private IEnumerator LoadSceneWithFade(string scene)
    {

        for (int i = 0; i < 100; i++)
        {

            myImage.canvasRenderer.SetAlpha(myImage.canvasRenderer.GetAlpha() - 0.05f);

            yield return new WaitForSeconds(0.01f);

        }


        SceneManager.LoadScene(scene);


        yield return 0;
    }

    private IEnumerator FadeOutGameObject(GameObject go, float seconds)
    {

        CanvasRenderer myCanvasRenderer = go.GetComponent<CanvasRenderer>();
        float alpha = myCanvasRenderer.GetAlpha();

        while (alpha > 0)
        { 
            myCanvasRenderer.SetAlpha(alpha);
            alpha -= 0.01f;

            yield return new WaitForSeconds(seconds);

        }
        yield return 0;
    }


}
