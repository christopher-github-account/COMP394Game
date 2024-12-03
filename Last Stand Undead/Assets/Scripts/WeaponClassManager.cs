using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponClassManager : MonoBehaviour
{
    [SerializeField] TwoBoneIKConstraint LeftHandIK;
    public Transform recoilFollowPos;
    ActionStateManager actions;

    public WeaponManager[] weapons;
    int currentWeaponIndex;

    [SerializeField] CrosshairManager crosshairManager;

    private void Awake()
    {
        crosshairManager = GetComponent<CrosshairManager>();
        currentWeaponIndex = 0;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0) { weapons[i].gameObject.SetActive(true); }
            else { weapons[i].gameObject.SetActive(false); }
        }
    }

    public void SetCurrentWeapon(WeaponManager weapon)
    {
        if (actions == null) { actions = GetComponent<ActionStateManager>(); }
        LeftHandIK.data.target = weapon.leftHandTarget;
        LeftHandIK.data.hint = weapon.leftHandHint;
        actions.SetWeapon(weapon);
        crosshairManager.SetCrosshair(weapon.GetComponent<WeaponBloom>());
    }

    public void ChangeWeapon(float direction)
    {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        if (direction < 0)
        {
            if (currentWeaponIndex == 0) { currentWeaponIndex = weapons.Length - 1; }
            else { currentWeaponIndex--; }
        }
        else
        {
            if (currentWeaponIndex == weapons.Length - 1) { currentWeaponIndex = 0; }
            else { currentWeaponIndex++; }
        }
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void WeaponPutAway()
    {
        ChangeWeapon(actions.Default.scrollDirection);
    }

    public void WeaponPulledOut()
    {
        actions.SwitchState(actions.Default);
    }
}
