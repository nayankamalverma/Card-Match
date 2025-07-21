using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.Script.Gameplay
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image cardImage;

        private LevelService levelService;
        public CardSO cardData { get; private set; }
        public bool isSelected { get; private set; }

        public void SetReference(CardSO cardData, LevelService levelService)
        {
            this.cardData = cardData;
            this.levelService = levelService;
            Hide();
        }

        public void OnCardClick()
        {
            levelService.SetSelected(this);
        }
        
        public void Show()
        {
            cardImage.sprite = cardData.cardIcon_back;
            isSelected = true;
        }

        public void Hide()
        {
            cardImage.sprite = cardData.cardFrame_front;
            isSelected = false;
        }
    }
}