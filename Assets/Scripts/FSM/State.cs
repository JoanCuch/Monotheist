﻿namespace Monotheist.FSM
{
    public interface State
    {
        void Enter();
        void Execute();
        void Exit();
    }
}
