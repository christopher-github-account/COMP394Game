using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] RectTransform crosshair;
    private float baseSize = 10f;

    [SerializeField] WeaponClassManager weaponClass;
    WeaponBloom bloom;

    private void Awake()
    {
        bloom = new WeaponBloom();
        weaponClass = GetComponentInParent<WeaponClassManager>();
    }

    private void Update()
    {
        crosshair.sizeDelta = 
            Vector2.Lerp(crosshair.sizeDelta, new Vector2(
                baseSize * (bloom.currentBloom + 1), baseSize * (bloom.currentBloom + 1)
                ), 5f * Time.deltaTime);
    }

    public void SetCrosshair(WeaponBloom activeBloom)
    {
        bloom = activeBloom;
    }
}
