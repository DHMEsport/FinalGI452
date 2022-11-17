using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScreenCapture : MonoBehaviour
{
    //เก็บค่า resolution
    public int capturewidth = 1920;

    public int capturehight = 1080;

    //เก็บค่าไฟล์ที่เป็นนามสกุล
    public enum Format
    {
        Raw,
        Png,
        Jpg,
        PPM
    }

    public Image showImage;
    public Format formats = Format.Jpg;
    private string outputfloder;
    private Rect rect;
    private RenderTexture _renderTexture;
    private Texture2D screenshot;
    public bool processing;
    private byte[] currenttexture;

    public UnityEvent Onshowimage;

    void Start()
    {
        outputfloder = Application.persistentDataPath + "/Screen_Shot";
        if (!Directory.Exists(outputfloder))
        {
            Directory.CreateDirectory(outputfloder);
            Debug.Log("Create" + outputfloder);
        }
    }

    private string CreatefileName(int width, int hight)
    {
        //เก็บเวลาที่cap
        string timestamp = DateTime.Now.ToString("yyyy MMMM dd");
        //เเล้วก็ให้มันประทับลงไปในภาพ
        var filename = string.Format("{0}/screen_{1}x{2}_{3}_{4}", outputfloder, width, hight, timestamp,
            formats.ToString().ToLower());
        return filename;
    }

    public void CaptureScreenShot()
    {
        processing = true;
        if (_renderTexture == null)
        {
            rect = new Rect(0, 0, capturewidth, capturehight);
            _renderTexture = new RenderTexture(capturewidth, capturehight, 24);
            screenshot = new Texture2D(capturewidth, capturehight, TextureFormat.RGB24, false);
        }

        //เอาcammara มาเก็บภาพ
        Camera _camera = Camera.main;
        _camera.targetTexture = _renderTexture;
        _camera.Render();

        //mark render texture 
        RenderTexture.active = _renderTexture;


        screenshot.ReadPixels(rect, 0, 0);

        //reset texture เอาออกจากcammara เมื่อมันเสร็จ
        _camera.targetTexture = null;
        RenderTexture.active = null;
        //get file name
        string filename = CreatefileName((int) rect.width, (int) rect.height);

        //get file header/data size for Image

        byte[] fileheader = null;
        byte[] filedata = null;

        switch (formats)
        {
            case Format.Jpg:
                filedata = screenshot.EncodeToJPG();
                break;
            case Format.Png:
                filedata = screenshot.EncodeToPNG();
                break;
            case Format.Raw:
                filedata = screenshot.GetRawTextureData();
                break;
            case Format.PPM:
                string headerPPM = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
                fileheader = System.Text.Encoding.ASCII.GetBytes(headerPPM);
                filedata = screenshot.GetRawTextureData();
                break;
        }

        currenttexture = filedata;

        //create thread for upload saveing main thread
        new System.Threading.Thread(() =>
        {
            var file = System.IO.File.Create(filename);
            if (fileheader != null)
            {
                file.Write(fileheader, 0, fileheader.Length);
            }

            file.Write(filedata, 0, filedata.Length);
            file.Close();
            Debug.Log(string.Format("save finish {0},size {1}", filename, filedata.Length));
            processing = false;
        }).Start();

        StartCoroutine(ShowImages());

        Destroy(_renderTexture);
        _renderTexture = null;
        screenshot = null;
    }

    public IEnumerator Showimage()
    {
        yield return new WaitForEndOfFrame();
    }

    public void TakeScreenShot()
    {
        if (processing)
        {
            CaptureScreenShot();
        }
        else
        {
            Debug.Log("is processing");
        }
    }

    public IEnumerator ShowImages()
    {
        yield return new WaitForEndOfFrame();
        showImage.material.mainTexture = null;
        Texture2D texture2D = new Texture2D(capturewidth, capturehight, TextureFormat.RGB24, false);
        texture2D.LoadImage(currenttexture);
        showImage.material.mainTexture = texture2D;
        showImage.gameObject.SetActive(true);

        Onshowimage?.Invoke();
        Debug.Log("Show image");
    }
}