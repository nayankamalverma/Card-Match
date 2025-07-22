using System.Collections;
using System.Collections.Generic;
using CardMatch.Scripts.Utilities.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.Script.Gameplay
{
    public class LevelService : MonoBehaviour
    {
        [SerializeField] private CardSO[] cardData;
        [Space]
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform gridTransform;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [Space]
        [SerializeField] private float matchingWaiTime = 0.33f;

        private int gridLength;
        private List<CardSO> cardDataPairsList = new List<CardSO>();
        private List<Card> cards = new List<Card>();
        private Card firstSelection;
        private Card SecondSelection;
        
        private CardObjectPool cardObjectPool;
        private EventService eventService;
        public void SetReferences(EventService eventService)
        {
            this.eventService = eventService;
            cardObjectPool = new CardObjectPool(cardPrefab, gridTransform);
            AddEventListeners();
        }

        private void AddEventListeners()
        {
           eventService.OnGameStart.AddListener(GenerateCardGrid);
        }

        private void GenerateCardGrid(int row, int col )
        {
            ClearCards();
            PrepareCardData(row,col);
            SetCardsData();
        }

        private void ClearCards()
        {
            cardObjectPool.ReturnAllCards();
            cards.Clear();
        }

        private void SetCardsData()
        {   
            foreach (var cardData in cardDataPairsList)
            {
                Card card = cardObjectPool.GetCard();
                card.SetReference(cardData, this);
                cards.Add(card);
            }
        }

        private void PrepareCardData(int row, int col)
        {
            gridLayoutGroup.constraintCount = col;
            gridLength = (col * row) / 2 ;
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
                if (firstSelection==null)
                {
                    firstSelection = card;
                    return;
                }
                if (SecondSelection == null)
                {
                    SecondSelection = card;
                    StartCoroutine(Matching(firstSelection, SecondSelection));
                    firstSelection = null;
                    SecondSelection = null;
                    eventService.IncreaseTurn.Invoke();
                }
            }
        }

        private IEnumerator Matching(Card a, Card b)
        {
            yield return new WaitForSeconds(matchingWaiTime);
            if (a.cardData.CardType == b.cardData.CardType)
            {
                eventService.OnMatchFound.Invoke();
                SoundService.Instance.Play(SoundType.Card_Match);
            }
            else
            {
                a.Hide();
                b.Hide();
                SoundService.Instance.Play(SoundType.Card_Mismatch);
            }
        }

        private void OnDestroy()
        {
            eventService.OnGameStart.RemoveListener(GenerateCardGrid);
        }
    }
}