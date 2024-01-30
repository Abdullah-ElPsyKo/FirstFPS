using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    // Shooting
    public float rateOfFire = 1f; // Rounds per second
    private float nextFireTime = 0f;

    // Ammo
    public int ammoCapacity = 12;
    private int currentAmmo;
    public float reloadTime = 2.0f;
    private bool isReloading = false;

    // Hitscan
    public float range = 100f; // Max range of the gun
    public int damage = 25;    // Damage the gun deals

    // Camera reference for raycasting
    public Camera playerCamera;

    // Audio
    public AudioClip fireSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    // Photon
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponentInParent<PhotonView>(); // Get the PhotonView from the player
        audioSource = GetComponent<AudioSource>();
        currentAmmo = ammoCapacity;
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / rateOfFire;
            Shoot();
        }
    }

    void Shoot()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera not assigned.");
            return;
        }

        currentAmmo--;
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
            if (target != null)
            {
                // Pass this photonView as the attacker
                target.TakeDamage(damage, photonView);
            }
        }
        audioSource.PlayOneShot(fireSound);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = ammoCapacity;
        isReloading = false;
    }
}
