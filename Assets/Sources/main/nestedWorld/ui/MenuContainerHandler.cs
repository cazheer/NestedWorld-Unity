using UnityEngine;

namespace nestedWorld.ui
{
    public class MenuContainerHandler : MonoBehaviour
    {
        void Start ()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
