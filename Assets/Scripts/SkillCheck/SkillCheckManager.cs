using UnityEngine;

public class SkillCheckManager : MonoBehaviour
{
    [Header("Skill Check Results")]
    [SerializeField] private SkillCheckResultSO failResult;
    [SerializeField] private SkillCheckResultSO goodResult;
    [SerializeField] private SkillCheckResultSO greatResult;
    [Header("Events")]
    [SerializeField] private GameEvent OnSkillCheckEnd;

    private float goodStartAngle;
    private float goodEndAngle;
    private float greatStartAngle;
    private float greatEndAngle;
    private float indicatorAngle;
    private bool skillCheckStart = false;
    private const float angleThreshold = -30;
    private float failAngle;
    private RectTransform indicator;

    public static SkillCheckManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckFail();
    }

    public void OnSkillCheckGenerated(object[] skillCheckSettingsArray)
    {
        GeneratedSkillCheckInfo skillCheckSettings = (GeneratedSkillCheckInfo)skillCheckSettingsArray[0];
        goodStartAngle = skillCheckSettings.startAngle;
        goodEndAngle = skillCheckSettings.endAngle;
        greatStartAngle = skillCheckSettings.greatStartAngle;
        greatEndAngle = skillCheckSettings.greatEndAngle;
        failAngle = angleThreshold + goodEndAngle;
        skillCheckStart = true;
    }

    public void SetIndicator(RectTransform rectTransform)
    {
        indicator = rectTransform;
    }

    public void OnSkillCheck()
    {
        CheckOnInputReceived();
    }

    private void CheckFail()
    {
        if (!skillCheckStart || indicator == null)
            return;
        if (IsBetweenAngles(goodEndAngle, failAngle, indicator.eulerAngles.z))
        {
            if (goodEndAngle == goodStartAngle)
                OnSkillCheckEnd.Raise(goodResult);
            else
                OnSkillCheckEnd.Raise(failResult);
            skillCheckStart = false;
        }
    }

    public void CheckOnInputReceived()
    {
        if (!skillCheckStart || indicator == null)
            return;

        indicatorAngle = indicator.eulerAngles.z;
        OnSkillCheckEnd.Raise(IsBetweenAngles(greatStartAngle, greatEndAngle, indicatorAngle) ? greatResult :
                                goodStartAngle == goodEndAngle ? goodResult :
                                IsBetweenAngles(goodStartAngle, goodEndAngle, indicatorAngle) ? goodResult :
                                failResult);
        skillCheckStart = false;
    }

    private static bool IsBetweenAngles(float angle1, float angle2, float testAngle)
    {
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(angle1, angle2));

        if (angleDifference > 180f)
        {
            float temp = angle1;
            angle1 = angle2;
            angle2 = temp;
        }

        if (angle1 < angle2)
        {
            return !(testAngle >= angle1 && testAngle <= angle2);
        }
        else
        {
            return !(testAngle >= angle1 || testAngle <= angle2);
        }
    }
}
