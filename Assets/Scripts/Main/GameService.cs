using Assets.Scripts.UI;
using CardMatch.Script.Gameplay;
using CardMatch.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.Main
{
    public class GameService:MonoBehaviour
    {

        [SerializeField] private UIService UIService;
        [SerializeField] private LevelService levelService;

        private EventService eventService;
        private void Awake()
        {
            eventService = new EventService();
            UIService.SetReferences(eventService);
            levelService.SetReferences(eventService);
        }
    }
}