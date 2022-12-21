using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmissionGame : MonoBehaviour
{
    [Header("Game")]
    [SerializeField] private RectTransform enemyArea;
    [SerializeField] private RectTransform slider;
    [SerializeField] private RectTransform point;
    [SerializeField] private RectTransform successPoint;

    [SerializeField] private RectTransform player;
    [SerializeField] private RectTransform enemy;

    [Header("Success Popup")]
    [SerializeField] private GameObject successPopup;
    [SerializeField] private TextMeshProUGUI clearPoint;
    [SerializeField] private Slider clearSlider;


    [Header("Failed Popup")]
    [SerializeField] private GameObject failedPopup;

    public float demage;

    List<RectTransform> successList = new List<RectTransform>();

    bool _point_flag = false;
    float _point_move_speed;
    bool _point_stop = false;
    int _fail_count = 5;

    // Start is called before the first frame update
    void Start()
    {
        demage = 400f;

        _point_move_speed = 10.0f;

        float size = Random.Range(30, 200f);
        float x = Random.Range(0, slider.rect.width - size);
        successPoint.sizeDelta = new Vector2(size, 100f);
        successPoint.anchoredPosition = new Vector2(x + (successPoint.sizeDelta.x / 2), 0);

        successList.Add(successPoint);

        GameObject ins;

        for (int i = 0; i < 4; i++)
        {
            size = Random.Range(30, 200f);
            x = Random.Range(0, slider.rect.width - size);
            ins = Instantiate(successPoint.gameObject, Vector3.zero, Quaternion.identity, successPoint.transform.parent);
            ins.GetComponent<RectTransform>().sizeDelta = new Vector2(size, 100f);
            ins.GetComponent<RectTransform>().anchoredPosition = new Vector2(x + (successPoint.sizeDelta.x / 2), 0);
            successList.Add(ins.GetComponent<RectTransform>());
        }
    }

    int _ani_state = 0;

    // Update is called once per frame
    void Update()
    {
        if (!_point_stop)
        {
            if (!_point_flag)
            {
                if (point.anchoredPosition.x < slider.rect.width)
                {
                    point.anchoredPosition += new Vector2(_point_move_speed, 0);
                }
                else
                {
                    _point_flag = true;
                }
            }
            else
            {
                if (point.anchoredPosition.x > 0)
                {
                    point.anchoredPosition -= new Vector2(_point_move_speed, 0);
                }
                else
                {
                    _point_flag = false;
                }
            }
        }
    }

    public void OnClick_ActionEvent()
    {
        float sizePlus = 1f;
        bool fail_flag = true;

        if (!_point_stop)
        {
            foreach (RectTransform rectTr in successList)
            {
                if (rectTr.anchoredPosition.x - (rectTr.sizeDelta.x / 2) <= point.anchoredPosition.x &&
                   rectTr.anchoredPosition.x + (rectTr.sizeDelta.x / 2) >= point.anchoredPosition.x)
                {
                    enemyArea.anchoredPosition += new Vector2(demage / 2, 0);
                    enemyArea.sizeDelta -= new Vector2(demage, 0);
                    successList.Remove(rectTr);
                    Destroy(rectTr.gameObject);
                    fail_flag = false;
                    _fail_count++;

                    if (_fail_count > 5)
                    {
                        player.transform.localScale += new Vector3(-sizePlus * 3, sizePlus * 3);
                        player.transform.localPosition += new Vector3(1f, -1f);
                        enemy.transform.localScale -= new Vector3(sizePlus, sizePlus);
                        enemy.transform.localPosition += new Vector3(1f, 0f);
                        player.Rotate(new Vector3(0, 0, -5f));
                    }
                    else if(_fail_count < 6)
                    {
                        player.transform.localScale += new Vector3(-sizePlus, sizePlus);
                        player.transform.localPosition += new Vector3(1f, 0f);
                        enemy.transform.localScale -= new Vector3(sizePlus * 3, sizePlus * 3);
                        enemy.transform.localPosition += new Vector3(1f, 1f);
                        enemy.Rotate(new Vector3(0, 0, -5f));
                    }

                    break;
                }
            }

            if (successList.Count == 0)
            {
                clearPoint.text = (_fail_count * 10) + "%";
                clearSlider.value = _fail_count * 0.1f;
                successPopup.SetActive(true);
            }
            else
            {
                if (fail_flag)
                {
                    enemyArea.anchoredPosition -= new Vector2((demage) / 2, 0);
                    enemyArea.sizeDelta += new Vector2((demage), 0);
                    _fail_count--;

                    if (_fail_count > 4)
                    {
                        player.transform.localScale -= new Vector3(-sizePlus * 3, sizePlus * 3);
                        player.transform.localPosition += new Vector3(-1f, 1f);
                        enemy.transform.localScale += new Vector3(sizePlus, sizePlus);
                        enemy.transform.localPosition += new Vector3(-1f, 0f);
                        player.Rotate(new Vector3(0, 0, 5f));
                    }
                    else if(_fail_count < 5)
                    {
                        player.transform.localScale -= new Vector3(-sizePlus, sizePlus);
                        player.transform.localPosition += new Vector3(-1f, 0f);
                        enemy.transform.localScale += new Vector3(sizePlus * 3, sizePlus * 3);
                        enemy.transform.localPosition += new Vector3(-1f, -1f);
                        enemy.Rotate(new Vector3(0, 0, 5f));
                    }
                }

                if (_fail_count == 0)
                {
                    failedPopup.SetActive(true);
                }
            }

            if (_fail_count > 5)
            {
                player.GetChild(0).GetComponent<Animator>().SetBool("Angry", true);
                enemy.GetChild(0).GetComponent<Animator>().SetBool("Tremble", true);
            }
            else if (_fail_count == 5)
            {
                player.GetChild(0).GetComponent<Animator>().SetBool("Angry", false);
                player.GetChild(0).GetComponent<Animator>().SetBool("Tremble", false);
                enemy.GetChild(0).GetComponent<Animator>().SetBool("Angry", false);
                enemy.GetChild(0).GetComponent<Animator>().SetBool("Tremble", false);
            }
            else
            {
                player.GetChild(0).GetComponent<Animator>().SetBool("Tremble", true);
                enemy.GetChild(0).GetComponent<Animator>().SetBool("Angry", true);
            }

            _point_stop = true;
            Invoke("Restart_Point_Move", 2.0f);
        }
    }

    void Restart_Point_Move()
    {
        point.anchoredPosition = new Vector2(0, point.anchoredPosition.y);
        _point_stop = false;
    }
}
