using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillCheckGenerator : MonoBehaviour
{
    [SerializeField] private Image circleFill;
    [SerializeField] private Image bonusFill;
    [SerializeField] private RectTransform indicator;
    [SerializeField] private GameEvent OnSkillCheckGenerated;
    [SerializeField] private Transform skillCheckT;
    private float startAngle;
    private float endAngle;
    private float greatStartAngle;
    private float greatEndAngle;
    private float circleFillAmount;
    private float bonusFillAmount;
    private SkillCheckSettings skillCheckSettings;
    private Coroutine scaleCO;

    [Header("Animation Settings")]
    [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private float animationDuration = 0.5f;
    private readonly Vector3 startScale = Vector3.one * 0.5f;
    private readonly Vector3 endScale = Vector3.one;

    private void Start()
    {
        skillCheckSettings = new SkillCheckSettings(0, 0.35f);
    }

    public void HideSkillCheck()
    {
        if (scaleCO != null)
        {
            StopCoroutine(scaleCO);
            scaleCO = null;
        }
        scaleCO = StartCoroutine(ScaleDownCoroutine());
    }

    public void GenerateSkillCheck()
    {
        
        skillCheckSettings.RandomizeRotation();

        circleFillAmount = skillCheckSettings.safeAreaSize;
        bonusFillAmount = skillCheckSettings.bonusAreaSize / 1 * circleFillAmount;
        var circleFillRotation = circleFill.rectTransform.eulerAngles;
        circleFillRotation.z = skillCheckSettings.safeAreaRotation;
        circleFill.rectTransform.eulerAngles = circleFillRotation;
        circleFill.fillAmount = circleFillAmount;
        bonusFill.fillAmount = bonusFillAmount;
        endAngle = (360 + circleFill.rectTransform.eulerAngles.z - (360 * circleFill.fillAmount)) % 360;
        startAngle = circleFill.rectTransform.eulerAngles.z;
        greatStartAngle = bonusFill.rectTransform.eulerAngles.z;
        greatEndAngle = (360 + bonusFill.rectTransform.eulerAngles.z - (360 * bonusFill.fillAmount)) % 360;
        var indicatorEuler = indicator.eulerAngles;
        indicatorEuler.z = endAngle - 50;
        indicator.eulerAngles = indicatorEuler;
        indicator.gameObject.SetActive(true);
        OnSkillCheckGenerated.Raise(new GeneratedSkillCheckInfo(startAngle, endAngle, greatStartAngle, greatEndAngle));

        if (scaleCO != null)
        {
            StopCoroutine(scaleCO);
            scaleCO = null;
        }
        scaleCO = StartCoroutine(ScaleUpCoroutine());
    }

    private IEnumerator ScaleUpCoroutine()
    {
        skillCheckT.gameObject.SetActive(true);
        float time = 0f;
        float t;
        while (time < animationDuration)
        {
            t = curve.Evaluate(time / animationDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

    private IEnumerator ScaleDownCoroutine()
    {
        float time = 0f;
        float t;
        while (time < animationDuration)
        {
            t = curve.Evaluate(time / animationDuration);
            transform.localScale = Vector3.Lerp(endScale, startScale, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = startScale;
        skillCheckT.gameObject.SetActive(false);
    }

    #region SettingsUI
    public void SetSafeAreaSize(float size)
    {
        skillCheckSettings.safeAreaSize = size;
    }

    public void SetBonusAreaSize(float size)
    {
        skillCheckSettings.bonusAreaSize = size;
    }
    #endregion
}
