//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TextureController : MonoBehaviour
{
    [Header("PANEL BUTTONS")]
    public Button[] texturesButtons = new Button[10];
    public GameObject nextButton;
    public GameObject prevButton;
    public Button bodyButton;
    public Button doorButton;
    public Button drawerButton;
    public Button legButton;
    public Button elementsButton;
    public Text textLabel;
    public Material highlightMateial;
    public Material currentMaterial;
    [Header("IMAGE BUTTONS (SPRITES)")]
    public Sprite[] bodiesMaterialsSprites = new Sprite[10];
    public Sprite[] doorsMaterialsSprites = new Sprite[10];
    public Sprite[] drawersMaterialsSprites = new Sprite[10];
    public Sprite[] elememtsMaterialsSprites = new Sprite[10];
    public Sprite[] legsMaterialsSprites = new Sprite[5];
    public Sprite[] knobsMaterialsSprites = new Sprite[5];
    public Sprite spriteNone;
    [Header("MATERIALS")]
    public Material[] bodiesMaterials;
    public Material[] doorsMaterials;
    public Material[] drawersMaterials;
    public Material[] legsMaterials;
    public Material[] elementsMaterials;
    public Material[] knobsMaterials;

    public class texturesController
    {
        public Transform[] bodiesObjects = new Transform[20];
        public Transform[] drawersObjects = new Transform[10];
        public Transform[] doorsObjects = new Transform[10];
        public Transform[] elementsObjects = new Transform[10];
        public Transform[] legsObjects = new Transform[5];

        public Material[] bodieObjectTexture = new Material[20];
        public Material[] drawersObjectTexture = new Material[10];
        public Material[] doorsObjectTexture = new Material[10];
        public Material[] elementsObjetTexture = new Material[10];
        public Material[] legsObjectTexture = new Material[5];

        public int bodiesCounter = 0;
        public int doorsCounter = 0;
        public int drawersCounter = 0;
        public int elementsCounter = 0;
        public int legsCounter = 0;

        public int bodiesCount;
        public int doorsCount;
        public int drawersCount;
        public int legsCount;
        public int elementsCount;

        public Transform objectToBackMaterial;
        public Material backMaterial;
        public string Panel;
    }

    public class textureButtons
    {
        public Vector2[] ButtonPosition = new Vector2[10];
        public int Position = 0;
    }

    public textureButtons btnPosition = new textureButtons();

    Transform currentObject;
    public texturesController textures = new texturesController();
    public Transform[] bodiesBases;
    public Transform[] drawersBases;
    public Transform[] doorsBases;
    public Transform[] elementsBases;
    public Transform[] legsBases;
    public GameObject[] finishedButtons = new GameObject[10];
    public GameObject selectedMaterialButton;
    public GameObject overMaterialButton;
    public GameObject furniture;
    int theCount = 0;


    void Start()
    {
        furniture = GameObject.FindWithTag("Furniture");
        highlightMateial = furniture.GetComponent<FurnitureAutoConfig>().highlightMaterial;

        initializedArrays();

        textures.bodiesCount = elementCounter(textures.bodiesObjects);
        textures.doorsCount = elementCounter(textures.doorsObjects);
        textures.drawersCount = elementCounter(textures.drawersObjects);
        textures.legsCount = elementCounter(textures.legsObjects);

        textures.objectToBackMaterial = textures.bodiesObjects[0];
        textures.backMaterial = textures.bodiesObjects[0].GetComponent<Renderer>().material;
        textLabel = furniture.GetComponent<FurnitureAutoConfig>().textLabel;
        bodyClicked();
    }

    void initializedArrays()
    {
        doorsMaterialsSprites = bodiesMaterialsSprites;
        drawersMaterialsSprites = bodiesMaterialsSprites;
        elememtsMaterialsSprites = bodiesMaterialsSprites;

        bodiesMaterials = GetComponent<FurnitureAutoConfig>().bodiesMaterials;
        doorsMaterials = GetComponent<FurnitureAutoConfig>().doorsMaterials;
        drawersMaterials = GetComponent<FurnitureAutoConfig>().drawersMaterials;
        elementsMaterials = GetComponent<FurnitureAutoConfig>().elementsMaterials;
        legsMaterials = GetComponent<FurnitureAutoConfig>().legsMaterials;
        knobsMaterials = GetComponent<FurnitureAutoConfig>().knobsMaterials;

        textures.bodiesObjects = GetComponent<FurnitureAutoConfig>().bodies;
        textures.doorsObjects = GetComponent<FurnitureAutoConfig>().doorsBase;
        textures.drawersObjects = GetComponent<FurnitureAutoConfig>().drawersBase;
        textures.elementsObjects = GetComponent<FurnitureAutoConfig>().elements;
        textures.legsObjects = GetComponent<FurnitureAutoConfig>().legs;

        textures.bodieObjectTexture = getTextureObjects(textures.bodiesObjects);
        textures.doorsObjectTexture = getTextureObjects(textures.doorsObjects);
        textures.drawersObjectTexture = getTextureObjects(textures.drawersObjects);
        textures.legsObjectTexture = getTextureObjects(textures.legsObjects);
        textures.elementsObjetTexture = getTextureObjects(textures.elementsObjects);

        getTextureButtonsPosition();
    }

    void getTextureButtonsPosition()
    {
        for (int i = 0; i < finishedButtons.Length; i++)
        {
            btnPosition.ButtonPosition[i] = finishedButtons[i].transform.localPosition;
        }
    }

    Material[] getTextureObjects(Transform[] objects)
    {
        Material[] arrayToReturn = new Material[20];
        Transform item;
        for (int i = 0; i < objects.Length; i++)
        {
            item = objects[i];
            if (item != null)
            {
                if (item.childCount > 0)
                    arrayToReturn[i] = item.GetChild(0).GetComponent<Renderer>().material;
                else
                    arrayToReturn[i] = item.GetComponent<Renderer>().material;
            }
        }
        return arrayToReturn;
    }

    public void actualizeImmageButtons(Sprite[] spriteArray)
    {
        for (int i = 0; i < spriteArray.Length; i++)
        {
            Sprite currentSprite = spriteArray[i];
            if (currentSprite == null)
                texturesButtons[i].GetComponent<Image>().sprite = spriteNone;
            else
                texturesButtons[i].GetComponent<Image>().sprite = currentSprite;
        }
    }

    void disableButton(Button buttonToDisable)
    {
        bodyButton.GetComponent<Button>().interactable = true;
        doorButton.GetComponent<Button>().interactable = true;
        drawerButton.GetComponent<Button>().interactable = true;
        legButton.GetComponent<Button>().interactable = true;
        elementsButton.GetComponent<Button>().interactable = true;
        buttonToDisable.GetComponent<Button>().interactable = false;
    }

    void actualizeLabelText(string textToLabel)
    {
        textLabel.text = textToLabel;
    }

    void backToCurrentMaterial(Transform obj)
    {
        print("Estoy en backToCurrentMaterial");
        Material currentMaterial;
        obj = textures.objectToBackMaterial;
        currentMaterial = obj.GetComponent<Renderer>().material;
        if (currentMaterial.color == highlightMateial.color) obj.GetComponent<Renderer>().material = textures.backMaterial;
    }

    public void putHighlightMaterialToObject(Transform objToHighlight)
    {
        textures.objectToBackMaterial = objToHighlight;
        textures.backMaterial = objToHighlight.GetComponent<Renderer>().material;
        objToHighlight.GetComponent<Renderer>().material = highlightMateial;
    }

    void findTextureButton(Sprite[] images)
    {
        Sprite item;
        for (int i = 0; i < images.Length; i++)
        {
            item = images[i];
            if (item != null)
            {
                if (item.name.Contains(textures.backMaterial.name))
                {
                    overMaterialButton.transform.localPosition = finishedButtons[i].transform.localPosition;
                }
           }
        }
    }

    void whenClickAButton(Boolean nextPrevBtn, String panelName, Button disableBtn, Sprite[] actualizeImgBtns, String labelText, Transform objForHighlight)
    {
        backToCurrentMaterial(textures.objectToBackMaterial);
        nextPrevButtons(nextPrevBtn);
        textures.Panel = panelName;
        disableButton(disableBtn);
        actualizeImmageButtons(actualizeImgBtns);
        actualizeLabelText(labelText);
        findTextureButton(actualizeImgBtns);
        putHighlightMaterialToObject(objForHighlight);
        samePositionhighlight();
    }

    public void bodyClicked()
    {
        whenClickAButton(true, "body", bodyButton, bodiesMaterialsSprites, "Cuerpos", textures.bodiesObjects[textures.bodiesCounter]);
    }

    public void doorClicked()
    {
        whenClickAButton(true, "door", doorButton, doorsMaterialsSprites, "Puertas", textures.doorsObjects[textures.doorsCounter].GetChild(0).transform);
    }

    public void drawerClicked()
    {
        whenClickAButton(true, "drawer", drawerButton, drawersMaterialsSprites, "Cajones", textures.drawersObjects[textures.drawersCounter].GetChild(0).transform);
    }

    public void legClicked()
    {
        whenClickAButton(false, "leg", legButton, legsMaterialsSprites, "Bases", textures.legsObjects[textures.legsCounter]);
    }

    void nextPrevButtons(Boolean state)
    {
        prevButton.gameObject.SetActive(state);
        nextButton.gameObject.SetActive(state);
    } 

    void whenNextButtonClicked(String label)
    {       
        backToCurrentMaterial(textures.objectToBackMaterial);
        if (textures.Panel == "body")
        {
            textures.objectToBackMaterial = textures.bodiesObjects[textures.bodiesCounter];
            textures.backMaterial = textures.objectToBackMaterial.GetComponent<Renderer>().material;
            putHighlightMaterialToObject(textures.bodiesObjects[textures.bodiesCounter].transform);
        }
        if (textures.Panel == "door")
        {
            textures.objectToBackMaterial = textures.doorsObjects[textures.doorsCounter];
            textures.backMaterial = textures.objectToBackMaterial.GetChild(0).GetComponent<Renderer>().material;
            putHighlightMaterialToObject(textures.doorsObjects[textures.doorsCounter].GetChild(0).transform);
        }
        if (textures.Panel == "drawer")
        {
            textures.objectToBackMaterial = textures.drawersObjects[textures.drawersCounter];
            textures.backMaterial = textures.objectToBackMaterial.GetChild(0).GetComponent<Renderer>().material;
            putHighlightMaterialToObject(textures.drawersObjects[textures.drawersCounter].GetChild(0).transform);
        }
        if (textures.Panel == "element")
        {
            textures.objectToBackMaterial = textures.elementsObjects[textures.elementsCounter];
            textures.backMaterial = textures.objectToBackMaterial.GetComponent<Renderer>().material;
            putHighlightMaterialToObject(textures.elementsObjects[textures.elementsCounter].transform);
        }
        if (textures.Panel == "leg")
        {
            textures.objectToBackMaterial = textures.legsObjects[textures.legsCounter];
            textures.backMaterial = textures.objectToBackMaterial.GetComponent<Renderer>().material;
            putHighlightMaterialToObject(textures.legsObjects[textures.legsCounter].transform);
        }

        actualizeLabelText(label);
    }

    public void nextButtonClicked()
    {
        if (textures.Panel == "body")
        {
            textures.bodiesCounter = Mathf.Clamp(textures.bodiesCounter + 1, 0, textures.bodiesCount - 1); ;
            whenNextButtonClicked("Cuerpos");
        }

        if (textures.Panel == "door")
        {
            textures.doorsCounter = Mathf.Clamp(textures.doorsCounter + 1, 0, textures.doorsCount - 1); ;
            whenNextButtonClicked("Puertas");
        }

        if (textures.Panel == "drawer")
        {
            textures.drawersCounter = Mathf.Clamp(textures.drawersCounter + 1, 0, textures.drawersCount - 1); ;
            whenNextButtonClicked("Cajones");
        }

        if (textures.Panel == "element")
        {
            textures.elementsCounter = Mathf.Clamp(textures.elementsCounter + 1, 0, textures.elementsCount - 1); ;
            whenNextButtonClicked("Elementos");
        }

        if (textures.Panel == "leg")
        {
            textures.legsCounter = Mathf.Clamp(textures.legsCounter + 1, 0, textures.legsCount - 1); ;
            whenNextButtonClicked("Bases");
        }
    }

    public void prevButtonClicked()
    {
        if (textures.Panel == "body")
        {
            textures.bodiesCounter = Mathf.Clamp(textures.bodiesCounter - 1, 0, textures.bodiesCount - 1); ;
            whenNextButtonClicked("Cuerpos");
        }

        if (textures.Panel == "door")
        {
            textures.doorsCounter = Mathf.Clamp(textures.doorsCounter - 1, 0, textures.doorsCount - 1); ;
            whenNextButtonClicked("Puertas");
        }

        if (textures.Panel == "drawer")
        {
            textures.drawersCounter = Mathf.Clamp(textures.drawersCounter - 1, 0, textures.drawersCount - 1); ;
            whenNextButtonClicked("Cajones");
        }

        if (textures.Panel == "element")
        {
            textures.elementsCounter = Mathf.Clamp(textures.elementsCounter - 1, 0, textures.elementsCount - 1); ;
            whenNextButtonClicked("Elementos");
        }

        if (textures.Panel == "leg")
        {
            textures.legsCounter = Mathf.Clamp(textures.legsCounter - 1, 0, textures.legsCount - 1); ;
            whenNextButtonClicked("Bases");
        }
    }

    void samePositionhighlight()
    {
        overMaterialButton.transform.localPosition = selectedMaterialButton.transform.localPosition;
    }

    public void overButton00()
    {
        string spriteName = finishedButtons[0].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[0];
        else
            samePositionhighlight();
    }

    public void clickButton00()
    {
        if (areContentButton(0))
        {
            assignMaterial(textures.Panel, 0);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[0];
        }
    }

    public void overButton01()
    {
        string spriteName = finishedButtons[1].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[1];
        else
            samePositionhighlight();
    }

    public void clickButton01()
    {
        if (areContentButton(1))
        {
            assignMaterial(textures.Panel, 1);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[1];
        }
    }

    public void overButton02()
    {
        string spriteName = finishedButtons[2].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[2];
        else
            samePositionhighlight();
    }

    public void clickButton02()
    {
        if (areContentButton(2))
        {
            assignMaterial(textures.Panel, 2);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[2];
        }
    }

    public void overButton03()
    {
        string spriteName = finishedButtons[3].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[3];
        else
            samePositionhighlight();
    }

    public void clickButton03()
    {
        if (areContentButton(3))
        {
            assignMaterial(textures.Panel, 3);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[3];
        }
    }

    public void overButton04()
    {
        string spriteName = finishedButtons[4].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[4];
        else
            samePositionhighlight();
    }

    public void clickButton04()
    {
        if (areContentButton(4))
        {
            assignMaterial(textures.Panel, 4);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[4];
        }
    }


    public void overButton05()
    {
        string spriteName = finishedButtons[5].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[5];
        else
            samePositionhighlight();
    }

    public void clickButton05()
    {
        if (areContentButton(5))
        {
            assignMaterial(textures.Panel, 5);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[5];
        }
    }


    public void overButton06()
    {
        string spriteName = finishedButtons[6].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[6];
        else
            samePositionhighlight();
    }

    public void clickButton06()
    {
        if (areContentButton(6))
        {
            assignMaterial(textures.Panel, 6);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[6];
        }
    }

    public void overButton07()
    {
        string spriteName = finishedButtons[7].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[7];
        else
            samePositionhighlight();
    }

    public void clickButton07()
    {
        if (areContentButton(7))
        {
            assignMaterial(textures.Panel, 7);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[7];
        }
    }

    public void overButton08()
    {
        string spriteName = finishedButtons[8].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[8];
        else
            samePositionhighlight();
    }

    public void clickButton08()
    {
        if (areContentButton(8))
        {
            assignMaterial(textures.Panel, 8);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[8];
        }
    }

    public void overButton09()
    {
        string spriteName = finishedButtons[9].transform.GetComponent<Image>().sprite.name;
        if (spriteName != "None_Button")
            overMaterialButton.transform.localPosition = btnPosition.ButtonPosition[9];
        else
            samePositionhighlight();
    }

    public void clickButton09()
    {
        if (areContentButton(9))
        {
            assignMaterial(textures.Panel, 9);
            selectedMaterialButton.transform.localPosition = btnPosition.ButtonPosition[9];
        }
    }

    public Boolean areContentButton(int buttonClicked)
    {
        string spriteName = finishedButtons[buttonClicked].GetComponent<Button>().name;
        if (spriteName != "None_Button") return true;
        return false;
    }

    public void assignMaterial(string panelType, int numberButton)
    {
        if (panelType == "body")
        {
            textures.bodiesObjects[textures.bodiesCounter].GetComponent<Renderer>().material = bodiesMaterials[numberButton];
        }
        if (panelType == "door")
        {
            textures.doorsObjects[textures.doorsCounter].GetChild(0).GetComponent<Renderer>().material = doorsMaterials[numberButton];
        }
        if (panelType == "drawer")
        {
            textures.drawersObjects[textures.drawersCounter].GetChild(0).GetComponent<Renderer>().material = drawersMaterials[numberButton];
        }
        if (panelType == "leg")
        {
            textures.legsObjects[textures.legsCounter].GetComponent<Renderer>().material = legsMaterials[numberButton];
        }
    }

    int elementCounter(Transform[] arrayToCount)
    {
        theCount = 0;
        for (int i = 0; i < arrayToCount.Length; i++)
        {
            if (arrayToCount[i] != null) theCount++;
        }
        return theCount;
    }
}