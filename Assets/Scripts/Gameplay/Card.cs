using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

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
            Tween.Rotation(transform, new Vector3(0f, 180f, 0f), 0.2f);
            Tween.Delay(0.1f, () => cardImage.sprite = cardData.cardIcon_back);
            
            isSelected = true;
        }

        public void Hide()
        {
            Tween.Rotation(transform, new Vector3(0f, 0f, 0f), 0.2f);
            Tween.Delay(0.1f, () => cardImage.sprite = cardData.cardFrame_front);
            isSelected = false;
        }
    }
}