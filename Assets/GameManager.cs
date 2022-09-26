using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> availableCards = new List<Card>();
    List<CardManager> cardManagers = new List<CardManager>();
    [SerializeField] GameObject cardHolder;
    [SerializeField] Transform[] cardSpawn;
    [SerializeField] Transform[] diceSpawns;
    [SerializeField] Transform dieSpawn;
    [SerializeField] GameObject diceObject;
    [SerializeField] TextMeshProUGUI textForAction;
    [SerializeField] float delayBetweenRounds = 2f;
    List<Card> storedCards = new List<Card>();
    List<GameObject> tempCards = new List<GameObject>();
    bool nonDoubleFound;
    int selectedDice;
    int selectRound;
    bool choseCards;
    bool searchingForSecond;
    [HideInInspector]
    CardManager storedCardManager;
    private void Start()
    {

        DisplayFirstCard();
    }
    


    private void Update()
    {
        if (searchingForSecond)
        {
            if (tempCards[0].GetComponent<CardManager>() != null)
            {
                Debug.Log("ttttt");
                Card useCard = CheckDouble(tempCards[0]);


                if (useCard == null)
                {
                  
                        CheckDouble(tempCards[0]);
                    

                    Debug.Log("Trying to find a non double");
                }
                else if (nonDoubleFound && useCard != null)
                {
                    searchingForSecond = false;
                    Debug.Log("ddddd");
                    DisplaySecondCard(useCard);
                }
            }
        }
    }


    public void DisplayFirstCard()
    {
       
        if (selectRound == 0)
        {
            textForAction.text = "Select A Card";
        }
        else
        {
            
            textForAction.text = ("Select Another Card");
        }
        GameObject displayCard = Instantiate(cardHolder, cardSpawn[0]);
       

        if (displayCard.GetComponent<CardManager>() != null)
        {
            displayCard.GetComponent<CardManager>().PassInCard(GrabRandomCard());
            if (availableCards.Contains(displayCard.GetComponent<CardManager>().card))
            {
                availableCards.Remove(displayCard.GetComponent<CardManager>().card);

            }
        }
        
        displayCard.GetComponent<CardManager>().UpdateCard(displayCard.GetComponent<CardManager>());
        tempCards.Add(displayCard);
        searchingForSecond = true;
    }
//        if (displaySecondCard.GetComponent<CardManager>() != null)
//        {
//            Card useCard = CheckDouble(displayCard);
//            if(useCard == null && !nonDoubleFound)
//            {
//                CheckDouble(displayCard);
//    Debug.Log("Trying to find a non double");
//            }
//if (useCard != null && nonDoubleFound)
//{
//    Debug.Log("None double found");
//    displaySecondCard.GetComponent<CardManager>().PassInCard(useCard);
//    if (availableCards.Contains(displaySecondCard.GetComponent<CardManager>().card))
//    {
//        availableCards.Remove(displaySecondCard.GetComponent<CardManager>().card);

//    }

//}
           
//        }

       
//        displaySecondCard.GetComponent<CardManager>().UpdateCard(displaySecondCard.GetComponent<CardManager>());



//    }
     void DisplaySecondCard(Card useCard)
    {
        tempCards.Clear();
        GameObject displaySecondCard = Instantiate(cardHolder, cardSpawn[1]);
        displaySecondCard.GetComponent<CardManager>().PassInCard(useCard);
        if (availableCards.Contains(displaySecondCard.GetComponent<CardManager>().card))
         {
             availableCards.Remove(displaySecondCard.GetComponent<CardManager>().card);

          }
        displaySecondCard.GetComponent<CardManager>().UpdateCard(displaySecondCard.GetComponent<CardManager>());
    }

    public Card CheckDouble(GameObject passIn)
    {
        Card tempCard = GrabRandomCard();
        if (tempCard.typeInt == passIn.GetComponent<CardManager>().card.typeInt)
        {

            return null;
        }
        else
        {
            nonDoubleFound = true;
            return tempCard;
        }
    }
    public Card GrabRandomCard()
    {
        Card randomCard = availableCards[Random.Range(0, availableCards.Count)];
        return randomCard;
    }

    public void SelectedCards(Card passedInCard, CardManager cm)
    {
        storedCardManager = cm;
        storedCards.Add(passedInCard);
        choseCards = true;
        selectRound++;
        Debug.Log("Stored card " + passedInCard.nameOfCard);
        GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
        foreach(GameObject card in cards)
        {
            if (!card.GetComponent<CardManager>().choseCard)
            {
                Destroy(card, .1f);
            }
        }

        RollDiceForCard(passedInCard);
    }

    void RollDiceForCard(Card passIn)
    {
        textForAction.text = "Roll The Cards Dice";
        if (passIn.howManyDice >= 2)
        {
            for (int i = 0; i < passIn.howManyDice; i++)
            {

                GameObject dice = Instantiate(diceObject, diceSpawns[i]);
            }
        }
        else
        {
            GameObject dice = Instantiate(diceObject, dieSpawn);
        }


    }

    public void SelectedDice(int amountOnDie)
    {
        

        storedCardManager.UpdateDiceAmount(amountOnDie);
    }
    

    public void CheckRounds()
    {
        if (selectRound <= 9)
        {

          
                GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
                foreach (GameObject card in cards)
                {
                   
                        Destroy(card, 1f);
                    
                }
            

            StartCoroutine(NewRound());
            GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i] != null)
                {
                    Destroy(dice[i], 1f);
                }
            }

        }
        else
        {
            if (choseCards)
            {
                GameObject[] cards = GameObject.FindGameObjectsWithTag("Card");
                foreach (GameObject card in cards)
                {
                    Destroy(card, 1f);
                }

                GameObject[] dice = GameObject.FindGameObjectsWithTag("Dice");
                for (int i = 0; i < dice.Length; i++)
                {
                    if (dice[i] != null)
                    {
                        Destroy(dice[i], 1f);
                    }
                }
            }

           
        }
       

        
    }


  
    IEnumerator NewRound()
    {
        yield return new WaitForSeconds(delayBetweenRounds);
        searchingForSecond = false;
        nonDoubleFound = false;
        choseCards = false;
        
        DisplayFirstCard();
        yield break;
    }
}
