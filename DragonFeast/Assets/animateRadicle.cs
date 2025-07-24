using UnityEngine;

public class animateRadicle : MonoBehaviour
{
    [Header("Base")]
    [Tooltip("If left as (0,0,0), the current localScale at Awake() will be used.")]
    public Vector3 baseScale = Vector3.zero;

    [Header("Scale Range (as multipliers of baseScale)")]
    public float minMultiplier = 0.9f;
    public float maxMultiplier = 1.1f;

    [Header("Timing")]
    [Tooltip("How fast it pulses (cycles per second).")]
    public float frequency = 1.0f;
    public bool useUnscaledTime = false;

    [Header("Optional shaping")]
    public bool useCurve = false;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [Header("Axes mask (1 = affect, 0 = ignore)")]
    public Vector3 axisMask = Vector3.one;

    float _phaseOffset;

    void Awake()
    {
        if (baseScale == Vector3.zero)
            baseScale = transform.localScale;

        // Randomize start so multiple objects don't pulse in sync (optional)
        _phaseOffset = Random.value * Mathf.PI * 2f;
    }

    void Update()
    {
        float t = (useUnscaledTime ? Time.unscaledTime : Time.time) * frequency * Mathf.PI * 2f + _phaseOffset;

        // 0..1
        float s = 0.5f * (Mathf.Sin(t) + 1f);

        if (useCurve)
            s = curve.Evaluate(s);

        float mul = Mathf.Lerp(minMultiplier, maxMultiplier, s);

        Vector3 target = baseScale * mul;

        // Apply axis mask (e.g., (1,0,1) to only scale X & Z)
        Vector3 finalScale = new Vector3(
            Mathf.Lerp(baseScale.x, target.x, axisMask.x),
            Mathf.Lerp(baseScale.y, target.y, axisMask.y),
            Mathf.Lerp(baseScale.z, target.z, axisMask.z)
        );

        transform.localScale = finalScale;
    }
}
