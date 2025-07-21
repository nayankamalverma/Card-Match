using Assets.Scripts.Events;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{

    private EventService eventService;

    public void SetReference(EventService eventService)
    {
        this.eventService = eventService;
    }

}
