using UnityEngine;

public class HipfireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.animator.SetBool("Aiming", false);
        //aim.animator.SetBool("Rifle", true);

        aim.currentFOV = aim.hipFOV;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Aim);
        }
    }
}
