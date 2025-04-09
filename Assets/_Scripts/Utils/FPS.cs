using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private float fpsMeasurePeriod = 0.5f;
    private int fpsAccumulator = 0;
    private float fpsNextPeriod = 0;
    private int currentFps;
    private string display = "FPS: {0}";
    private Rect rect = new Rect(50f, 50f, 2000f, 200f);

    void Start()
    {
        Application.targetFrameRate = 60;
        fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
    }
    void Update()
    {
        fpsAccumulator++;

        if (Time.realtimeSinceStartup > fpsNextPeriod)
        {
            currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
            fpsAccumulator = 0;
            fpsNextPeriod += fpsMeasurePeriod;
        }
    }

    void OnGUI()
    {
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 50;
        GUI.Label(rect, string.Format(display, currentFps));
        
    }

}