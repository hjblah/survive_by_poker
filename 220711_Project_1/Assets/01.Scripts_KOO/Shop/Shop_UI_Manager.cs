using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop_UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI goldCountText;
    public TextMeshProUGUI gemCountText;

    private void Start()
    {
    
    }

    private void Update()
    {
        
    }

    public void OnClick_ProductButton(GameObject button)
    {
        string goods = button.transform.GetChild(2).GetChild(0).name.Split('_')[1];
        string price = button.transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text;

        Debug.Log("재화 : " + goods);
        Debug.Log("가격 : " + price);
    }
}
