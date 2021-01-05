using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.FSM
{
    public enum ActionTags
    {
        idle,
        walk,
        interact,
        drag,
        drop,
        claim,
        die
    }

    public enum GoalTags
	{
        wander,
        claim,
        recollect,
        eat,
        sleep
	}
}
