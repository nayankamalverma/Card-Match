using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.Script.Gameplay
{
    public class LevelService : MonoBehaviour
    {
        [SerializeField] private CardSO[] cardData;
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform gridTransform;

        [Header("Must be even")]
        [SerializeField] private int column = 4;
        [SerializeField] private int row = 3;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        private int gridLength;
        private List<CardSO> cardDataPairsList = new List<CardSO>();

        private void Start()
        {
            PrepareCardData();
            CreateCards();
        }

        private void CreateCards()
        {
            foreach (var cardData in cardDataPairsList)
            {
                Card card = Instantiate(cardPrefab, gridTransform);
                card.SetReference(cardData, this);
            }
        }

        private void PrepareCardData()
        {
            gridLayoutGroup.constraintCount = column;
            gridLength = (column * row) / 2 ;
            cardDataPairsList.Clear();
            for (int i = 0; i < gridLength; i++)
            {
                cardDataPairsList.Add(cardData[i]);
                cardDataPairsList.Add(cardData[i]);
            }
            ShuffleData();
        }
            
        private void ShuffleData()
        {
            for (int i = cardDataPairsList.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);

                (cardDataPairsList[i], cardDataPairsList[randomIndex]) = (cardDataPairsList[randomIndex], cardDataPairsList[i]);
            }
        }

        public void SetSelected(Card card)
        {
            if (!card.isSelected)
            {
                card.Show();
            }
        }
    }
}