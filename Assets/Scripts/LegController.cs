using System;
using UnityEngine;
using UnityEngine.UI;

public class LegController : MonoBehaviour
{
    public class legsController
    {
        public int Count;
        public Transform[] Legs = new Transform[20];
        public int CurrentLeg = 0;
        public Material CurrentMaterial;
        public Material HighlightMateial;
        public Vector3[] BoundignBox = new Vector3[5];
        public Boolean Panel;
    }

    public Transform[] legsObjects;
    public Text legName;
    public Material highlightMateial;
    int legsCount = 0;
    public Boolean firstTime = true;
    public float maxHeightBase;
    public GameObject furniture;

    public legsController legs = new legsController();

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        legName = furniture.GetComponent<FurnitureAutoConfig>().legName;
        highlightMateial = furniture.GetComponent<FurnitureAutoConfig>().highlightMaterial;
        legs.Legs = transform.GetComponent<FurnitureAutoConfig>().legs;
        int counter = 0;
        for (int i = 0; i < legs.Legs.Length; i++)
        {
            if (legs.Legs[i] != null) counter++;
        }
        legs.Count = counter;
        legs.BoundignBox = transform.GetComponent<FurnitureAutoConfig>().legBoundingBox;
        legs.HighlightMateial = highlightMateial;
        showBase(legs.CurrentLeg);

    }

    public void showBase (int position)
    {
        Transform item;
        for (int i = 0; i < legs.Legs.Length; i++)
        {
            item = legs.Legs[i];
            if (item != null) item.gameObject.SetActive(false);
        }
        legs.CurrentLeg = position;
        getDefaultMaterial();
        openLegPanel();
    }

    public void openLegPanel()
    {
        float newPositionY;
        Transform obj = legs.Legs[legs.CurrentLeg];
        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            newPositionY = legs.BoundignBox[legs.CurrentLeg].y;
            furniture.transform.position = new Vector3(furniture.transform.position.x, 0 - (maxHeightBase - newPositionY), furniture.transform.position.z);
            firstTime = false;
            legName.text = "Base" + (legs.CurrentLeg + 1);
            legHighLight();
        }
    }

    public void nextButtonClicled()
    {
        returnToDefaultMaterial();
        legs.CurrentLeg = Mathf.Clamp(legs.CurrentLeg + 1, 0, legs.Count - 1);
        showBase(legs.CurrentLeg);
        legName.text = "Base" + (legs.CurrentLeg + 1);
        legHighLight();
    }

    public void prevButtonClicled()
    {
        returnToDefaultMaterial();
        legs.CurrentLeg = Mathf.Clamp(legs.CurrentLeg - 1, 0, legs.Count - 1);
        showBase(legs.CurrentLeg);
        legName.text = "Base" + (legs.CurrentLeg + 1);
        legHighLight();
    }

    void getDefaultMaterial()
    {
        Transform obj;
        obj = legs.Legs[legs.CurrentLeg];
        if (obj != null) legs.CurrentMaterial = obj.transform.GetComponent<Renderer>().material;

    }

    public void legHighLight()
    {
        Transform obj;
        getDefaultMaterial();
        obj = legs.Legs[legs.CurrentLeg];
        if (obj != null) obj.transform.GetComponent<Renderer>().material = legs.HighlightMateial;
    }

    public void returnToDefaultMaterial()
    {
        legs.CurrentMaterial = legs.Legs[legs.CurrentLeg].transform.GetComponent<Renderer>().material = legs.CurrentMaterial;

    }

    public void legPanelClose()
    {
        legs.Legs[legs.CurrentLeg].GetComponent<Renderer>().material = legs.CurrentMaterial;
    }
}