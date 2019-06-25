using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler {
    public RawImage output;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".png, .jpg", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        StartCoroutine(OutputRoutine(url));
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start() {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick() {
        // Open file with filter
        var extensions = new[] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        new ExtensionFilter("Sound Files", "mp3", "wav" ),
        new ExtensionFilter("All Files", "*" ),
        };


        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        if (paths.Length > 0) {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
#endif

    private IEnumerator OutputRoutine(string url) {
        var loader = new WWW(url);
        yield return loader;
        output.texture = loader.texture;
        Texture2D texture = new Texture2D(64, 64);

        texture = loader.texture;
        TextureScale.Bilinear(texture, 256, 75);

        //Texture2D whatermarkTexture = Resources.Load("Captura") as Texture2D;
        Texture2D texture2 = GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture as Texture2D;
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        Texture2D texture3 = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        //whatermarkTexture.Resize(256, 50);
        //whatermarkTexture.width = 250;

        int contadorY = 0;
        int contadorX = 0;
        // set the pixel values
        for (int x = texture3.width;  x > 0; x--)
        {
            for (int y = 0; y < texture3.height; y++)
            {

                texture3.SetPixel(x, y, texture2.GetPixel(x, y));
                /*if (texture3.height / 2 > y && texture3.height / 2 - 100 < y)
                {
                    texture3.SetPixel(x, y, texture.GetPixel(x, contadorY));
                    contadorY++;

                }*/
            }
            contadorY = texture3.height / 2-50;
            for (int y = texture.height; y >0 ; y--)
            {

                texture3.SetPixel(x, contadorY, texture.GetPixel(contadorX, y));
                contadorY++;

            }
            contadorX++;

        }
        // Apply all SetPixel calls
        texture3.Apply();
        GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture = texture3;

    }
}