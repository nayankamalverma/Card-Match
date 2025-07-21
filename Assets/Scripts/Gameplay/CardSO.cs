using CardMatch.Script.Gameplay;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Objects/CardSO")]
public class CardSO : ScriptableObject
{
    public CardIconType CardType;
    public Sprite cardFrame_front;
    public Sprite cardIcon_back;
}
