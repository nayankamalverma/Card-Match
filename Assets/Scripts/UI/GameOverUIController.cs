using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverUIController:MonoBehaviour
    {
        private EventService eventService;

        public void SetReference(EventService eventService)
        {
            this.eventService = eventService;
        }

        public void OnMenuButtonClick()
        {
            eventService.OnMainMenuButtonClick.Invoke();
        }
    }
}