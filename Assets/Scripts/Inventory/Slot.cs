using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Item itemInSlot;
    [SerializeField] int amountOfItemsInSlot;
    [SerializeField] TextMeshProUGUI itemName;

    private void Start()
    {
        InitializeTheItem();
    }

    public void InitializeTheItem()
    {
        //print(itemInSlot.ReturnName());
        itemName.text = itemInSlot.ReturnName().ToString();
        //print(itemName);
    }

    public int ReturnAmountInSlot()
    {
        return amountOfItemsInSlot;
    }

    public Item ReturnItem()
    {
        return itemInSlot;
    }

    public string ReturnDescription()
    {
        return itemInSlot.itemDescription.ToString();
    }

    public void AssignItemInSlot(Item item)
    {
        itemInSlot = item;
        amountOfItemsInSlot++;
    }

    public void KillItemInSlot()
    {
        itemInSlot = null;
    }
}