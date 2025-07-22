using CardMatch.Scripts.Utilities.Events;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GamePlayUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI matchesText;
        [SerializeField] private TextMeshProUGUI turnText;

        private int turns;
        private int match;
        private float time;
        private int pairs;

        private bool isTimerOn;
        private EventService eventService;
        
        public void SetReference(EventService eventService)
        {
            this.eventService = eventService;
            AddEventListeners();    
        }

        private void AddEventListeners()
        {
            eventService.OnGameStart.AddListener(OnGameStart);
            eventService.OnMatchFound.AddListener(OnMatchFound);
            eventService.IncreaseTurn.AddListener(IncreaseTurn);
        }

        private void Update()
        {
            if(!isTimerOn)return;
            time += Time.deltaTime;
            UpdateTimerText();
        }

        private void OnGameStart(int row,int col)
        {
            pairs = (col * row) / 2;
            isTimerOn = true;
            turns = 0;
            match = 0;
            time = 0;
            UpdateTurnText();
            UpdateMatchText();
            UpdateTimerText();
        }

        private void OnMatchFound()
        {
            match++;
            UpdateMatchText();
            if (match >= pairs) GameOver();
        }

        private void GameOver()
        {
            isTimerOn = false;
            eventService.OnGameOver.Invoke(turns, time);
        }

        public void OnEndGameButtonClicked()
        {
            isTimerOn = false;
            eventService.OnMainMenuButtonClick.Invoke();
        }

        private void IncreaseTurn()
        {
            turns++;
            UpdateTurnText();
        }

        private void UpdateTurnText() => turnText.text = "Turns\n" + turns;
        private void UpdateMatchText() => matchesText.text = "Matches\n" + match;

        private void UpdateTimerText()
        { int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"Time\n{minutes:00}:{seconds:00}";
        }
    }
}