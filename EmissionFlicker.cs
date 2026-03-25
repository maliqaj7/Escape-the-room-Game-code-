using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EmissionFlicker : MonoBehaviour
{
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;

    public float minInterval = 0.05f;   // Fast, jittery flicker
    public float maxInterval = 0.3f;

    public Material mat1;
    public Material mat2;

    //private Color baseEmissionColor1;
    //private Color baseEmissionColor2;
    private Color originalEmission1;   // stores exact original emission color
    private Color originalEmission2;   // stores exact original emission color

    private Coroutine flickerRoutine;

    private void OnEnable()
    {
        //mat = GetComponent<Renderer>().material;
        //baseEmissionColor1 = mat1.GetColor("_EmissionColor");
        //baseEmissionColor2 = mat2.GetColor("_EmissionColor");

        // Cache original color only once when enabling
        originalEmission1 = mat1.GetColor("_EmissionColor");
        originalEmission2 = mat2.GetColor("_EmissionColor");

        flickerRoutine = StartCoroutine(FlickerRoutine());
    }

    void OnDisable()
    {
        // Stop flicker
        if (flickerRoutine != null)
            StopCoroutine(flickerRoutine);

        // Restore original emission color/intensity
        mat1.SetColor("_EmissionColor", originalEmission1);
        mat2.SetColor("_EmissionColor", originalEmission2);
    }

    private System.Collections.IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // Pick random intensity
            float intensity = Random.Range(minIntensity, maxIntensity);
            float _intensity = Random.Range(minIntensity, maxIntensity);

            // Apply to emission
            mat1.SetColor("_EmissionColor", originalEmission1 * intensity);
            mat2.SetColor("_EmissionColor", originalEmission2 * _intensity);

            // Random wait for uneven flicker timing
            float wait = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(wait);
        }
    }
}
