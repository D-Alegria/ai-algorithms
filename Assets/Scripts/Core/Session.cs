using System;

namespace Core
{
    public interface ISession: IChangeNotifier<SessionState>
    {
        void StartSession();
        void PauseSession();
        void ResumeSession();
        void EndSession();
    }

    public enum SessionState
    {
        Running, Paused, NotRunning
    }

    public class Session : ISession
    {
        static SessionState _state;

        public event Action<SessionState> ONStateChanged;

        // Initialize the single instance
        static Session()
        {
            _state = SessionState.NotRunning;
            Instance = new Session();
        }

        // The property for retrieving the single instance
        public static Session Instance { get; private set; }

        private Session() { }

        public void StartSession()
        {
            if (_state is SessionState.NotRunning)
            {
                _state = SessionState.Running;
                NotifyObservers();
            }
        }

        public void PauseSession()
        {
            if (_state is SessionState.Running)
            {
                _state = SessionState.Paused;
                NotifyObservers();
            }
        }

        public void ResumeSession()
        {
            if (_state is SessionState.Paused)
            {
                _state = SessionState.Running;
                NotifyObservers();
            }
        }

        public void EndSession()
        {
            if (_state is SessionState.Running || _state is SessionState.Paused)
            {
                _state = SessionState.NotRunning;
                NotifyObservers();
            }
        }

        public void NotifyObservers()
        {
            ONStateChanged?.Invoke(_state);
        }
    }
}