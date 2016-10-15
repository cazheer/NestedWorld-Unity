using UnityEngine;

namespace nestedWorld.logs
{
    public class ConsoleLogger : MonoBehaviour
    {
        private static ConsoleLogger _instance;

        public static ConsoleLogger Instance
        {
            get { return _instance; }
        }

        void Start()
        {
            _instance = this;
        }

        public void Log(string log)
        {
            Debug.Log(log);
        }

        public void LogWarning(string log)
        {
            Debug.LogWarning(log);
        }

        public void LogError(string log)
        {
            Debug.LogError(log);
        }
    }
}
