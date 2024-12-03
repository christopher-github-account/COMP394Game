using UnityEngine;

public class SwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.animator.SetTrigger("SwapWeapon");
        actions.leftHandIK.weight = 0f;
        actions.rightHandAim.weight = 0f;
    }

    public override void UpdateState(ActionStateManager actions)
    {

    }
}
