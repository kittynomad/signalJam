using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoPlayerd : MonoBehaviour
{
    public string NextScene = "MainMenu";
    public VideoClip toPlay;
    VideoPlayer videoPlayer;
    bool need = false;
    bool alsoNeed = true;
    void Start()
    {
        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = true;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.clip = toPlay;
        videoPlayer.Play();
    }
    void Update()
    {
        if (videoPlayer.isPrepared && !videoPlayer.isPlaying)
        {
            if (need && alsoNeed)
            {
                StartCoroutine(AsyncLoad(NextScene));
                alsoNeed = false;
            }
            else
            {
                need = true;
            }
            
        }
    }
    public void SpeedUp()
    {
        videoPlayer.playbackSpeed *= 1.1f;
    }

    IEnumerator AsyncLoad(string target) // loads scene
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(target);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }
}
