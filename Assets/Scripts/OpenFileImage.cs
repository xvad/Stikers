using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class OpenFileImage : MonoBehaviour, IPointerDownHandler {
    public RawImage output;
    private Texture2D originalTexture;
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

        Texture2D texture = loader.texture;
        //resize an image maintaining the aspect ratio
        //calculate the ratio
        double dbl = (double)texture.width / (double)texture.height;
        //new size
        int newWidth = 800/4;
        int newHeight = (int)((double)newWidth / dbl);

        TextureScale.Bilinear(texture, newWidth, newHeight);
        if (UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTexture.name != "")
        {
            originalTexture = UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTexture as Texture2D;
        }
        else
        {
            UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTexture  = originalTexture;

        }
        //Texture2D whatermarkTexture = Resources.Load("Captura") as Texture2D;
        Texture2D texture2 = UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTexture as Texture2D;
        double height = UniversalVar.Instance.Bottle.GetComponent<Collider>().bounds.size.y;
        double width = UniversalVar.Instance.Bottle.GetComponent<Collider>().bounds.size.x;

        newHeight =(int) (14f * dbl);
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        Texture2D texture3 = new Texture2D(800, 800, TextureFormat.ARGB32, false);


        int contadorY = 0;
        int contadorX = 0;
        // set the pixel values
        for (int x = 0;  x < texture3.width; x++)
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
            contadorY = (texture3.height / 2)+ newHeight;
            //Dibuja hacia arriba
            for (int y = 0; y < texture.height; y++)
            {

                texture3.SetPixel(x, contadorY, texture.GetPixel(x, y));
                contadorY++;

            }
            contadorX++;

        }
        // Apply all SetPixel calls
        texture3.Apply();

        UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTexture = texture3;

    }

    
}