using UnityEngine.UI;

using UnityEngine;

public class FurnitureAutoConfig : MonoBehaviour
{
    string findBodies = "body";
    string findDrawers = "dummy_drawer";
    string findDoors = "dummy_door";
    string findElements = "element";
    string findLegs = "leg";

    [Header("FURNITURE PARTS")]
    public Transform[] bodies;
    public Transform[] drawersBase;
    public Transform[] doorsBase;
    public Transform[] elements;
    public Transform[] legs;

    public Transform furnitureParent;
    public Vector3[] legBoundingBox = new Vector3[5];
    int MaxMaterials = 10;

    [Header("FINGER TYPES")]
    public GameObject[] legsTypes = new GameObject[5];
    [Header("BODIES MATERIALS")]
    public Material[] bodiesMaterials = new Material[10];
    [Header("SAME MATERIALS")]
    public bool sameBodiesAndDoors = false;
    public bool sameBodiesAndDrawers = false;
    public bool sameBodiesAndElements = false;
    [Header("DOORS MATERIALS")]
    public Material[] doorsMaterials = new Material[10];
    [Header("DRAWERS MATERIALS")]
    public Material[] drawersMaterials = new Material[10];
    [Header("ELEMENTS MATERIALS")]
    public Material[] elementsMaterials = new Material[10];
    [Header("LEGS MATERIALS")]
    public Material[] legsMaterials = new Material[5];
    [Header("KNOBS MATERIALS")]
    public Material[] knobsMaterials = new Material[5];

    public GameObject furniture;
    public Text doorName;
    public Text drawerName;
    public Text legName;
    public Text textLabel;
    public Material highlightMaterial;

    private void Awake()
    {
        furnitureParent = GetComponent<Transform>();

        int childNumber = furnitureParent.childCount;

        bodies = new Transform[childNumber];
        bodies = findChilds(findBodies, furnitureParent, childNumber);

        drawersBase = new Transform[10];
        drawersBase = findChilds(findDrawers, furnitureParent, 10);

        doorsBase = new Transform[10];
        doorsBase = findChilds(findDoors, furnitureParent, 10);


        elements = new Transform[10];
        elements = findChilds(findElements, furnitureParent, 10);

        legs = new Transform[5];
        legs = findChilds(findLegs, furnitureParent, 5);

        addMeshCollider(legs, "leg");
        getBoundingBox(legs, "leg");

        if (sameBodiesAndDoors) doorsMaterials = bodiesMaterials;
        if (sameBodiesAndDrawers) drawersMaterials = bodiesMaterials;
        if (sameBodiesAndElements) elementsMaterials = bodiesMaterials;
    }
    private void Start()
    {

    }

    Transform[] findChilds(string findChildContains, Transform Parent, int childsNumber)
    {
        Transform children;
        int TotalChild = Parent.childCount;
        Transform[] objectsToReturn = new Transform[childsNumber];
        int objectsToReturnPosition = 0;
        for (int i = 0; i < TotalChild; i++)
        {
            children = Parent.GetChild(i);
            if (children.name.Contains(findChildContains))
            {
                objectsToReturn[objectsToReturnPosition] = children;
                objectsToReturnPosition++;
            }
        }
        return objectsToReturn;
    }

    void getBoundingBox(Transform[] arrayItems, string stringToFind)
    {
        Transform item;
        for (int i = 0; i < arrayItems.Length; i++)
        {
            item = arrayItems[i];
            if (item != null) legBoundingBox[i] = item.GetComponent<MeshCollider>().bounds.size;
        }
    }

    void addMeshCollider(Transform[] arrayItems, string stringToFind)
    {
        Transform item;
        Transform childItem;
        int itemChildNumber;
        for (int i = 0; i < arrayItems.Length; i++)
        {
            item = arrayItems[i];
            if (item != null)
            {
                itemChildNumber = item.childCount;
                if (itemChildNumber > 0)
                {
                    for (int x = 0; x < item.childCount; x++)
                    {
                        childItem = item.GetChild(x);
                        addMeshColliderToTransform(childItem, stringToFind);
                    }
                }
                else
                {
                    item.gameObject.AddComponent<MeshCollider>();
                }
            }
        }
    }

    void addMeshColliderToTransform(Transform obj, string strToFind)
    {
        if (obj.name.Contains(strToFind))
        {
            obj.gameObject.AddComponent<MeshCollider>();
        }
    }
}
