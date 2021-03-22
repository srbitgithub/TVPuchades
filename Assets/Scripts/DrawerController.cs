using System;
using UnityEngine;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour
{
    public class drawersController
    {
        public int Count;
        public GameObject[] DrawersBases = new GameObject[20];
        public float[] DrawerAperture = new float[20];
        public int CurrentDrawer = 0;
        public Material CurrentMaterial;
        public Material HighlightMateial;
        public float slimDrawerMin;
        public float slimDrawerMax;
        public float fatDrawerMin;
        public float fatDrawerMax;
        public Boolean Panel;
    }

    public Transform[] drawerBases;
    public Text drawerName;
    public Material highlightMateial;
    public Slider apertureDrawerSlider;
    int drawersCount = 0;
    public float slimDrawerClose;
    public float slimDrawerOpen;
    public float fatDrawerClose;
    public float fatDrawerOpen;
    float positonDrawer = 0.0f;
    public Boolean firstTime = true;
    public GameObject furniture;

    public drawersController drawers = new drawersController();

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        apertureDrawerSlider = GameObject.FindWithTag("SliderDrawer").GetComponent<Slider>();

        drawerName = furniture.GetComponent<FurnitureAutoConfig>().drawerName;
        highlightMateial = furniture.GetComponent<FurnitureAutoConfig>().highlightMaterial;

        drawerBases = transform.GetComponent<FurnitureAutoConfig>().drawersBase;
        for (int i = 0; i < drawerBases.Length; i++)
        {
            if (drawerBases[i] != null) drawersCount++;
        }

        drawers.Count = drawersCount;
        drawers.HighlightMateial = highlightMateial;

        for (int i = 0; i < drawers.Count; i++)
        {
            drawers.DrawersBases[i] = drawerBases[i].gameObject;
            saveCurrentAperture(i);
        }

        drawers.CurrentDrawer = 0;

        drawers.slimDrawerMin = slimDrawerClose;
        drawers.slimDrawerMax = slimDrawerOpen;
        drawers.fatDrawerMin = fatDrawerClose;
        drawers.fatDrawerMax = fatDrawerOpen;

        firstTime = false;
        openDrawerPanel();
    }

    public void openDrawerPanel()
    {
        if (drawerName != null) apertureDrawerSlider.value = drawers.DrawerAperture[drawers.CurrentDrawer];
        setMinMaxSlider();
        drawers.Panel = true;
        drawerHighLight();
        if (drawerName != null) drawerName.text = "Cajón " + (drawers.CurrentDrawer + 1);
    }

    public void actualizeDrawerSelected()
    {
        Vector3 currentSelectedDrawerPosition = drawers.DrawersBases[drawers.CurrentDrawer].transform.localPosition;
        Vector3 newSelectedDrawerPosition;
        newSelectedDrawerPosition = new Vector3 (currentSelectedDrawerPosition.x, currentSelectedDrawerPosition.y, apertureDrawerSlider.value);
        drawers.DrawersBases[drawers.CurrentDrawer].transform.localPosition = newSelectedDrawerPosition;
        saveCurrentAperture(drawers.CurrentDrawer);
    }

    void saveCurrentAperture(int position)
    {
        drawers.DrawerAperture[position] = drawers.DrawersBases[position].transform.localPosition.z;
    }

    void getDefaultMaterial()
    {
        GameObject obj = drawers.DrawersBases[drawers.CurrentDrawer];
        if (obj != null)
        {
            drawers.CurrentMaterial = obj.transform.GetChild(0).transform.GetComponent<Renderer>().material;
        }
    }

    public void drawerHighLight()
    {
        getDefaultMaterial();
        GameObject obj = drawers.DrawersBases[drawers.CurrentDrawer];
        if (obj != null)
        {
            obj.transform.GetChild(0).transform.GetComponent<Renderer>().material = drawers.HighlightMateial;
        }
    }

    public void setMinMaxSlider()
    {
        string drawerName;
        GameObject obj = drawers.DrawersBases[drawers.CurrentDrawer];
        if (obj != null)
        {
            drawerName = obj.name;
            if (drawerName.Contains("slim"))
            {
                apertureDrawerSlider.minValue = drawers.slimDrawerMin;
                apertureDrawerSlider.maxValue = drawers.slimDrawerMax;
            }
            else
            {
                apertureDrawerSlider.minValue = drawers.fatDrawerMin;
                apertureDrawerSlider.maxValue = drawers.fatDrawerMax;
            }
        }
    }

    public void returnToDefaultMaterial()
    {
        drawers.CurrentMaterial = drawers.DrawersBases[drawers.CurrentDrawer].transform.GetChild(0).transform.GetComponent<Renderer>().material = drawers.CurrentMaterial;
    }

    public void nextButtonClicked()
    {
        returnToDefaultMaterial();
        saveCurrentAperture(drawers.CurrentDrawer);
        drawers.CurrentDrawer = Mathf.Clamp(drawers.CurrentDrawer + 1, 0, drawers.Count - 1);
        setMinMaxSlider();
        openDrawerPanel();
    }

    public void PrevButtonClicked()
    {
        returnToDefaultMaterial();
        saveCurrentAperture(drawers.CurrentDrawer);
        drawers.CurrentDrawer = Mathf.Clamp(drawers.CurrentDrawer -1, 0, drawers.Count - 1);
        setMinMaxSlider();
        openDrawerPanel();
    }

    public void openCloseAll(string openClose)
    {
        int currentDrawer = drawers.CurrentDrawer;
        for (int i = 0; i < drawers.Count; i++)
        {
            drawers.CurrentDrawer = i;
            setMinMaxSlider();
            if (openClose == "open") apertureDrawerSlider.value = apertureDrawerSlider.maxValue;
            if (openClose == "close") apertureDrawerSlider.value = apertureDrawerSlider.minValue;
            actualizeDrawerSelected();
            saveCurrentAperture(i);
        }
        drawers.CurrentDrawer = currentDrawer;
    }
    public void drawerPanelClose()
    {
        drawers.DrawersBases[drawers.CurrentDrawer].transform.GetChild(0).transform.GetComponent<Renderer>().material = drawers.CurrentMaterial;
    }
}
