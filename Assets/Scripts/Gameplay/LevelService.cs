using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatch.Script.Gameplay
{
    public class LevelService : MonoBehaviour
    {
        [SerializeField] private CardSO[] cardData;
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform gridTransform;

        //[Header("row or column one must be even for better ux")] 
        //[SerializeField][Range(2,8)] private int column = 4;
        //[SerializeField][Range(2,4)] private int row = 3;
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [Space]
        [SerializeField] private float matchingWaiTime = 0.03f;

        private int gridLength;
        private List<CardSO> cardDataPairsList = new List<CardSO>();
        private Card firstSelection;
        private Card SecondSelection;
        private EventService eventService;
        public void SetReferences(EventService eventService)
        {
            this.eventService = eventService;
            AddEventListeners();
        }

        private void AddEventListeners()
        {
            
        }


        private void GenerateCardGrid(int row, int col)
        {
            PrepareCardData(row,col);
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
                }
            }
        }

        private IEnumerator Matching(Card a, Card b)
        {
            yield return new WaitForSeconds(matchingWaiTime);
            if (a.cardData.CardType == b.cardData.CardType)
            {
                //on match found logic
            }
            else
            {
                a.Hide();
                b.Hide();
            }
        }
    }
}