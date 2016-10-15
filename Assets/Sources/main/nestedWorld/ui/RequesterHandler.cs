using UnityEngine;

namespace nestedWorld.ui
{
    public class RequesterHandler : MonoBehaviour
    {
        void Start ()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
