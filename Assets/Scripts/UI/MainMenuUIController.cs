using Assets.Scripts.Events;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectUiPanel;
    private EventService eventService;

    public void SetReference(EventService eventService)
    {
        this.eventService = eventService;
    }

    private void OnEnable()
    {
        levelSelectUiPanel.SetActive(false);
    }

    public void Play2x3() => eventService.OnGameStart.Invoke(2, 3);
    public void Play3x4() => eventService.OnGameStart.Invoke(3,4);
    public void Play4x5() => eventService.OnGameStart.Invoke(4, 5);
    public void Play4x7() => eventService.OnGameStart.Invoke(4, 7);

    public void ExitApp() => Application.Quit();

}
