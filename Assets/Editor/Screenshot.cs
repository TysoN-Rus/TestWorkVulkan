using UnityEngine;
using UnityEditor;

public class Screenshot : MonoBehaviour {


    void Start() {
    }

    [MenuItem("MyMenu/Screenshot &p")]
    static void Print() {
        //WebCamTexture webCamTexture;
        //webCamTexture = new WebCamTexture();
        //Camera.main.renderer.material.mainTexture = webCamTexture;
        //webCamTexture = Camera.main.;
        //webCamTexture.deviceName = Camera.main;
        //webCamTexture.Play();




        RenderTexture renderTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24);
        Camera.main.targetTexture = renderTexture;
        Camera.main.Render();
        Camera.main.targetTexture = null;

        RenderTexture.active = renderTexture;

        Texture2D photo = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight, TextureFormat.RGB24, false);
        photo.ReadPixels(new Rect(0, 0, Camera.main.pixelWidth, Camera.main.pixelHeight), 0, 0);
        photo.Apply();
        DestroyImmediate(renderTexture);
        RenderTexture.active = null;

        System.IO.File.WriteAllBytes(ScreenShotName(), photo.EncodeToPNG());
    }

    public static string ScreenShotName() {
        return string.Format("C:/Users/User/Desktop/screen_{0}_{1}.png",
                             Application.productName,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}