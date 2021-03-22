using System;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public class doorsController
    {
        public int Count;
        public GameObject[] DoorsBases = new GameObject[20];
        public float[] DoorAperture = new float[20];
        public float InitialAngle;
        public int CurrentDoor = 0;
        public Material CurrentMaterial;
        public Material HighlightMateial;
        public float leftDoorMinValue = 180f;
        public float leftDoorMaxValue = 265f;
        public float rightDoorMinValue = 95f;
        public float rightDoorMaxValue = 180f;
        public float upDoorMinValue = 0f;
        public float upDoorMaxValue = 85f;
        public float downDoorMinValue = 0f;
        public float downDoorMaxValue = 85f;
        public Boolean Panel;
    }

    public Transform[] doorsBases;
    public Text doorName;
    public Material highlightMateial;
    public Slider apertureDoorSlider;
    int doorsCount = 0;
    public Boolean firstTime = true;
    float minValue;
    float maxValue;
    public doorsController doors = new doorsController();
    public GameObject furniture;

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        apertureDoorSlider = GameObject.FindWithTag("SliderDoor").GetComponent<Slider>();
        doorName = furniture.GetComponent<FurnitureAutoConfig>().doorName;
        highlightMateial = furniture.GetComponent<FurnitureAutoConfig>().highlightMaterial;
        doorsBases = transform.GetComponent<FurnitureAutoConfig>().doorsBase;

        for (int i = 0; i < doorsBases.Length; i++)
        {
            if (doorsBases[i] != null) doorsCount++;
        }

        doors.Count = doorsCount;
        doors.InitialAngle = 180;
        doors.HighlightMateial = highlightMateial;

        for (int i = 0; i < doors.Count; i++)
        {
            doors.DoorsBases[i] = doorsBases[i].gameObject;
            saveCurrentAperture(i);
        }

        firstTime = false;
        openDoorPanel();
    }

    public void openDoorPanel()
    {
        GameObject obj;
        sliderMinAndMaxValues();
        getDoorAperture();
        if (apertureDoorSlider != null) apertureDoorSlider.value = doors.DoorAperture[doors.CurrentDoor];

        obj = doors.DoorsBases[doors.CurrentDoor];

        if (obj != null && obj.name.Contains("right"))
            apertureDoorSlider.value = doors.rightDoorMinValue + (doors.rightDoorMaxValue - doors.DoorAperture[doors.CurrentDoor]);

        doors.Panel = true; 
        if (doorName != null) doorName.text = "Puerta " + (doors.CurrentDoor + 1);
        doorHighLight();
    }

    public void actualizeDoorSelected()
    {
        GameObject selectedDoor = doors.DoorsBases[doors.CurrentDoor];

        if (selectedDoor.name.Contains("left"))
            selectedDoor.transform.localRotation = Quaternion.Euler(0, apertureDoorSlider.value, 0);

        if (selectedDoor.name.Contains("right"))
            selectedDoor.transform.localRotation = Quaternion.Euler(0, doors.InitialAngle - (apertureDoorSlider.value) + doors.rightDoorMinValue, 0);

        if (selectedDoor.name.Contains("up"))
            selectedDoor.transform.localRotation = Quaternion.Euler(apertureDoorSlider.value * -1, 0, 0);

        if (selectedDoor.name.Contains("down"))
            selectedDoor.transform.localRotation = Quaternion.Euler(apertureDoorSlider.value, 0, 0);
    }

    public void getDoorAperture()
    {
        if (apertureDoorSlider != null) apertureDoorSlider.value = doors.DoorAperture[doors.CurrentDoor];
    }

    public void nextButtonClicked()
    {
        saveCurrentAperture(doors.CurrentDoor);
        returnToDefaultMaterial();
        doors.CurrentDoor = Mathf.Clamp(doors.CurrentDoor + 1, 0, doors.Count -1);
        openDoorPanel();
    }

    public void prevButtonClicked()
    {
        saveCurrentAperture(doors.CurrentDoor);
        returnToDefaultMaterial();
        doors.CurrentDoor = Mathf.Clamp(doors.CurrentDoor - 1, 0, doors.Count - 1);
        openDoorPanel();
    }

    public void returnToDefaultMaterial()
    {
        doors.CurrentMaterial = doors.DoorsBases[doors.CurrentDoor].transform.GetChild(0).transform.GetComponent<Renderer>().material = doors.CurrentMaterial;

    }

    void getDefaultMaterial()
    {
        GameObject obj;
        obj = doors.DoorsBases[doors.CurrentDoor];
        if (obj != null) doors.CurrentMaterial = obj.transform.GetChild(0).transform.GetComponent<Renderer>().material;
    }

    void saveCurrentAperture(int position)
    {
        string doorName = doors.DoorsBases[position].name;
        doors.DoorAperture[position] = apertureDoorSlider.value;

        if (doorName.Contains("right"))
            doors.DoorAperture[position] = doors.DoorsBases[position].transform.localRotation.eulerAngles.y;
    }
    void setSliderMinMaxValues(float min, float max)
    {
        apertureDoorSlider.minValue = min;
        apertureDoorSlider.maxValue = max;
    }

    void sliderMinAndMaxValues()
    {
        string doorName;
        GameObject obj = doors.DoorsBases[doors.CurrentDoor];
        if (obj != null)
        {
            doorName = obj.name;
            if (doorName.Contains("right"))
                setSliderMinMaxValues(doors.rightDoorMinValue, doors.rightDoorMaxValue);

            if (doorName.Contains("left"))
                setSliderMinMaxValues(doors.leftDoorMinValue, doors.leftDoorMaxValue);

            if (doorName.Contains("up"))
                setSliderMinMaxValues(doors.upDoorMinValue, doors.upDoorMaxValue);

            if (doorName.Contains("down"))
                setSliderMinMaxValues(doors.downDoorMinValue, doors.downDoorMaxValue);
        }
    }

    public void openAll()
    {
        int currentDoor = doors.CurrentDoor;
        for (int i = 0; i < doors.Count; i++)
        {
            doors.CurrentDoor = i;
            string doorName = doors.DoorsBases[doors.CurrentDoor].name;
            sliderMinAndMaxValues();
            apertureDoorSlider.value = apertureDoorSlider.maxValue;
            if (doorName.Contains("right"))
                apertureDoorSlider.value = apertureDoorSlider.maxValue;
            actualizeDoorSelected();
            saveCurrentAperture(i);
        }
        doors.CurrentDoor = currentDoor;
        sliderMinAndMaxValues();
    }

    public void closeAll()
    {
        int currentDoor = doors.CurrentDoor;
        for (int i = 0; i < doors.Count; i++)
        {
            doors.CurrentDoor = i;
            string doorName = doors.DoorsBases[doors.CurrentDoor].name;
            sliderMinAndMaxValues();
            apertureDoorSlider.value = apertureDoorSlider.minValue;
            if (doorName.Contains("right"))
                apertureDoorSlider.value = apertureDoorSlider.minValue;
            actualizeDoorSelected();
            saveCurrentAperture(i);
        }
        doors.CurrentDoor = currentDoor;
        sliderMinAndMaxValues();
    }

    public void doorHighLight()
    {
        GameObject obj;
        getDefaultMaterial();
        obj = doors.DoorsBases[doors.CurrentDoor];
        if (obj != null) obj.transform.GetChild(0).transform.GetComponent<Renderer>().material = doors.HighlightMateial;
    }

    public void doorPanelClose()
    {
        doors.DoorsBases[doors.CurrentDoor].transform.GetChild(0).transform.GetComponent<Renderer>().material = doors.CurrentMaterial;
    }
}