using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipFedGun : MonoBehaviour
{
    [Header("Gun Variables")]
    [SerializeField] int currentAmmo;
    [SerializeField] int maxAmmo;
    public Controls controls;
    [SerializeField] float range;
    [SerializeField] float delay;
    [SerializeField] float damagePerBullet;
    bool isShooting;

    [Header("Gun Components")]
    [SerializeField] Item ammo;
    [SerializeField] Transform shootPoint;
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioClip[] gunShots;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private void Awake()
    {
        controls = new Controls();
        controls.Weapons.Fire.performed += t => StartCoroutine(Shoot(shootPoint, delay));
        controls.Weapons.Reload.performed += t => Reload();
        controls.Weapons.Stow.performed += t => Stow();
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
        isShooting = false;
    }

    public void PlayRandomShotSound()
    {
        int ranNum = Random.Range(0, gunShots.Length);
        shootSound.PlayOneShot(gunShots[ranNum]);
    }

    public IEnumerator Shoot(Transform origin, float delayToShoot)
    {
        if (isShooting) yield break;

        if (currentAmmo <= 0) yield break;

        animator.SetTrigger("shot");

        isShooting = true;

        yield return new WaitForSeconds(delayToShoot);

        RaycastHit hit;
        Physics.Raycast(origin.position, origin.transform.forward, out hit, range);

        currentAmmo--;

        if (hit.collider != null)
        {
            print(hit.collider.gameObject.name);
            if (hit.collider.tag == "Zombie")
            {
                hit.collider.gameObject.GetComponent<ZombieStats>().TakeDamage(damagePerBullet);
            }
        }

        isShooting = false;
    }

    public void AssignComponents(Transform shootingPoint)
    {
        shootPoint = shootingPoint;
    }

    public void Reload()
    {
        if(Inventory.Instance.ReturnAmmo() == true)
        {
            if(currentAmmo == 7)
            {
                animator.SetInteger("bulletsneededtoreload", 1);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 6)
            {
                animator.SetInteger("bulletsneededtoreload", 2);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 5)
            {
                animator.SetInteger("bulletsneededtoreload", 3);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 4)
            {
                animator.SetInteger("bulletsneededtoreload", 4);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 3)
            {
                animator.SetInteger("bulletsneededtoreload", 5);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 2)
            {
                animator.SetInteger("bulletsneededtoreload", 6);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 1)
            {
                animator.SetInteger("bulletsneededtoreload", 7);
                animator.SetTrigger("reload");
            }

            if (currentAmmo == 0)
            {
                animator.SetInteger("bulletsneededtoreload", 8);
                animator.SetTrigger("reload");
            }

            currentAmmo = maxAmmo;
        }
    }

    public void Stow()
    {
        if (currentAmmo == maxAmmo)
        {
            Inventory.Instance.AddItem(ammo);
        }

        Inventory.Instance.StowWeaponAway();
    }
}