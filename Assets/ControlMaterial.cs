using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveTexture(float offset)
    {
        Debug.Log(offset);
        UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
    }
}
