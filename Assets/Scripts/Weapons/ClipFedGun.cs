using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipFedGun : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] int currentAmmo;
    [SerializeField] int maxAmmo;
    public Controls controls;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Fire.performed += t => TestShoot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void TestShoot()
    {
        print(transform.name + " SHOT");
    }

    public IEnumerator Shoot(Transform origin, float range, float delayToShoot)
    {
        animator.SetTrigger("shot");

        yield return new WaitForSeconds(delayToShoot);

        RaycastHit hit;
        Physics.Raycast(origin.position, origin.transform.forward, out hit, range);

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.name);
            if(hit.collider.gameObject.GetComponent<ZombieMovement>() != null)
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}