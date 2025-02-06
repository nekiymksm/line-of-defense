using _project.Code.session.state.@base;

namespace _project.Code.session.result
{
    public class SessionResultHandler : IStateObserver
    {
        private int _pointsValue;
        
        public void AddPoints(int pointsValue)
        {
            _pointsValue += pointsValue;
        }
    }
}