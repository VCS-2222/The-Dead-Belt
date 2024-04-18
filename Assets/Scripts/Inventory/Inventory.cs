using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("Customization Of Inventory")]
    [SerializeField] int amountOfItemsPerPageAllowed;

    [Header("Important Components and Assets")]
    [SerializeField] GameObject pageHolder;
    [SerializeField] GameObject pagePrefab;
    public List<InventoryPage> pages;

    [SerializeField] GameObject itemSlot;
    public List<Item> items;
    [SerializeField] TextMeshProUGUI weightCounter;
    [SerializeField] TextMeshProUGUI pageNumber;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] Button startupSelectedButton;

    [SerializeField] GameObject newestPageMade;

    [Header("Important Variables")]
    [SerializeField] float maxWeightOnBag;
    [SerializeField] float currentWeight;
    [SerializeField] bool canvasActive;

    public Controls controls;
    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        UpdateWeightText();
        UpdatePageText();
    }

    private void Update()
    {
        if (controls.UI.Inventory.WasPressedThisFrame())
        {
            CanvasOperator();
        }
    }

    public void AddItem(Item item)
    {
        if (currentWeight >= maxWeightOnBag) return;

        if (pages.Count == 0)
        {
            MakeNewPage();
        }

        for (int i = 0; i < pages.Count;)
        {
            if (pages[i].amountOfItemsInPage < amountOfItemsPerPageAllowed)
            {
                if(currentWeight > maxWeightOnBag) return;

                GameObject newSlot = Instantiate(itemSlot, pages[i].transform.GetChild(0), false);

                newSlot.GetComponent<Slot>().AssignItemInSlot(item);
                items.Add(item);
                AssignSlotName(newSlot, item.ReturnName());
                AddToWeight(item.ReturnWeight());
                break;
            }

            if (pages[i].amountOfItemsInPage >= amountOfItemsPerPageAllowed)
            {
                i++;

                if (currentWeight > maxWeightOnBag) return;

                if (i == pages.Count)
                {
                    MakeNewPage();
                    FocusOnPage(pages.Count - 1);
                    UpdatePageText();

                    GameObject newSlot = Instantiate(itemSlot, pages[i].transform.GetChild(0), false);

                    newSlot.GetComponent<Slot>().AssignItemInSlot(item);
                    items.Add(item);
                    AssignSlotName(newSlot, item.ReturnName());
                    AddToWeight(item.ReturnWeight());
                    break;
                }
            }
        }

        UpdateWeightText();
    }

    public void FocusOnPage(int pageNumber)
    {
        for(int i = 0; i < pages.Count; i++)
        {
            if (pages.IndexOf(pages[i]) == pageNumber)
            {
                pages[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                pages[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void MakeNewPage()
    {
        GameObject newPage = Instantiate(pagePrefab, pageHolder.transform, false);
        newestPageMade = newPage;
        pages.Add(newPage.GetComponent<InventoryPage>());
    }

    public void AddToWeight(float amount)
    {
        if (amount >= maxWeightOnBag) return;

        currentWeight += amount;
    }

    public void RemoveFromWeight(float amount)
    {
        float amountRetracted = currentWeight - amount;

        if(amountRetracted <= 0)
        {
            currentWeight = 0;
        }
        else
        {
            currentWeight -= amount;
        }
    }

    public void UpdateWeightText()
    {
        weightCounter.text = currentWeight.ToString("000.00") + " / " + maxWeightOnBag.ToString("000.00");
    }

    public void UpdatePageText()
    {
        int pagAdd;

        for (int i = 0; i < pages.Count; i++)
        {
            if (pages[i].transform.GetChild(0).gameObject.activeSelf == true)
            {
                pagAdd = pages.IndexOf(pages[i]) + 1;
                pageNumber.text = pagAdd.ToString("0");
                break;
            }
        }
    }

    public void TurnOffSpecificPage(int toTurnOff)
    {
        pages[toTurnOff].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void TurnPage(bool right)
    {
        if (right)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].transform.GetChild(0).gameObject.activeSelf == true)
                {
                    if (pages.Last() == pages[i]) return;
                    if (pages[i + 1] == null) return;

                    TurnOffSpecificPage(i);
                    pages[i + 1].transform.GetChild(0).gameObject.SetActive(true);
                    break;
                }
            }
        }

        if (!right)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].transform.GetChild(0).gameObject.activeSelf == true)
                {
                    if (pages.First() == pages[i]) return;
                    if (pages[i - 1] == null) return;

                    TurnOffSpecificPage(i);
                    pages[i - 1].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

        UpdatePageText();
    }

    public void AssignSlotName(GameObject newSlot, string newName)
    {
        newSlot.GetComponentInChildren<TextMeshPro>().text = newName;
    }

    public void CanvasOperator()
    {
        canvasActive = !canvasActive;

        if (canvasActive)
        {
            startupSelectedButton.Select();
            inventoryCanvas.SetActive(true);
        }
        else
        {
            startupSelectedButton.Select();
            inventoryCanvas.SetActive(false);
        }
    }
}