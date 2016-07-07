using UnityEngine;
using System.Collections;

public class Logger
{
    private static Logger instance;
    public static Logger Instance
    {
        get
        {
            if (instance == null)
                instance = new Logger();
            return instance;
        }
    }

    Color panelColor = Color.white;
    Color textColor = Color.black;

    public void Init(Canvas mainCanvas)
    {
        MaterialUI.ToastControl.InitToastSystem(mainCanvas);
    }

    public void Log(string content, float duration = 5.0f, int fontSize = 32)
    {
        MaterialUI.ToastControl.MakeToast(content, duration, panelColor, textColor, fontSize);
    }
}
