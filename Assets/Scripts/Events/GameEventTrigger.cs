using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;

    public void TriggerEvent()
    {
        gameEvent.Raise();
    }
}
