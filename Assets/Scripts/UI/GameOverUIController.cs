using Assets.Scripts.Events;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverUIController:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI turnsText;
        [SerializeField] private TextMeshProUGUI timeText;

        private EventService eventService;

        public void SetReference(EventService eventService)
        {
            this.eventService = eventService;
            eventService.OnGameOver.AddListener(UpdateScore);
        }

        private void UpdateScore(int turns, float time)
        {
            turnsText.text = "Turns Used: " + turns;
            UpdateTimerText(time);
        }

        public void OnMenuButtonClick()
        {
            eventService.OnMainMenuButtonClick.Invoke();
        }
        private void UpdateTimerText(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timeText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}