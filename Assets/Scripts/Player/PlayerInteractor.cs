using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float interactRange;
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

        if(hit.transform.tag == "Interactable")
        {
            ComponentCheck(hit);
        }
        else
        {
            return;
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
    #endregion
}