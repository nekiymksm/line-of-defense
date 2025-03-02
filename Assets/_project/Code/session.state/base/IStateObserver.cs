namespace _project.Code.session.state.@base
{
    public interface IStateObserver
    {
        public void OnStart();

        public void OnPause();

        public void OnEnd();
    }
}