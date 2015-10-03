using UnityEngine;
using System.Collections;

public class FrontWaterManager : MonoBehaviour
{
    public float OffsetX = 0.0f;
    public float OffsetY = 0.0f;

    private float HorizontalWavePeriod = 2.5f;
    private float HorizontalAmplitude = 0.5f;

    private float VerticalWavePeriod = 3.0f;
    private float VerticalAmplitude = 0.1f;

    // Use this for initialization
    void Start()
    {
   }

    // Update is called once per frame
    void Update()
    {

        UpdateHorizontalOffset();
        UpdateVerticalOffset();

        transform.position = new Vector3(OffsetX, OffsetY, transform.position.z);

    }

    private void UpdateHorizontalOffset()
    {
        float timeInPeriod = Time.timeSinceLevelLoad % HorizontalWavePeriod * 2.0f;
        timeInPeriod -= HorizontalWavePeriod;
        float positionInPhase = timeInPeriod / HorizontalWavePeriod * Mathf.PI; // Normalize

        float normalizedOffset = Mathf.Sin(positionInPhase);
        OffsetX = normalizedOffset * HorizontalAmplitude;

    }

    private void UpdateVerticalOffset()
    {
        float timeInPeriod = Time.timeSinceLevelLoad % VerticalWavePeriod * 2.0f;
        timeInPeriod -= VerticalWavePeriod;
        float positionInPhase = timeInPeriod / VerticalWavePeriod * Mathf.PI; // Normalize

        float normalizedOffset = Mathf.Sin(positionInPhase);
        OffsetY = normalizedOffset * VerticalAmplitude;

    }


    public void OnTransformChildrenChanged()
    {

    }
}
