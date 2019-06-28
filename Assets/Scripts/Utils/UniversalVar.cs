using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalVar : MonoBehaviour
{
    private static UniversalVar instance;

    public static UniversalVar Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UniversalVar();
            }
            return instance;
        }
    }
    private static GameObject bottle;

    public GameObject   Bottle { get { return bottle; } set { bottle = value; } }
}
