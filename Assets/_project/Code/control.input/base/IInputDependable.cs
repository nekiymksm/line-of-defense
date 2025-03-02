using UnityEngine;

namespace _project.Code.control.input.@base
{
    public interface IInputDependable
    {
        public void PullTrigger();

        public void HoldTrigger(Vector2 lookValue);

        public void ReleaseTrigger();
    }
}