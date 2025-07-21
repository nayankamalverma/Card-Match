namespace Assets.Scripts.Events
{
    public class EventService
    {
        public EventController OnMainMenuButtonClick = new();
        public EventController<int, int> OnGameStart = new();
        public EventController<int, float> OnGameOver = new();
    }
}