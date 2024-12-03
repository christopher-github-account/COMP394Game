using UnityEngine;

public class DefaultState : ActionBaseState
{
    public float scrollDirection;

    public override void EnterState(ActionStateManager actions)
    {

    }

    public override void UpdateState(ActionStateManager actions)
    {
        actions.rightHandAim.weight = Mathf.Lerp(actions.rightHandAim.weight, 1, 10 * Time.deltaTime);
        if (actions.leftHandIK.weight == 0) { actions.leftHandIK.weight = 1; }

        if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
        {
            actions.SwitchState(actions.Reload);
        }
        else if (Input.mouseScrollDelta.y != 0)
        {
            scrollDirection = Input.mouseScrollDelta.y;
            actions.SwitchState(actions.Swap);
        }
    }

    bool CanReload(ActionStateManager action)
    {
        if (action.ammo.currentAmmo == action.ammo.clipSize) { return false; }
        else if (action.ammo.extraAmmo == 0) { return false; }

        return true;
    }
}
