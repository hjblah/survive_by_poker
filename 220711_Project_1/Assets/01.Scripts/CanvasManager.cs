using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] CurrentCardList;
    [SerializeField] private GameObject ChangeCardUI;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Unit_Player_Manager playerManager;
    [SerializeField] private CardCheck_Manager cardcheckManager;
    [SerializeField] private Text currentMadeTxt;

    private List<Card_Index> _CardList = new List<Card_Index>();
    private List<Card_Index> BeforeCardList = new List<Card_Index>();
    private int isCardChanged = 0;
    public int CardChangeChange = 1;


    private void Start()
    {
        for (int i = 0; i < 13; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                Card_Index indx = new Card_Index();
                indx.shape = j;
                indx.number = i;
                _CardList.Add(indx);
            }
        }

        GetCard();
        CardImage_Change();
    }

    private void Update()
    {
        Debug.Log(_CardList.Count);
    }

    public void CardImage_Change() {
        for (int i = 0; i < CurrentCardList.Length; i++)
        {
            CurrentCardList[i].transform.GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Card/" + CurrentCardList[i].transform.name.Split('_')[0] + "/Image/Card_" + CurrentCardList[i].transform.name.Split('_')[1]);
            CurrentCardList[i].transform.Find("Card_Obj").GetComponent<Image>().sprite =
                Resources.Load<Sprite>("Card/" + CurrentCardList[i].transform.name.Split('_')[0] + "/Image/" + CurrentCardList[i].transform.name.Split('_')[1]);
        }
    }

    public void Onclick_FilpCard(int i){
        //if (isCardChanged == 0) return;

        if (enemyManager.currentWaveStage == 0)
        {
            if (CurrentCardList[i].transform.GetComponent<Animator>().GetBool("Filp"))
            {
                CurrentCardList[i].transform.GetComponent<Animator>().SetBool("Filp", false);
            }
            else {
                CurrentCardList[i].transform.GetComponent<Animator>().SetBool("Filp", true);
            }
        }
    }

    public void StageEnd() {
        ChangeCardUI.SetActive(true);
        isCardChanged = CardChangeChange;
    }

    public void StageStart()
    {
        List<Card_Index> currentGot_CardList = new List<Card_Index>();

        if (!CurrentCardList[0].transform.Find("Back").gameObject.activeSelf
            && !CurrentCardList[1].transform.Find("Back").gameObject.activeSelf
            && !CurrentCardList[2].transform.Find("Back").gameObject.activeSelf
            && !CurrentCardList[3].transform.Find("Back").gameObject.activeSelf
            && !CurrentCardList[4].transform.Find("Back").gameObject.activeSelf)
        {
            for (int i = 0; i < CurrentCardList.Length; i++)
            {
                Card_Index currentGotCard = new Card_Index();
                playerManager.pet[i].transform.name = CurrentCardList[i].transform.name;
                
                Destroy(playerManager.pet[i].transform.GetChild(1).gameObject);
                playerManager.pet[i].transform.GetComponent<Unit_PetManager>().Load_CardObj(CurrentCardList[i].transform.name);
                currentGotCard.shape = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[0]);
                currentGotCard.number = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[1]);

                currentGot_CardList.Add(currentGotCard);
            }
            ChangeCardUI.SetActive(false);
            enemyManager.StartStage();

            int made = cardcheckManager.Check_Deck_Event(currentGot_CardList);

            for (int j = 0; j < CurrentCardList.Length; j++) {
                playerManager.pet[j].transform.GetComponent<Unit_PetManager>().currentCardMade = made;
            }
        }
        else
        {
            GetCard(true);
            CardImage_Change();
            isCardChanged -= 1;
        }
    }

    public void GetCard(bool isTrade = false)
    {
        if (isTrade)
        {
            List<Card_Index> trashCard = new List<Card_Index>();
            BeforeCardList.Clear();

            for (int i = 0; i < CurrentCardList.Length; i++)
            {
                Card_Index beforecard = new Card_Index();

                beforecard.shape = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[0]);
                beforecard.number = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[1]);
                BeforeCardList.Add(beforecard);

                if (CurrentCardList[i].transform.Find("Back").gameObject.activeSelf)
                {
                    Card_Index card = new Card_Index();
                    Card_Index trashcard = new Card_Index();

                    trashcard.shape = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[0]);
                    trashcard.number = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[1]);
                    trashCard.Add(trashcard);
                    CurrentCardList[i].transform.transform.Find("Back").gameObject.SetActive(false);
                    CurrentCardList[i].transform.GetComponent<Animator>().SetBool("Filp", false);

                    int rand = UnityEngine.Random.Range(0, _CardList.Count);

                    card.shape = _CardList[rand].shape;
                    card.number = _CardList[rand].number;
                    _CardList.Remove(card);

                    CurrentCardList[i].transform.name = card.shape.ToString() + "_" + card.number;
                }
            }
            for (int j = 0; j < trashCard.Count; j++)
            {
                _CardList.Add(trashCard[j]);
            }
        }
        else
        {
            for (int i = 0; i < CurrentCardList.Length; i++)
            {
                Card_Index card = new Card_Index();
                int rand = UnityEngine.Random.Range(0, _CardList.Count);

                card.shape = _CardList[rand].shape;
                card.number = _CardList[rand].number;
                _CardList.RemoveAt(rand);

                string num = "";
                switch (card.number)
                {
                    case 12:
                        num = "A";
                        break;
                    case 11:
                        num = "K";
                        break;
                    case 10:
                        num = "Q";
                        break;
                    case 9:
                        num = "J";
                        break;
                    case 8:
                        num = "10";
                        break;
                    case 7:
                        num = "9";
                        break;
                    case 6:
                        num = "8";
                        break;
                    case 5:
                        num = "7";
                        break;
                    case 4:
                        num = "6";
                        break;
                    case 3:
                        num = "5";
                        break;
                    case 2:
                        num = "4";
                        break;
                    case 1:
                        num = "3";
                        break;
                    case 0:
                        num = "2";
                        break;
                }
                //CurrentCardList[i].GetComponentInChildren<Text>().text = num;
                CurrentCardList[i].transform.name = card.shape.ToString() + "_" + card.number;
            }
        }//최초실

        GetCurrentMade();
    }

    public void GetCurrentMade() {
        List<Card_Index> currentGot_CardList = new List<Card_Index>();

        for (int i = 0; i < CurrentCardList.Length; i++)
        {
            Card_Index currentGotCard = new Card_Index();

            currentGotCard.shape = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[0]);
            currentGotCard.number = Convert.ToInt32(CurrentCardList[i].transform.name.Split('_')[1]);

            currentGot_CardList.Add(currentGotCard);
        }
        int made = cardcheckManager.Check_Deck_Event(currentGot_CardList);
        currentMadeTxt.text = made + "";
    }

    public void Onclick_ReturnCard()
    {
        for (int i = 0; i < CurrentCardList.Length; i++)
        {
            if (CurrentCardList[i].name != BeforeCardList[i].shape + "_" + BeforeCardList[i].number)
            {
                Card_Index AddCard = new Card_Index();
                AddCard.shape = Convert.ToInt32(CurrentCardList[i].name.Split('_')[0]);
                AddCard.number = Convert.ToInt32(CurrentCardList[i].name.Split('_')[1]);
                _CardList.Add(AddCard);

                CurrentCardList[i].transform.name = BeforeCardList[i].shape.ToString() + "_" + BeforeCardList[i].number;
          
                Card_Index RemoveCard = new Card_Index();
                RemoveCard.shape = Convert.ToInt32(BeforeCardList[i].shape);
                RemoveCard.number = Convert.ToInt32(BeforeCardList[i].number);
                _CardList.Remove(RemoveCard);

                StartCoroutine(ReturnCard(i));
            }
        }
    }

    IEnumerator ReturnCard(int i) {
        _CardList.Remove(BeforeCardList[i]);

        CurrentCardList[i].transform.GetComponent<Animator>().SetBool("Filp", true);
        CurrentCardList[i].transform.GetComponent<Image>().raycastTarget = false;
        yield return new WaitForSeconds(0.5f);
        CurrentCardList[i].transform.GetComponent<Animator>().SetBool("Filp", false);
        CurrentCardList[i].transform.GetComponent<Image>().raycastTarget = true;
        CardImage_Change();
        GetCurrentMade();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


[SerializeField]
public class Card_Index
{
    public int shape;
    public int number;
}

