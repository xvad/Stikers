using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text;
using SFB;

public class ManagerFile : MonoBehaviour
{
    public string filePath = "";
    public Texture2D texture;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void OpenFilePanel()
    {

        // Open file with filter
        var extensions = new[] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" ),
        new ExtensionFilter("Sound Files", "mp3", "wav" ),
        new ExtensionFilter("All Files", "*" ),
        };


        var filePath = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true);
        texture = new Texture2D(64, 64);

        var loader = new WWW(new System.Uri(filePath[0]).AbsoluteUri);
        texture = loader.texture;
        TextureScale.Bilinear(texture, 256, 256);
        GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture = texture;


    }
}
