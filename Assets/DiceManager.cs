using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    [Tooltip("1,2,3,4,5,6")]
    [SerializeField] Sprite[] imagesForDie;
    [SerializeField] Image imageHolderForSprite;
   
    [HideInInspector]
    Card passedInCard;

    [HideInInspector]
    GameManager gManager;

    bool dieHasBeenSelected;
    int dieNumber;
    private void Start()
    {
        gManager = FindObjectOfType<GameManager>();
        PrepairDice();
    }
    public void PrepairDice() 
    {
        imageHolderForSprite.sprite = imagesForDie[Random.Range(0, imagesForDie.Length)];

    }
    public void PassInInfo(Card passIn)
    {
        passedInCard = passIn;
    }

    public void ClickDice()
    {
        if (!dieHasBeenSelected)
        {
            dieHasBeenSelected = true;
            int rangeInt = Random.Range(0, imagesForDie.Length);
            imageHolderForSprite.sprite = imagesForDie[rangeInt];
            dieNumber = rangeInt + 1;
            StartCoroutine(WaitToChange());
        }

    }

    IEnumerator WaitToChange()
    {

        yield return new WaitForSeconds(.5f);
        gManager.SelectedDice(dieNumber);
        yield break;
    }
}
