using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Image backgroundImgae;
    [SerializeField] private Image typeImage;
    [SerializeField] private Image charImage;
    [SerializeField] private GameObject starGroup;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Sprites")]
    [SerializeField] private Sprite[] background_sprite;
    [SerializeField] private Sprite[] type_sprite;
    [SerializeField] private Sprite[] star_sprite;

    [Header("Card Data")]
    /*
     * type
     * 0 : 탱커
     * 1 : 근접 물리
     * 2 : 원거리 물리
     * 3 : 원거리 마법
     * 4 : 힐러
     */
    public int type;
    public GameObject unit;
    public string unit_name;
    // 1 ~ 6
    public int tier = 1;
    // 1 ~ 3
    public int starLevel = 1;
    public bool awakening = false;

    public Sprite charSprite;

    [Header("Etc")]
    public int card_pos_target = 0;
    public bool isMove;

    // private
    private GameObject _cardLayout;
    private GameObject _cardGroup;
    private Manager_UI _manager_UI;
    private Vector3 _target_pos;

    private void Update()
    {
        if (isMove)
        {
            _target_pos = _cardLayout.transform.GetChild(card_pos_target).position;
            transform.position = Vector3.Lerp(transform.position, _target_pos, 0.1f);

            for (int i = 0; i < starLevel; i++)
            {
                starGroup.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void CreateCard()
    {
        _cardLayout = GameObject.FindWithTag("Card_Layout").gameObject;
        _cardGroup = GameObject.FindWithTag("Card_Group").gameObject;
        _manager_UI = GameObject.FindWithTag("Canvas").GetComponent<Manager_UI>();

        if (tier > 0)
        {
            for (int i = 0; i < 3; i ++)
            {
                starGroup.transform.GetChild(i).GetComponent<Image>().sprite = star_sprite[0];
                starGroup.transform.GetChild(i).gameObject.SetActive(false);
            }

            charImage.sprite = charSprite;

            backgroundImgae.sprite = background_sprite[tier - 1];
            typeImage.sprite = type_sprite[type];
            nameText.text = unit_name;

            for (int i = 0; i < starLevel; i++)
            {
                starGroup.transform.GetChild(i).gameObject.SetActive(true);
            }

            if (awakening)
            {
                for (int i = 0; i < 3; i++)
                {
                    starGroup.transform.GetChild(i).GetComponent<Image>().sprite = star_sprite[2];
                }
            }

            isMove = true;
        }
    }
}
