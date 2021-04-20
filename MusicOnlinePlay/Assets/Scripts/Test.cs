using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Test : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
     void Start()
    {              
        //string url = "http://www.my-server.com/audio.ogg";
        url = "http://localhost:80/J4.mp3";
#if !UNITY_EDITOR
        url="http://10.1.23.61:80/J4.mp3";
#endif
    }

    string status ="网络请求状态";
    float _volume = 0.5f;
    private void OnGUI()
    {
        url= GUI.TextField(new Rect(100,100, Screen.width, 100), url);
        GUI.Label(new Rect(100, 200, Screen.width, 100), status);
        if (GUI.Button(new Rect(Screen.width / 2,300, 100, 100), "网络请求声音"))
        {           
            Debug.Log("测试");
            status = "请求中。。。";
            StartCoroutine(GetAudioClip());
        }

        if (GUI.Button(new Rect(Screen.width / 2, 500, 100, 100), "播放声音"))
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.volume = 1f;
            audioSource.Play();
        }

        GUI.Label(new Rect(0, 600, Screen.width, 100), "调节音量");
        _volume = GUI.HorizontalSlider(new Rect(100, 700, Screen.width, 300), _volume, 0, 1);
        audioSource.volume = _volume;
    }
    string url;
    IEnumerator GetAudioClip()
    {

        Debug.Log(url);

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                Debug.Log(www.error);
                status = "请求失败。。"+ www.error;
            }
            else
            {
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                status = "请求成功: "+audioClip.name;
            }
        }
    }

}
