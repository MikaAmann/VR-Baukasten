using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public float scrollSpeedX = 0.05f;
    public float scrollSpeedY = 0.02f;
    public float waveFrequency = 1.0f;
    public float waveAmplitude = 0.01f;

    private Renderer rend;
    private Vector2 baseOffset;
    private float waveTime;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Zufälliger Start-Offset für Variation
        baseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    void Update()
    {
        waveTime += Time.deltaTime;

        // Basis-Scroll
        float offsetX = baseOffset.x + scrollSpeedX * Time.time;
        float offsetY = baseOffset.y + scrollSpeedY * Time.time;

        // Wellenbewegung oben drauf (sinusförmig)
        offsetX += Mathf.Sin(waveTime * waveFrequency) * waveAmplitude;
        offsetY += Mathf.Cos(waveTime * waveFrequency * 0.8f) * waveAmplitude;

        rend.material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}