using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{

    UnityEngine.Video.VideoPlayer myVideoPlayer;
    public string sceneName;
    ulong videoFrameCount;

    // Start is called before the first frame update
    void Start()
    {
        myVideoPlayer = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        videoFrameCount = myVideoPlayer.frameCount;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((long)videoFrameCount <= myVideoPlayer.frame) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1",UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        

    }
}
