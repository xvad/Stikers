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
        UniversalVar.Instance.Bottle.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
        GameObject[] clones = GameObject.FindGameObjectsWithTag("clone");
        clones[0].GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
        clones[1].GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, offset);
    }

    public void rotateImage(float offset)
    {
        GameObject image = GameObject.Find("OutputImage");
        image.transform.eulerAngles = new Vector3(offset, image.transform.eulerAngles.y, image.transform.eulerAngles.z);
    }

    public void rotateBottles(float offset)
    {
        UniversalVar.Instance.Bottle.transform.eulerAngles = new Vector3(UniversalVar.Instance.Bottle.transform.eulerAngles.x, offset, UniversalVar.Instance.Bottle.transform.eulerAngles.z);
        GameObject[] clones = GameObject.FindGameObjectsWithTag("clone");
        clones[0].transform.eulerAngles = new Vector3(clones[0].transform.eulerAngles.x, offset, clones[0].transform.eulerAngles.z);
        clones[1].transform.eulerAngles = new Vector3(clones[1].transform.eulerAngles.x, offset, clones[1].transform.eulerAngles.z);
    }
}
