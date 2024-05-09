using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemType;
    public string itemDescription;
    public float itemWeight;
    public GameObject itemPrefab;

    public bool isWeapon;
    public bool isConsumable;

    public bool isMelee;
    public float ReturnWeight()
    {
        return itemWeight;
    }

    public string ReturnName()
    {
        return itemName;
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(Item))]
//class ItemEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        Item itemScript = (Item)target;

//        EditorGUILayout.LabelField("Item Name");
//        itemScript.itemName = GUILayout.TextField(itemScript.itemName);

//        EditorGUILayout.LabelField("Item Description");
//        itemScript.itemDescription = GUILayout.TextField(itemScript.itemDescription);

//        EditorGUILayout.LabelField("Item Type");
//        itemScript.itemType = GUILayout.TextField(itemScript.itemType);

//        EditorGUILayout.LabelField("Item Weight");
//        itemScript.itemWeight = EditorGUILayout.FloatField(itemScript.itemWeight);

//        EditorGUILayout.Space();

//        itemScript.isWeapon = GUILayout.Toggle(itemScript.isWeapon, "Is the item a weapon");
//        itemScript.isConsumable = GUILayout.Toggle(itemScript.isConsumable, "Is the item consumable?");

//        if (itemScript.isWeapon)
//        {
//            itemScript.isConsumable = false;
//            itemScript.isMelee = GUILayout.Toggle(itemScript.isMelee, "Is the weapon melee?");

//            EditorGUILayout.Space();

//            if (itemScript.isMelee)
//            {

//            }
//            else
//            {

//            }
//        }
//    }
//}
//#endif