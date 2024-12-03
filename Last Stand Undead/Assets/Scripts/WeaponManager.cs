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
    public int damage = 20;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public WeaponAmmo ammo;
    WeaponBloom bloom;
    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    VisualEffect muzzleFlashVFX;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20f;

    public Transform leftHandTarget, leftHandHint;
    WeaponClassManager weaponClass;

    void Start()
    {
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashVFX = GetComponentInChildren<VisualEffect>();
        fireRateTimer = fireRate;
    }

    private void OnEnable()
    {
        if (weaponClass == null)
        {
            weaponClass = GetComponentInParent<WeaponClassManager>();
            recoil = GetComponent<WeaponRecoil>();
            audioSource = GetComponent<AudioSource>();
            aim = GetComponentInParent<AimStateManager>();
            ammo = GetComponent<WeaponAmmo>();
            recoil.recoilFollowPosition = weaponClass.recoilFollowPos;
        }
        weaponClass.SetCurrentWeapon(this);
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
        if (actions.currentState == actions.Reload) { return false; }
        if (actions.currentState == actions.Swap) { return false; }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) { return true; }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) { return true; }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        ammo.currentAmmo--;
        //Debug.Log("Fire");

        barrelPosition.LookAt(aim.aimPosition);
        barrelPosition.localEulerAngles = bloom.BloomAngle(barrelPosition);

        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);

            Bullet bulletScript = currentBullet.GetComponent<Bullet>();
            bulletScript.weapon = this;

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
