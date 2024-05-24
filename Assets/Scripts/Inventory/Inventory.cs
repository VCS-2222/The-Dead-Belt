
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("Customization Of Inventory")]
    [SerializeField] int amountOfItemsPerPageAllowed;

    [Header("Important Components and Assets")]
    [SerializeField] GameObject pageHolder;
    [SerializeField] GameObject pagePrefab;
    public List<InventoryPage> pages;

    [SerializeField] GameObject itemSlot;
    public List<Item> items;
    public List<GameObject> possiblePhysicalItems;
    [SerializeField] MeshRenderer inventoryCanvasMesh;
    [SerializeField] Canvas inventoryCanvasUI;
    [SerializeField] Button startupSelectedButton;
    [SerializeField] GameObject currentActivePage;
    [SerializeField] Slot currentSelectedSlot;
    [SerializeField] GameObject itemHolder;
    [SerializeField] WeaponUseManager weaponUseManager;
    [SerializeField] Item ammoNeeded;

    [SerializeField] PlayerStats playerStats;

    [SerializeField] GameObject newestPageMade;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI weightCounter;
    [SerializeField] TextMeshProUGUI pageNumber;
    [SerializeField] TextMeshProUGUI description;

    [Header("Important Variables")]
    [SerializeField] float maxWeightOnBag;
    [SerializeField] float currentWeight;
    [SerializeField] bool canvasActive;

    public Controls controls;

    private void Awake()
    {
        Instance = this;
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
                //newSlot.GetComponent<Button>().OnSelect(CheckSlotForSelection(newSlot.GetComponent<Slot>()));
                items.Add(item);
                AddToWeight(item.ReturnWeight());
                //AssignSlotName(newSlot, item.ReturnName().ToString());
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
                    //newSlot.GetComponent<Button>().onClick.AddListener(() => SubscribeToDescription(newSlot.GetComponent<Slot>()));
                    items.Add(item);
                    AddToWeight(item.ReturnWeight());
                    //AssignSlotName(newSlot, item.ReturnName().ToString());
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
        ClearDescription();
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

    public float CheckWeight()
    {
        return currentWeight;
    }

    public float ReturnMaxWeight()
    {
        return maxWeightOnBag;
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
        ClearDescription();

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
                    break;
                }
            }
        }

        GetActivePage();
        //startupSelectedButton = ReturnFirstSlotButton();

        UpdatePageText();
    }

    public void AssignSlotName(GameObject newSlot, string newName)
    {
        GameObject newSlotChild = newSlot.transform.GetChild(0).gameObject;

        newSlotChild.GetComponent<Text>().text = newName;
    }

    public void GetActivePage()
    {
        for(int i = 0; i < pages.Count; i++)
        {
            if (pages[i].enabled)
            {
                currentActivePage = pages[i].gameObject;
            }
        }
    }

    public void AssignSlotToCurrentSlot(Slot newSlot)
    {
        currentSelectedSlot = newSlot;
    }

    public void AssignItemToDescription(Item designatedItem)
    {
        description.text = designatedItem.itemDescription;
    }

    public void UseItemInCurrentSlot()
    {
        if(currentSelectedSlot == null) return;

        if (currentSelectedSlot.ReturnItem().isWeapon)
        {
            GameObject currentWeapon = Instantiate(currentSelectedSlot.ReturnItem().itemPrefab, itemHolder.transform);
            weaponUseManager.SetCurrentWeapon(currentWeapon);
            weaponUseManager.AuthentificationOfCurrentWeapon();
            ClearDescription();
            currentSelectedSlot = null;
        }
        else if(currentSelectedSlot.ReturnItem().isConsumable)
        {
            playerStats.GainHealth(15);
            playerStats.Eat(25);
            ClearDescription();
            SearchAndDestroySpecificItem(currentSelectedSlot.ReturnItem());
            Destroy(currentSelectedSlot.transform.gameObject);
        }
    }

    public void ClearDescription()
    {
        description.text = "";
    }

    public void ThrowItemAway()
    {
        if (currentSelectedSlot == null) return;

        if(itemHolder.transform.childCount > 0)
        {
            if (currentSelectedSlot.GetComponent<Slot>().ReturnItem().isStory) return;

            SearchAndDestroySpecificItem(currentSelectedSlot.ReturnItem());
            Destroy(currentSelectedSlot.transform.gameObject);
            StowWeaponAway();
        }
        else
        {
            if (currentSelectedSlot.GetComponent<Slot>().ReturnItem().isStory) return;

            SearchAndDestroySpecificItem(currentSelectedSlot.ReturnItem());
            Destroy(currentSelectedSlot.transform.gameObject);
        }
    }

    public void StowWeaponAway()
    {
        if(itemHolder.transform.childCount > 0)
        {
            Destroy(itemHolder.transform.GetChild(0).gameObject);
            currentSelectedSlot = null;
        }
    }

    public void ThrowSpecificItemAway(Item throwItem)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            print("looking in page " + pages[i]);
            for(int o = 0; o < pages[i].slotHolder.transform.childCount; o++)
            {
                print("looking at slot " + pages[i].slotHolder.transform.GetChild(o));
                if (pages[i].slotHolder.transform.GetChild(o).gameObject.GetComponent<Slot>().ReturnItem() == throwItem)
                {
                    Destroy(pages[i].slotHolder.transform.GetChild(o).gameObject);

                    items.Remove(throwItem);
                    print("found and destroyed slot");
                    break;
                }
            }
        }
    }

    public Item ReturnAmmo()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            print("looking in page " + pages[i]);
            for (int o = 0; o < pages[i].slotHolder.transform.childCount; o++)
            {
                print("looking at slot " + pages[i].slotHolder.transform.GetChild(o));
                if (pages[i].slotHolder.transform.GetChild(o).gameObject.GetComponent<Slot>().ReturnItem() == ammoNeeded)
                {
                    Item theAmmo = pages[i].slotHolder.transform.GetChild(o).gameObject.GetComponent<Slot>().ReturnItem();

                    print("found and collected ammo");
                    Destroy(pages[i].slotHolder.transform.GetChild(o).gameObject);

                    SearchAndDestroySpecificItem(theAmmo);

                    return theAmmo;

                }
                else
                {
                    
                }
            }
        }

        return null;
    }

    public Item ReturnItem(Item wantedItem)
    {
        for (int i = 0; i < pages.Count; i++)
        {
            print("looking in page " + pages[i]);
            for (int o = 0; o < pages[i].slotHolder.transform.childCount; o++)
            {
                print("looking at slot " + pages[i].slotHolder.transform.GetChild(o));
                if (pages[i].slotHolder.transform.GetChild(o).gameObject.GetComponent<Slot>().ReturnItem() == wantedItem)
                {
                    Item theItem = pages[i].slotHolder.transform.GetChild(o).gameObject.GetComponent<Slot>().ReturnItem();

                    print("found and collected ammo");
                    Destroy(pages[i].slotHolder.transform.GetChild(o).gameObject);

                    SearchAndDestroySpecificItem(theItem);

                    return theItem;

                }
                else
                {

                }
            }
        }

        return null;
    }

    public void SearchAndDestroySpecificItem(Item theItem)
    {
        items.Remove(theItem);
    }

    public void CheckSlotForSelection(Slot slot)
    {
        description.text = slot.ReturnDescription();
    }

    public Slot ReturnFirstSlotOfActivePage()
    {
        GetActivePage();

        if (currentActivePage.GetComponent<InventoryPage>().amountOfItemsInPage >= 1)
        {
            print("returning slot");

            GameObject actualPage = currentActivePage.transform.GetChild(0).gameObject;

            Slot wantedSlot = actualPage.GetComponentInChildren<Slot>();

            print(wantedSlot);

            return wantedSlot;
        }
        else
        {
            return null;
        }
    }

    public Button ReturnFirstSlotButton()
    {
        return ReturnFirstSlotOfActivePage().gameObject.GetComponent<Button>();
    }

    public void CanvasOperator()
    {
        canvasActive = !canvasActive;

        if (canvasActive)
        {
            GetActivePage();

            if (currentActivePage != null)
            {
                print("found active page");

                startupSelectedButton.Select();

                if (itemHolder.transform.childCount > 0)
                {
                    //StowWeaponAway();
                    ClearDescription();
                    inventoryCanvasMesh.enabled = true;
                    inventoryCanvasUI.enabled = true;
                }
                else
                {
                    ClearDescription();
                    inventoryCanvasMesh.enabled = true;
                    inventoryCanvasUI.enabled = true;
                }
            }

            ClearDescription();
            startupSelectedButton.Select();

            inventoryCanvasMesh.enabled = true;
            inventoryCanvasUI.enabled = true;
        }
        else
        {
            ClearDescription();

            inventoryCanvasMesh.enabled = false;
            inventoryCanvasUI.enabled = false;
        }
    }
}