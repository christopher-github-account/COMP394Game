using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.animator.SetBool("Aiming", true);
        //aim.animator.SetBool("Rifle", true);

        aim.currentFOV = aim.aimFOV;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.Hipfire);
        }
    }
}
