using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_UI : MonoBehaviour
{
    public HorizontalLayoutGroup cardLayout;
    public GameObject card_parent;
    public GameObject card_pos_ins;
    public GameObject card_ins;

    public Sprite[] char_image;
    public int cardCount = 0;

    private float card_sapcing;
    [SerializeField] private GameObject[] unit_CreateList;

    // Start is called before the first frame update
    void Start()
    { 
        card_sapcing = -110f;
    }

    // Update is called once per frame
    void Update()
    {
        Combine_Card_Check();

        TouchEvent();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Card_Create(2000f, 0);
        }

        if (cardLayout.transform.childCount > 5)
        {
            cardLayout.spacing = card_sapcing + (((cardLayout.transform.childCount - 5) * -60f) + (cardLayout.transform.childCount - 5) * 10);
        }
        else
        {
            card_sapcing = -110f;
        }
    }

    bool isDeck = false;
    bool isUseCard = false;
    bool isCancel = false;
    int isUseCard_index = 0;

    void TouchEvent()
    {
        float min_x = card_parent.GetComponent<RectTransform>().position.x - (card_parent.GetComponent<RectTransform>().rect.width / 2);
        float max_x = card_parent.GetComponent<RectTransform>().position.x + (card_parent.GetComponent<RectTransform>().rect.width / 2);
        float min_y = card_parent.GetComponent<RectTransform>().position.y - (card_parent.GetComponent<RectTransform>().rect.height / 2);
        float max_y = card_parent.GetComponent<RectTransform>().position.y + (card_parent.GetComponent<RectTransform>().rect.height / 2);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (min_x < Input.mousePosition.x && max_x > Input.mousePosition.x &&
                min_y < Input.mousePosition.y && max_y > Input.mousePosition.y)
            {
                //???? ????
                isDeck = true;
                isCancel = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (isDeck)
            {
                if (!isUseCard)
                {
                    bool card_select = false;
                    for (int i = card_parent.transform.childCount - 1; i >= 0; i--)
                    {
                        float card_x = card_parent.transform.GetChild(i).GetComponent<RectTransform>().position.x;
                        float card_min_x = card_x - (card_parent.transform.GetChild(i).GetComponent<RectTransform>().rect.width / 2);
                        float card_max_x = card_x + (card_parent.transform.GetChild(i).GetComponent<RectTransform>().rect.width / 2);

                        if (card_min_x < Input.mousePosition.x && card_max_x > Input.mousePosition.x)
                        {
                            //???? ????
                            if (!card_select)
                            {
                                card_parent.transform.GetChild(i).GetComponent<CardControl>().isMove = false;
                                card_parent.transform.GetChild(i).GetComponent<RectTransform>().position = new Vector2(card_x, 400f);
                                isUseCard_index = i;
                                card_select = true;
                            }
                            else
                            {
                                card_parent.transform.GetChild(i).GetComponent<CardControl>().isMove = true;
                            }
                        }
                        else
                        {
                            card_parent.transform.GetChild(i).GetComponent<CardControl>().isMove = true;
                        }
                    }

                    if (card_select)
                    {
                        if (max_y < Input.mousePosition.y)
                        {
                            isUseCard = true;
                            cardLayout.transform.GetComponent<RectTransform>().position = new Vector2(cardLayout.transform.GetComponent<RectTransform>().position.x, -100f);
                        }
                    }
                }
                else
                {
                    card_parent.transform.GetChild(isUseCard_index).GetComponent<RectTransform>().position = Input.mousePosition;

                    if (min_x < Input.mousePosition.x && max_x > Input.mousePosition.x &&
                         min_y < Input.mousePosition.y && max_y > Input.mousePosition.y)
                    {
                        // ???? ????
                        isCancel = true;
                        cardLayout.transform.GetComponent<RectTransform>().position = new Vector2(cardLayout.transform.GetComponent<RectTransform>().position.x, 200f);
                    }
                    else
                    {
                        isCancel = false;
                        cardLayout.transform.GetComponent<RectTransform>().position = new Vector2(cardLayout.transform.GetComponent<RectTransform>().position.x, -100f);
                    }
                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDeck)
            {
                if (card_parent.transform.childCount > 0)
                {
                    isDeck = false;
                    isUseCard = false;
                    if (!isCancel)
                    {
                        for (int i = 0; i < card_parent.transform.childCount; i++)
                        {
                            card_parent.transform.GetChild(i).GetComponent<CardControl>().isMove = false;
                        }

                        Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        Ray2D ray = new Ray2D(wp, Vector2.zero);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                        Create_Unit(card_parent.transform.GetChild(isUseCard_index).name.Split('/')[0], hit.point); //add line

                        DestroyImmediate(card_parent.transform.GetChild(isUseCard_index).gameObject);
                        cardLayout.transform.GetComponent<RectTransform>().position = new Vector2(cardLayout.transform.GetComponent<RectTransform>().position.x, 200f);
                        DestroyImmediate(cardLayout.transform.GetChild(0).gameObject);

                        for (int i = 0; i < cardLayout.transform.childCount; i++)
                        {
                            card_parent.transform.GetChild(i).GetComponent<CardControl>().card_pos_target = i;
                        }

                        if (cardCount > 0)
                        {
                            cardCount--;
                        }

                    }
                    else
                    {
                        isCancel = false;
                    }

                    for (int i = 0; i < card_parent.transform.childCount; i++)
                    {
                        card_parent.transform.GetChild(i).GetComponent<CardControl>().isMove = true;
                    }
                }
            }
        }
    }

    void Combine_Card_Check()
    {
        if (card_parent.transform.childCount > 1)
        {
            for (int i = 0; i < card_parent.transform.childCount; i++)
            {
                for (int j = i + 1; j < card_parent.transform.childCount; j++)
                {
                    if (card_parent.transform.GetChild(i).name == card_parent.transform.GetChild(j).name)
                    {
                        if (card_parent.transform.GetChild(i).GetComponent<CardControl>().starLevel < 3)
                        {
                            card_parent.transform.GetChild(j).GetComponent<CardControl>().isMove = false;
                            string[] split_string = card_parent.transform.GetChild(i).name.Split('/');
                            card_parent.transform.GetChild(i).GetComponent<CardControl>().starLevel++;
                            card_parent.transform.GetChild(i).name = split_string[0] + "/" + split_string[1] + "/" + card_parent.transform.GetChild(i).GetComponent<CardControl>().starLevel;
                            cardCount--;
                            DestroyImmediate(card_parent.transform.GetChild(j).gameObject);
                            DestroyImmediate(cardLayout.transform.GetChild(0).gameObject);

                            Card_Target_Init();

                            break;
                        }
                    }
                }
            }
        }
    }

    void Card_Target_Init()
    {
        for(int i = 0; i < cardCount; i ++)
        {
            card_parent.transform.GetChild(i).GetComponent<CardControl>().card_pos_target = i;
        }
    }

    void Card_Create(float x, float y)
    {
        // ???? ????
        if (cardCount < 10)
        {
            GameObject ins = Instantiate(card_ins, new Vector2(x, y + 1440f), Quaternion.identity, card_parent.transform);
            Instantiate(card_pos_ins, Vector3.zero, Quaternion.identity, cardLayout.transform);
            ins.GetComponent<CardControl>().type = Random.Range(1, 4);
            ins.GetComponent<CardControl>().tier = Random.Range(1, 7);
            ins.GetComponent<CardControl>().charSprite = char_image[ins.GetComponent<CardControl>().type - 1];
            ins.GetComponent<CardControl>().starLevel = 1;
            ins.GetComponent<CardControl>().card_pos_target = cardLayout.transform.childCount - 1;
            ins.GetComponent<CardControl>().isMove = true;
            ins.name = "" + char_image[ins.GetComponent<CardControl>().type - 1].name + "/" + ins.GetComponent<CardControl>().tier + "/" + ins.GetComponent<CardControl>().starLevel;
            ins.GetComponent<CardControl>().CreateCard();
            cardCount++;
        }
    }

    private void Create_Unit(string unit_name, Vector3 pos) {
        GameObject ins;
        switch (unit_name) {
            case "Unit_H_0001":
                ins = Instantiate(unit_CreateList[0], pos, new Quaternion(0, 0, 0, 0));
                ins.transform.SetParent(GameObject.FindWithTag("UnitGroup").transform);
                break;
            case "Unit_H_0002":
                ins = Instantiate(unit_CreateList[1], pos, new Quaternion(0, 0, 0, 0));
                ins.transform.SetParent(GameObject.FindWithTag("UnitGroup").transform);
                break;
                case "Unit_H_0003":
                    ins = Instantiate(unit_CreateList[2], pos, new Quaternion(0, 0, 0, 0));
                    ins.transform.SetParent(GameObject.FindWithTag("UnitGroup").transform);
                    break;
        }
        
    }
}
