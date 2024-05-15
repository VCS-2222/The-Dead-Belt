using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUseManager : MonoBehaviour
{
    [SerializeField] GameObject currentWeapon;
    [SerializeField] Transform shootPoint;
    [SerializeField] bool currentWeaponMelee;
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

    public void SetCurrentWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = newWeapon;
        AuthentificationOfCurrentWeapon();
    }

    public void AuthentificationOfCurrentWeapon()
    {
        if (currentWeapon == null) return;

        if (currentWeapon.GetComponent<ClipFedGun>() != null)
        {
            currentWeapon.GetComponent<ClipFedGun>().AssignComponents(shootPoint);
        }
        
        if(currentWeapon.GetComponent<MagFedGun>() != null)
        {
            currentWeapon.GetComponent<MagFedGun>().AssignComponents(shootPoint);
        }

        if (currentWeapon.GetComponent<Melee>() != null)
        {
            currentWeapon.GetComponent<Melee>().AssignComponents(shootPoint);
        }
    }
}
