using TMPro;
using UnityEngine;

public class SkillCheckResultTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;

    public void OnSkillCheckEnd(object[] result)
    {
        SkillCheckResultSO skillCheckResult = (SkillCheckResultSO)result[0];
        resultText.text = skillCheckResult.resultText;
    }
}
