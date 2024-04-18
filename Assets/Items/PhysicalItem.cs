using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    [SerializeField] Item item;

    public Item GetItem()
    {
        return item;
    }
}