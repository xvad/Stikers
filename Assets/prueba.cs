using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AddWatermark();
       //Debug.Log(GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWatermark()
    {
        
        Texture2D whatermarkTexture = Resources.Load("Captura") as Texture2D;
        Texture2D texture2 = GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture as Texture2D;
        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        Texture2D texture = new Texture2D(256, 256, TextureFormat.ARGB32, false);
        //whatermarkTexture.Resize(256, 50);
        //whatermarkTexture.width = 250;
        // set the pixel values
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                texture.SetPixel(x, y, texture2.GetPixel(x, y));
                if (texture.height / 2 > y && texture.height / 2 -50 < y)
                {
                    texture.SetPixel(x, y, Color.red);

                }
            }
        }

        texture.SetPixel(1, 0, Color.clear);
        texture.SetPixel(0, 1, Color.white);
        texture.SetPixel(1, 1, Color.black);

        // Apply all SetPixel calls
        texture.Apply();

        // connect texture to material of GameObject this script is attached to
        GameObject.Find("Bottle_3").GetComponent<Renderer>().material.mainTexture = texture;


        //int startX = 0;
        //int startY = background.height;

        //for (int x = startX; x < background.width; x++)
        //{

        //    for (int y = startY; y < background.height; y++)
        //    {
        //        Color bgColor = background.GetPixel(x, y);
        //        Color wmColor = watermark.GetPixel(x - startX, y - startY);

        //        Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

        //        background.SetPixel(x, y, final_color);
        //    }
        //}

        //background.Apply();
    }
}
