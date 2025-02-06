namespace _project.Code.session.state.@base
{
    public interface IStateObserver
    {
        public virtual void OnStart()
        {
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnEnd()
        {
        }
    }
}