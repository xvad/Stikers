using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Camera camera;
    public GameObject arrow;
    public GameObject bottlesContainer;
    private string nameBottle = "Bottle_";
    private int numberBottle = 1;
    private int step = 1;
    public GameObject panelStep1;
    public GameObject panelStep2;
    private Vector3 positionCamera;
    // Start is called before the first frame update
    void Start()
    {
        positionCamera = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified
                string nameMesh = hit.collider.name;
                GameObject bottle = GameObject.Find(nameMesh);
                if (nameMesh.Contains(nameBottle))
                {
                    numberBottle = System.Int32.Parse(nameMesh.Split('_')[1]);
                    //change position
                    arrow.transform.position = new Vector3(bottle.transform.position.x, arrow.transform.position.y, arrow.transform.position.z);

                }
            }
        }
    }

    public void SelectBottleUI(int direction)
    {

        string nameMesh = nameBottle + (numberBottle + direction).ToString();
        GameObject bottle = GameObject.Find(nameMesh);

        //if exist
        if (bottle != null)
        {

            //change position
            arrow.transform.position = new Vector3(bottle.transform.position.x, arrow.transform.position.y, arrow.transform.position.z);
            numberBottle = numberBottle + direction;
        }
    }


    public void NextStep()
    {
        if(step!= 1)
        {
            return;
        }
        panelStep1.SetActive(false);
        panelStep2.SetActive(true);
        string nameMesh = nameBottle + (numberBottle).ToString();
        GameObject bottle = GameObject.Find(nameMesh);
        UniversalVar.Instance.Bottle = bottle;

        camera.transform.position = new Vector3(bottle.transform.position.x+0.25f, camera.transform.position.y, camera.transform.position.z+1.3f);

        step++;
        int children = bottlesContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (!bottlesContainer.transform.GetChild(i).gameObject.name.Contains(numberBottle.ToString()))
            {
                bottlesContainer.transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }

    public void PrevStep()
    {
        if (step != 2)
        {
            return;
        }
        panelStep1.SetActive(true);
        panelStep2.SetActive(false);
        camera.transform.position = positionCamera;
        step--;
        int children = bottlesContainer.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            bottlesContainer.transform.GetChild(i).gameObject.SetActive(true);

        }
    }


}
