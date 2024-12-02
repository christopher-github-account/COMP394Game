using UnityEngine;
using UnityEngine.VFX;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate = 1;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo ammo;
    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    VisualEffect muzzleFlashVFX;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        recoil = GetComponent<WeaponRecoil>();
        ammo = GetComponent<WeaponAmmo>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashVFX = GetComponentInChildren<VisualEffect>();
        fireRateTimer = fireRate;
    }

    void Update()
    {
        if (ShouldFire()) 
        { 
            Fire();
            //Debug.Log(ammo.currentAmmo);
        }
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) { return false; }
        if (ammo.currentAmmo == 0) { return false; }
        if (actions.currentState == actions.Reload) {  return false; }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) { return true; }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) { return true; }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        //Debug.Log("Fire");
        barrelPosition.LookAt(aim.aimPosition);
        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        ammo.currentAmmo--;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
            Rigidbody bulletRigidBody = currentBullet.GetComponent<Rigidbody>();
            bulletRigidBody.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashVFX.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }

}
