using CardMatch.Scripts.Utilities;
using UnityEngine;

namespace CardMatch.Script.Gameplay
{
    public class CardObjectPool : BaseObjectPool<Card>
    {
        private Card cardPrefab;
        private Transform parentTransform;

        public CardObjectPool(Card cardPrefab, Transform parentTransform)
        {
            this.cardPrefab = cardPrefab;
            this.parentTransform = parentTransform;
        }
        protected override Card CreateItem()
        {
            return Object.Instantiate<Card>(cardPrefab, parentTransform);
        }

        public Card GetCard() => GetItem();
        public void ReturnAllCards() => ReturnAllItems();

        protected override void Activate(Card item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void Deactivate(Card item)
        {
            item.gameObject.SetActive(false);
        }
    }
}