using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestPostData : MonoBehaviour
{
    public 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Upload());
    }

    // Update is called once per frame
    IEnumerator Upload()
    {
        yield return new WaitForEndOfFrame();

        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        yield return null;

        WWWForm form = new WWWForm();
        form.AddField("test", "myData",System.Text.Encoding.UTF8);    
        form.AddField("frameCount", Time.frameCount.ToString());
        form.AddBinaryData("fileUpload", bytes, "screenShot.png", "image/png");

        string url = "http://10.1.23.61:80/TestScore.php";        
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.uploadHandler.progress);
                Debug.Log("Form upload complete!");
            }
        }
    }

    private void Update()
    {
        
    }
}
