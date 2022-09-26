using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCard", menuName = " Enemy Card ")]

public class EnemyCard : ScriptableObject
{
    public Sprite spriteForCard;


    
    public string nameOfEnemy;

    [TextArea(20, 10)]
    public string descriptionOfEnemy;
}
