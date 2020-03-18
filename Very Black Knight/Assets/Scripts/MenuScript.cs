using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    bool free = true;
    Image myImage;

    void Start() {
        myImage = gameObject.GetComponent<Image>();
    }

    private IEnumerator LoadSceneWithFade(string scene) {

        for (int i = 0; i< 100; i++) {

            myImage.canvasRenderer.SetAlpha(myImage.canvasRenderer.GetAlpha() - 0.05f);
            
            yield return new WaitForSeconds(0.01f);

        }


        SceneManager.LoadScene(scene);


        yield return 0;
    }


    public void loadScene() {

        if (free)
        {

            StartCoroutine(LoadSceneWithFade("HEY"));
            free = false;
        }
    }

}
