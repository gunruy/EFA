using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsText; // UI Text bileþeni
    private float deltaTime = 0.0f;
    private float[] frameTimes = new float[30]; // Son 30 FPS deðeri
    private int index = 0;

    void LateUpdate()
    {
        // FPS Hesapla
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        // Jitter (FPS dalgalanmasý) hesapla
        frameTimes[index] = fps;
        index = (index + 1) % frameTimes.Length;
        float minFps = Mathf.Min(frameTimes);
        float maxFps = Mathf.Max(frameTimes);
        float jitter = maxFps - minFps;

        // Grafik oluþturma gecikmesi (Render Latency)
        float renderLatency = deltaTime * 1000f; // Milisaniye cinsinden

        // UI güncelle
        if (fpsText != null)
        {
            fpsText.text = $"FPS: {fps:F1}\nJitter: {jitter:F1}\nLatency: {renderLatency:F1} ms";
        }
    }
}
