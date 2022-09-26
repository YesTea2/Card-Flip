using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = " Card ")]

public class Card : ScriptableObject
{

    public bool isAttack;
    public bool isDefend;
    public bool isSearch;
    public bool isItem;

    [TextArea(20, 10)]
    public string cardDes;

    public string nameOfCard;
    public Sprite imageForCard;

    public int howManyDice;

    [Tooltip("attack = 1, defend = 2, search = 3, item = 4")]
    public int typeInt;

    
    
}
