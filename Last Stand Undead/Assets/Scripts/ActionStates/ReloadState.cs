using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions.rightHandAim.weight = 0f;
        actions.leftHandIK.weight = 0f;
        actions.animator.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager actions)
    {

    }
}
