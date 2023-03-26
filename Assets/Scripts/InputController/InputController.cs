using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameEvent OnCheckSkillCheck;
    [SerializeField] private GameEvent OnGenerateSkillCheck;
    EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }
    private void Update()
    {
        CheckKeyPress();
    }

    private void CheckKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnCheckKeyPressed();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            OnGenerateKeyPressed();
        }
    }

    private void OnCheckKeyPressed()
    {        
        //Last selected game object set to null to prevent triggering on click event in generate button unintentionally.
        eventSystem.SetSelectedGameObject(this.gameObject, null);
        OnCheckSkillCheck.Raise();
    }

    private void OnGenerateKeyPressed()
    {
        eventSystem.SetSelectedGameObject(this.gameObject, null);
        OnGenerateSkillCheck.Raise();
    }
}
