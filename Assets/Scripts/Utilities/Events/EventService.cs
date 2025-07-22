namespace CardMatch.Scripts.Utilities.Events
{
    public class EventService
    {
        public EventController OnMainMenuButtonClick = new();
        public EventController<int, int> OnGameStart = new();
        public EventController<int, float> OnGameOver = new();

        public EventController IncreaseTurn = new();
        public EventController OnMatchFound = new();
    }
}