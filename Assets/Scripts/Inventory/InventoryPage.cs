using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    public GameObject slotHolder;
    public int amountOfItemsInPage;

    public void LateUpdate()
    {
        amountOfItemsInPage = slotHolder.transform.childCount;
    }
}