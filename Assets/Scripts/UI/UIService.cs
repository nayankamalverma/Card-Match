using CardMatch.Scripts.Utilities.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIService : MonoBehaviour
    {
        [SerializeField] private MainMenuUIController mainMenuUiController;
        [SerializeField] private GamePlayUIController gamePlayUIController;
        [SerializeField] private GameOverUIController gameOverUIController;
        private EventService eventService;
        public void SetReferences(EventService eventService)
        {
            this.eventService = eventService;
            mainMenuUiController.SetReference(eventService);
            gamePlayUIController.SetReference(eventService);
            gameOverUIController.SetReference(eventService);
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            eventService.OnMainMenuButtonClick.AddListener(OnMainMenuButtonClick);
            eventService.OnGameStart.AddListener(GameStart);
            eventService.OnGameOver.AddListener(OnGameOver);
        }

        private void OnMainMenuButtonClick()
        {
            mainMenuUiController.gameObject.SetActive(true);
            gamePlayUIController.gameObject.SetActive(false);
            gameOverUIController.gameObject.SetActive(false);
        }

        private void GameStart(int row, int col)
        {
            mainMenuUiController.gameObject.SetActive(false);
            gamePlayUIController.gameObject.SetActive(true);
            gameOverUIController.gameObject.SetActive(false);
        }

        private void OnGameOver(int turns, float time)
        {
            mainMenuUiController.gameObject.SetActive(false);
            gamePlayUIController.gameObject.SetActive(false);
            gameOverUIController.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            eventService.OnMainMenuButtonClick.RemoveListener(OnMainMenuButtonClick);
            eventService.OnGameStart.RemoveListener(GameStart);
            eventService.OnGameOver.RemoveListener(OnGameOver);
        }
    }
}