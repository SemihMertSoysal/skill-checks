using UnityEngine;
public class SkillCheckSettings
{
    public float safeAreaRotation;
    [Range(0, 360)]
    public float safeAreaSize;
    [Range(0, 1)]
    public float bonusAreaSize;

    public SkillCheckSettings(float safeAreaSize, float bonusAreaSize)
    {
        this.safeAreaSize = safeAreaSize;
        this.bonusAreaSize = bonusAreaSize;
    }

    public void RandomizeRotation()
    {
        if (safeAreaSize == 0)
            safeAreaSize = Random.Range(0.30f, 0.5f);
        safeAreaRotation = Random.Range(0f, 360f);
    }
}