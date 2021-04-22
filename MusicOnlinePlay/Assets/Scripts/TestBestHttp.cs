using BestHTTP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class TestBestHttp : MonoBehaviour
{
    public GameObject Obj;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            HttpSendMsg();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(hr.State);
        }
    }

    HTTPRequest hr;
    void HttpSendMsg()
    {
        string url = "http://10.1.23.61:80/gfxqfc_Normal.png";
        hr = new HTTPRequest(new System.Uri(url), (request, response) =>
        {
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(response.Data);
            Obj.GetComponent<Renderer>().material.mainTexture = tex;

            Debug.Log(response.DataAsText);

        });
        hr.OnProgress = (originalRequest, ploaded, uploadLength) =>
        {

            Debug.Log(ploaded + "  " + uploadLength);
            float processVal =ploaded / (uploadLength*1f);
            Debug.Log(processVal);
            slider.value = processVal;
        };

        hr.Send();

    }


    void HttpSendStream()
    {
        var request = new HTTPRequest(new System.Uri("http://yourserver.com/bigfile"), (req, resp) => {
            List<byte[]> fragments = resp.GetStreamedFragments();            
            using (FileStream fs = new FileStream("pathToSave", FileMode.Append))
                foreach (byte[] data in fragments) fs.Write(data, 0, data.Length);
            if (resp.IsStreamingFinished) Debug.Log("Download finished!");
        });
        request.UseStreaming = true;
        request.StreamFragmentSize = 1 * 1024 * 1024;// 1 megabyte 
        request.DisableCache = true;// already saving to a file, so turn off caching 

    }
}
