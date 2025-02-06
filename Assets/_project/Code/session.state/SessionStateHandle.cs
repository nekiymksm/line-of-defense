using System.Collections.Generic;
using _project.Code.session.state.@base;
using _project.Code.session.state.enums;

namespace _project.Code.session.state
{
    public class SessionStateHandle
    {
        private List<IStateObserver> _observers;

        public SessionStateHandle()
        {
            _observers = new List<IStateObserver>();
        }

        public void AddObserver(IStateObserver observer)
        {
            _observers.Add(observer);
        }

        public void SetState(SessionStateKind stateKind)
        {
            switch (stateKind)
            {
                case SessionStateKind.Start:
                {
                    foreach (var observer in _observers)
                    {
                        observer.OnStart();
                    }
                }
                    break;

                case SessionStateKind.Pause:
                {
                    foreach (var observer in _observers)
                    {
                        observer.OnPause();
                    }
                }
                    break;

                case SessionStateKind.End:
                {
                    foreach (var observer in _observers)
                    {
                        observer.OnEnd();
                    }
                }
                    break;
            }
        }
    }
}