using _project.Code.session.state.@base;

namespace _project.Code.session.result
{
    public class SessionResultsHandle : IStateObserver
    {
        private int _pointsValue;
        
        public void AddPoints(int pointsValue)
        {
            _pointsValue += pointsValue;
        }

        public void OnStart()
        {
        }

        public void OnPause()
        {
        }

        public void OnEnd()
        {
        }
    }
}