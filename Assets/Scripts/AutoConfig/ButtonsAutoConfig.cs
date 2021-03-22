using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAutoConfig : MonoBehaviour
{
    public class bodiesController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }
    public class doorsController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }

    public class drawersController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }

    public class legsController
    {
        public Material[] materials = new Material[10];
        public Transform[] objects = new Transform[10];
        public int Current;
    }

    public bodiesController bodies = new bodiesController();
    public doorsController doors = new doorsController();
    public drawersController drawers = new drawersController();
    public legsController legs = new legsController();
   
    public GameObject furniture;
    public GameObject decoration;

    public GameObject decorationButton;
    public GameObject doorsButton;
    public GameObject drawersButton;
    public GameObject legButton;
    public GameObject texturesButton;
    public GameObject zoomButton;
    public GameObject rotateButton;

    public GameObject doorsPanel;
    public GameObject drawersPanel;
    public GameObject basesPanel;
    public GameObject texturesPanel;
    public GameObject zoomPanel;
    public Text modeText;

    float lastWidthResolution = Screen.width;
    public Material highlightMaterial;
    public GameObject cameraController;
    public String currentPanel = "rotate";

    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        decoration = GameObject.FindWithTag("Decoration");
        decorationButton = GameObject.FindWithTag("DecorationButtonMain");
        doorsButton = GameObject.FindWithTag("DoorsButtonMain");
        drawersButton = GameObject.FindWithTag("DrawersButtonMain");
        legButton = GameObject.FindWithTag("LegsButtonMain");
        texturesButton = GameObject.FindWithTag("TexturesButtonMain");
        zoomButton = GameObject.FindWithTag("ZoomButtonMain");
        rotateButton = GameObject.FindWithTag("RotateButtonMain");
        GameObject mdText = GameObject.FindWithTag("ModeTextMain");
        modeText = mdText.GetComponent<Text>();

        doorsPanel = GameObject.FindWithTag("DoorsPanel");
        drawersPanel = GameObject.FindWithTag("DrawersPanel");
        basesPanel = GameObject.FindWithTag("BasesPanel");
        texturesPanel = GameObject.FindWithTag("TexturesPanel");
        zoomPanel = GameObject.FindWithTag("ZoomPanel");

        //highlightMaterial = furniture.GetComponent<FurnitureAutoConfig>().highlightMaterial;
        cameraController = GameObject.FindWithTag("CameraController");

        redrawView();
        //doors.objects = furniture.transform.GetComponent<FurnitureAutoConfig>().doorsBase;
        //doors.materials = getObjectsMaterial(doors.objects);

        //drawers.objects = furniture.transform.GetComponent<FurnitureAutoConfig>().drawersBase;
        //drawers.materials = getObjectsMaterial(drawers.objects);


        //legs.objects = furniture.transform.GetComponent<FurnitureAutoConfig>().legs;
        //legs.materials = getObjectsMaterial(legs.objects);

        //bodies.objects = furniture.transform.GetComponent<FurnitureAutoConfig>().bodies;
        //bodies.materials = getObjectsMaterial(bodies.objects);

        rotateButtonClicked();
    }

    Material[] getObjectsMaterial(Transform[] objectsArray)
    {
        Material[] materialArray = new Material[10];
        Transform item;
        for (int i = 0; i < objectsArray.Length; i++)
        {
            item = objectsArray[i];
            if (item != null)
            {
                if (item.childCount > 0)
                    materialArray[i] = item.GetChild(0).transform.GetComponent<Renderer>().material;
                else
                    materialArray[i] = item.GetComponent<Renderer>().material;
            }
        }
        return materialArray;
    }

    void Update()
    {
        float currentScreenWidth = Screen.width;
        if (lastWidthResolution != currentScreenWidth)
        {
            redrawView();
            lastWidthResolution = currentScreenWidth;
            modeText.transform.position = new Vector2(currentScreenWidth - 170, 20);
        }
    }

    Material[] getMaterials(Transform[] objects, Material[] materials)
    {
        Transform item;
        Material getItemMaterial;
        for (int i = 0; i < objects.Length; i++)
        {
            item = objects[i];
            if (item != null)
            {
                if (item.childCount > 0)
                {
                    getItemMaterial = item.GetChild(0).transform.GetComponent<Renderer>().material;
                }
                else
                {
                    getItemMaterial = item.transform.GetComponent<Renderer>().material;
                }
                materials[i] = getItemMaterial;
            }
        }
        return materials;
    }

    void redrawView()
    {
        float widthResolution = Screen.width;
        int buttonsWidth = 100;
        int spaceBetweenButtons = 10;
        int rightMargin = 5;
        float initPoint = 0 + (buttonsWidth / 2.0f) + rightMargin;
        decorationButton.transform.position = new Vector2(initPoint, doorsButton.transform.position.y);
        doorsButton.transform.position = new Vector2(decorationButton.transform.position.x + buttonsWidth + spaceBetweenButtons, doorsButton.transform.position.y);
        drawersButton.transform.position = new Vector2(doorsButton.transform.position.x + buttonsWidth + spaceBetweenButtons, drawersButton.transform.position.y);
        legButton.transform.position = new Vector2(drawersButton.transform.position.x + buttonsWidth + spaceBetweenButtons, legButton.transform.position.y);
        texturesButton.transform.position = new Vector2(legButton.transform.position.x + buttonsWidth + spaceBetweenButtons, texturesButton.transform.position.y);
        zoomButton.transform.position = new Vector2(texturesButton.transform.position.x + buttonsWidth + spaceBetweenButtons, zoomButton.transform.position.y);
        rotateButton.transform.position = new Vector2(zoomButton.transform.position.x + buttonsWidth + spaceBetweenButtons, rotateButton.transform.position.y);
    }

    void enableDisableButtons(Boolean doorsBtn, Boolean drawersBtn, Boolean legsBtn, Boolean texturesBtn, Boolean zoomBtn, Boolean rotateBtn)
    {
        doorsButton.GetComponent<Button>().interactable = doorsBtn;
        drawersButton.GetComponent<Button>().interactable = drawersBtn;
        legButton.GetComponent<Button>().interactable = legsBtn;
        texturesButton.GetComponent<Button>().interactable = texturesBtn;
        zoomButton.GetComponent<Button>().interactable = zoomBtn;
        rotateButton.GetComponent<Button>().interactable = rotateBtn;
    }

    public void doorButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        //if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "base") returnToCurrentMaterials();
        if (currentPanel == "textures") returnFromTextures();
        enableDisableCameraControllers(false);
        enableDisableFurnitureScripts(true, false, false, false);
        currentPanel = "door";
        closePanels();
        doorsPanel.SetActive(true);
        enableDisableButtons(false, true, true, true, true, true);
        modeText.text = "Abrir/Cerrar Puertas";
        furniture.GetComponent<DoorController>().openDoorPanel();
    }

    public void drawerButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        //if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "base") returnToCurrentMaterials();
        if (currentPanel == "textures") returnFromTextures();
        enableDisableCameraControllers(false);
        enableDisableFurnitureScripts(false, true, false, false);
        currentPanel = "drawer";
        closePanels();
        drawersPanel.SetActive(true);
        enableDisableButtons(true, false, true, true, true, true);
        modeText.text = "Abrir/Cerrar Cajones";
        furniture.GetComponent<DrawerController>().openDrawerPanel();
    }

    public void legButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        //if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "base") returnToCurrentMaterials();
        if (currentPanel == "textures") returnFromTextures();
        enableDisableCameraControllers(false);
        enableDisableFurnitureScripts(false, false, true, false);
        currentPanel = "base";
        closePanels();
        basesPanel.SetActive(true);
        enableDisableButtons(true, true, false, true, true, true);
        modeText.text = "Base";
        //furniture.GetComponent<LegController>().showBase(furniture.GetComponent<LegController>().legs.CurrentLeg);
        furniture.GetComponent<LegController>().openLegPanel();
    }

    public void textureButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "textures") returnFromTextures();

        enableDisableCameraControllers(false);
        enableDisableFurnitureScripts(false, false, false, true);
        currentPanel = "textures";
        closePanels();
        texturesPanel.SetActive(true);
        enableDisableButtons(true, true, true, false, true, true);
        modeText.text = "Modo Texturas";
    }

    public void zoomButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "textures") returnFromTextures();

        enableDisableCameraControllers(false);
        enableDisableFurnitureScripts(false, false, false, false);
        closePanels();
        zoomPanel.SetActive(true);
        enableDisableButtons(true, true, true, true, false, true);
        modeText.text = "Modo Zoom";
    }

    public void rotateButtonClicked()
    {
        if (currentPanel == "body" || currentPanel == "door" || currentPanel == "drawer") returnToCurrentMaterials();
        if (currentPanel == "base") returnToCurrentMaterialsArray(legs.objects, legs.materials);
        if (currentPanel == "textures") returnFromTextures();

        enableDisableButtons(true, true, true, true, true, false);
        enableDisableFurnitureScripts(false, false, false, false);
        modeText.text = "Modo Rotación";
        closePanels();
        enableDisableCameraControllers(true);
    }

    public void closePanels()
    {
        doorsPanel.SetActive(false);
        drawersPanel.SetActive(false);
        basesPanel.SetActive(false);
        texturesPanel.SetActive(false);
        zoomPanel.SetActive(false);
    }

    public void returnToCurrentMaterials()
    {
        if (currentPanel == "door") furniture.GetComponent<DoorController>().doorPanelClose();
        if (currentPanel == "door") furniture.GetComponent<DoorController>().doorPanelClose();

        if (currentPanel == "drawer") furniture.GetComponent<DrawerController>().drawerPanelClose();
        if (currentPanel == "base") furniture.GetComponent<LegController>().legPanelClose();

        if (currentPanel == "texture")
        {
            Transform kko = furniture.GetComponent<TextureController>().textures.objectToBackMaterial;
            if (kko.GetComponent<Renderer>().material == furniture.GetComponent<TextureController>().highlightMateial)
                kko.GetComponent<Renderer>().material = furniture.GetComponent<TextureController>().textures.backMaterial;
            /*string currentOpenPanel = furniture.GetComponent<TextureController>().textures.Panel;
            for (int i = 0; i < doors.objects.Length; i++)
            {
                print(doors.materials[i].name);
            }*/
            /*if (currentOpenPanel == "body") returnToCurrentMaterialsArray(bodies.objects, bodies.materials);
            if (currentOpenPanel == "door") returnToCurrentMaterialsArray(doors.objects, doors.materials);
            if (currentOpenPanel == "drawer") returnToCurrentMaterialsArray(drawers.objects, drawers.materials);
            if (currentOpenPanel == "leg") returnToCurrentMaterialsArray(legs.objects, legs.materials);*/
        }
    }

    public void returnToCurrentMaterialsArray(Transform[] objs, Material[] materialsArray)
    {
        Transform item;
        for (int i = 0; i < objs.Length; i++)
        {
            item = objs[i];
            if (item != null)
            {
                if (item.childCount > 0)
                    item.GetChild(0).GetComponent<Renderer>().material = materialsArray[i];
                else
                    item.GetComponent<Renderer>().material = materialsArray[i];
            }
        }
    }

    public void returnFromTextures()
    {
        /*returnToCurrentMaterialsArray(doors.objects, doors.materials);
        returnToCurrentMaterialsArray(drawers.objects, drawers.materials);
        returnToCurrentMaterialsArray(legs.objects, legs.materials);
        returnToCurrentMaterialsArray(bodies.objects, bodies.materials);*/
        Transform kko = furniture.GetComponent<TextureController>().textures.objectToBackMaterial;
        if (kko.GetComponent<Renderer>().material.color == furniture.GetComponent<TextureController>().highlightMateial.color)
            kko.GetComponent<Renderer>().material = furniture.GetComponent<TextureController>().textures.backMaterial;
    }

    void enableDisableCameraControllers(Boolean enableDisable)
    {
        cameraController.GetComponent<RotateMode>().enabled = enableDisable;
        cameraController.GetComponent<ZoomMode>().enabled = enableDisable;
        cameraController.GetComponent<PanMode>().enabled = enableDisable;
    }

    void enableDisableFurnitureScripts(Boolean doorsValue, Boolean drawersValue, Boolean basesValue, Boolean texturesValue)
    {
        furniture.GetComponent<DoorController>().enabled = doorsValue;
        furniture.GetComponent<DrawerController>().enabled = drawersValue;
        furniture.GetComponent<LegController>().enabled = basesValue;
        furniture.GetComponent<TextureController>().enabled = texturesValue;
    }

    public void decorationButtonClicked()
    {
        decoration.SetActive(!decoration.activeSelf);
    }

}
