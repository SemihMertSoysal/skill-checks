using UnityEngine;

public class IndicatorMover : MonoBehaviour
{
    private RectTransform indicator;
    private bool skillCheckStarted = false;
    private float failAngle;
    private float speed = 150f;

    private readonly int sliderFactor = 300;

    private void Awake()
    {
        indicator = GetComponent<RectTransform>();
    }

    private void Start()
    {
        SkillCheckManager.Instance.SetIndicator(indicator);
    }

    private void Update()
    {
        RotateIndicator();
    }

    public void OnSkillCheckInfoGenerated(object[] skillSettingsArray)
    {
        skillCheckStarted = true;
    }

    public void SetIndicatorSpeed(float speed)
    {
        this.speed = speed * sliderFactor;
    }

    private void RotateIndicator()
    {
        if (skillCheckStarted)
            indicator.Rotate(Vector3.forward, -speed * Time.deltaTime);
    }

    public void MoveIndicator(float failAngle)
    {
        skillCheckStarted = true;
        this.failAngle = failAngle;
    }

    public void StopIndicator()
    {
        skillCheckStarted = false;
    }

    public void CheckIndicatorAngle()
    {
        if (skillCheckStarted && indicator.rotation.z > failAngle)
        {
            StopIndicator();
        }
    }
}
