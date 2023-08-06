using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MovementController
{

    internal override float DecideGravity()
    {
        return fallGravity;
    }

    internal override bool GetDoubleJumpInput()
    {
        return false;
    }

    internal override float GetHorizontalInput()
    {
        return -1;
    }

    internal override bool GetJumpInput()
    {
        return false;
    }

    internal override void JumpEffects()
    {
    }
}
