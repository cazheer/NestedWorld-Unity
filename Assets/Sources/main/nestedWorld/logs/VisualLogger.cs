
using MaterialUI;
using UnityEngine;

namespace nestedWorld.logs
{
    public class VisualLogger
    {
        private static VisualLogger _instance;
        public static VisualLogger Instance
        {
            get { return _instance ?? (_instance = new VisualLogger()); }
        }

        private Color _panelColor = Color.white;
        private Color _textColor = Color.black;

        public void Init(Canvas mainCanvas)
        {
            ToastControl.InitToastSystem(mainCanvas);
        }

        public void Log(string content, float duration = 5.0f, int fontSize = 32)
        {
            ToastControl.MakeToast(content, duration, _panelColor, _textColor, fontSize);
        }
    }
}
