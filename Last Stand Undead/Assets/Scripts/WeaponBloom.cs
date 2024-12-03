using UnityEngine;

public class WeaponBloom : MonoBehaviour
{
    [SerializeField] float defaultBloomAngle = 3;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float sprintBloomMultiplier = 2f;
    [SerializeField] float aimBloomMultiplier = 0.5f;

    MovementStateManager movement;
    AimStateManager aiming;

    [HideInInspector] public float currentBloom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponentInParent<MovementStateManager>();
        aiming = GetComponentInParent<AimStateManager>();
    }

    public Vector3 BloomAngle(Transform barrelPosition)
    {
        if (movement.currentState == movement.Idle) { currentBloom = defaultBloomAngle; }
        else if (movement.currentState == movement.Walk) { currentBloom = defaultBloomAngle * walkBloomMultiplier; }
        else if (movement.currentState == movement.Run) { currentBloom = defaultBloomAngle * sprintBloomMultiplier; }
        else if (movement.currentState == movement.Crouch) 
        {
            if (movement.direction.magnitude == 0) { currentBloom = defaultBloomAngle * crouchBloomMultiplier; }
            else { currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier; }
        }

        if (aiming.currentState == aiming.Aim) { currentBloom *= aimBloomMultiplier; }

        Vector3 randomRotation = new Vector3(Random.Range(-currentBloom, currentBloom), Random.Range(-currentBloom, currentBloom), Random.Range(-currentBloom, currentBloom));

        return barrelPosition.localEulerAngles + randomRotation;

    }
}
