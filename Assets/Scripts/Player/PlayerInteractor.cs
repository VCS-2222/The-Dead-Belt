using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float interactRange;
   // public RaycastHit hit;

    [Header("Components")]
    [SerializeField] Inventory inventory;

    public Controls controls;
    private void Awake()
    {
        controls = new Controls();
        controls.Player.Interacting.performed += t => HandleInteraction();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void HandleInteraction()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit, interactRange);

        if (hit.transform == null)
        {
            return;
        }
        else
        {
            if (hit.transform.tag == "Interactable")
            {
                ComponentCheck(hit);
            }
            else
            {
                return;
            }
        }
    }

    void ComponentCheck(RaycastHit hit)
    {
        if (hit.transform.gameObject.GetComponent<AnimatedInteractive>() != null)
        {
            if (hit.transform.gameObject.GetComponent<AnimatedInteractive>().ReturnFunctionName() == "door")
            {
                ExecuteAnimatedBasedOnName(hit, "door");
            }
        }

        if (hit.transform.gameObject.GetComponent<AudioInteractive>() != null)
        {
            if (hit.transform.gameObject.GetComponent<AudioInteractive>().ReturnFunctionName() == "log")
            {
                ExecuteAudioBasedOnName(hit, "log");
            }
        }

        if (hit.transform.gameObject.GetComponent<EndGame>() != null)
        {
            hit.transform.gameObject.GetComponent<EndGame>().FeedPackageToKiosk();
        }

        if (hit.transform.gameObject.GetComponent<PhysicalItem>() != null)
        {
            if (inventory.CheckWeight() >= inventory.ReturnMaxWeight()) return;

            AddItemToInventory(hit);
        }
    }

    #region executables
    public void ExecuteAnimatedBasedOnName(RaycastHit hit, string name)
    {
        if(name == "door")
        {
            hit.transform.gameObject.GetComponent<AnimatedInteractive>().ChangeBoolBasedOnAnimatorBool("opened");
        }
    }

    public void ExecuteAudioBasedOnName(RaycastHit hit, string name)
    {
        if (name == "log")
        {
            hit.transform.gameObject.GetComponent<AudioInteractive>().StartAudioSource();
        }
    }

    void AddItemToInventory(RaycastHit hit)
    {
        Item itemToAdd = hit.transform.gameObject.GetComponent<PhysicalItem>().GetItem();
        //print(itemToAdd.name);

        inventory.AddItem(itemToAdd);
        Destroy(hit.transform.gameObject);
    }
    #endregion
}