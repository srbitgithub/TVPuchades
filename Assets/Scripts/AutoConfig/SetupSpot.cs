//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class SetupSpot : MonoBehaviour
{
    GameObject spotObject;
    Light spotLight;
    public Color spotColor = Color.white;
    public float spotIntensity = 1;

    // Start is called before the first frame update
    void Start()
    {
        spotObject = GameObject.Find("Spot01");
        spotLight = spotObject.GetComponent<Light>();
        spotLight.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        spotLight.intensity = spotIntensity;
    }
}
