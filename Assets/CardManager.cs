using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardManager : MonoBehaviour
{
    public Card card;

    [SerializeField] Image imageContainer;
    [SerializeField] Sprite spriteForImage;
    [SerializeField] GameObject imageForFirstDie;
    [SerializeField] GameObject imageForSecondDie;
    [SerializeField] Image valueForFirstDie;
    [SerializeField] Image valueForSecondDie;
    [Tooltip("1,2,3,4,5,6")]
    [SerializeField] Sprite[] imagesForDieValue;
    [SerializeField] string descriptionText;
    [SerializeField] string nameOfCard;
    [SerializeField] int diceAmount;
    [SerializeField] bool isAttack;
    [SerializeField] bool isDefend;
    [SerializeField] bool isSearch;

    [SerializeField] TextMeshProUGUI nameForCard;
    [SerializeField] TextMeshProUGUI desForCard;

    [HideInInspector]
    public GameManager gManager;

    [HideInInspector]
    CardManager storedCardManager;
    public bool choseCard;
    bool hasSelected;
    public int firstDiceValue;
    public int secondDiceValue;
    bool hasClickedFirstDie;
    bool finishedRollingDice;
    public void Start()
    {
       
        gManager = FindObjectOfType<GameManager>();
    }


    public void PassInCard(Card passIn)
    {
        card = passIn;
    }

    public void UpdateCard(CardManager cm)
    {
        imageForFirstDie.SetActive(false);
        imageForSecondDie.SetActive(false);
        storedCardManager = cm;
       // spriteForImage = card.imageForCard;
        descriptionText = card.cardDes;
        nameOfCard = card.nameOfCard;
        diceAmount = card.howManyDice;
        isAttack = card.isAttack;
        isDefend = card.isDefend;
        isSearch = card.isSearch;


        nameForCard.text = nameOfCard;
        desForCard.text = descriptionText;
        if(diceAmount == 1)
        {
            imageForFirstDie.SetActive(true);
            Debug.Log("trying to set active dice 1");
        }
        if(diceAmount  == 2)
        {
            imageForFirstDie.SetActive(true);
            imageForSecondDie.SetActive(true);
            Debug.Log("trying to set active both dice");
        }
    }

    public void UpdateDiceAmount(int diceNumber)
    {

        if (diceAmount == 1)
        {

            firstDiceValue = diceNumber;
            valueForFirstDie.sprite = imagesForDieValue[firstDiceValue - 1];
            FinishedRollingDice();
            return;
        }
        if (diceAmount == 2)
        {
            if (!hasClickedFirstDie)
            {
                hasClickedFirstDie = true;
                firstDiceValue = diceNumber;
                valueForFirstDie.sprite = imagesForDieValue[firstDiceValue - 1];
                return;
            }
            else if (hasClickedFirstDie)
            {
                secondDiceValue = diceNumber;
                valueForSecondDie.sprite = imagesForDieValue[secondDiceValue - 1];
                FinishedRollingDice();
            }
        }
    }

    void FinishedRollingDice()
    {
        finishedRollingDice = true;
        gManager.CheckRounds();
    }

    public void SelectCard()
    {
        if (!hasSelected)
        {
            hasSelected = true;
            choseCard = true;
            gManager.SelectedCards(card, storedCardManager);
        }
        
    }
}
