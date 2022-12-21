using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SortData
{
    public Card_Index index = new Card_Index();
    public int sortNum;
}

public class CardCheck_Manager : MonoBehaviour
{
    int Sort(SortData A, SortData B)
    {
        if (A.index.number < B.index.number) return -1;
        else if (A.index.number > B.index.number) return 1;
        else
        {
            if (A.sortNum < B.sortNum) return -1;
            else if (A.sortNum > B.sortNum) return 1;
        }

        return 0;
    }

    public int Check_Deck_Event(List<Card_Index> card_index)
    {
        List<SortData> sortDataList = new List<SortData>();
        for (int i = 0; i < 5; i++)
        {
            SortData sortData = new SortData();
            sortData.index = card_index[i];
            sortData.sortNum = i;
            sortDataList.Add(sortData);
        }

        sortDataList.Sort(Sort);

        card_index.Clear();

        for(int i = 0; i < 5; i ++)
        {
            card_index.Insert(i, new Card_Index() { shape = sortDataList[i].index.shape, number = sortDataList[i].index.number });
        }

        if (Check_Royal_Straight_Flush(card_index))
        {
            Debug.Log("로열스트레이트 플래쉬!!!!!");
            return 12;
        }

        if (Check_Back_Straight_Flush(card_index))
        {
            Debug.Log("백 스트레이트 플래쉬!!!!!");
            return 11;
        }

        if (Check_Straight_Flush(card_index))
        {
            Debug.Log("스트레이트 플래쉬!!!!!");
            return 10;
        }

        if (Check_Four_Card(card_index))
        {
            Debug.Log("포카드!!!!!");
            return 9;
        }

        if (Check_Full_House(card_index))
        {
            Debug.Log("풀하우스!!!!");
            return 8;
        }

        if (Check_Flush(card_index))
        {
            Debug.Log("플러쉬!!!!!");
            return 7;
        }

        if (Check_Mountain(card_index))
        {
            Debug.Log("마운틴!!!!!");
            return 6;
        }

        if (Check_Back_Straight(card_index))
        {
            Debug.Log("백스트레이트!!!!!");
            return 5;
        }

        if (Check_Straight(card_index))
        {
            Debug.Log("스트레이트!!!!!");
            return 4;
        }

        if (Check_Triple(card_index))
        {
            Debug.Log("트리플!!!!!");
            return 3;
        }

        if (Check_Two_Pair(card_index))
        {
            Debug.Log("투페어!!!!!");
            return 2;
        }

        if (Check_One_Pair(card_index))
        {
            Debug.Log("원페어!!!!!");
            return 1;
        }

        Debug.Log("노페어!!!!");
        return 0;  
    }
    
    bool Check_Royal_Straight_Flush(List<Card_Index> card_index)
    {
        bool check_shape = true;
        bool check_number = false;

        for(int i = 1; i < card_index.Count; i ++)
        {
            if(card_index[0].shape != card_index[i].shape)
            {
                check_shape = false;
                break;
            }
        }

        if(check_shape)
        {
            if(card_index[0].number == 8 &&
                card_index[1].number == 9 &&
                card_index[2].number == 10 &&
                card_index[3].number == 11 &&
                card_index[4].number == 12 )
            {
                check_number = true;
            }
        }

        if(check_shape &&
            check_number)
        {
            return true;
        }

        return false;
    }

    bool Check_Back_Straight_Flush(List<Card_Index> card_index)
    {
        bool check_shape = true;
        bool check_number = false;

        for (int i = 1; i < card_index.Count; i++)
        {
            if (card_index[0].shape != card_index[i].shape)
            {
                check_shape = false;
                break;
            }
        }

        if (check_shape)
        {
            if (card_index[0].number == 0 &&
                card_index[1].number == 1 &&
                card_index[2].number == 2 &&
                card_index[3].number == 3 &&
                card_index[4].number == 12)
            {
                check_number = true;
            }
        }

        if (check_shape &&
            check_number)
        {
            return true;
        }

        return false;
    }

    bool Check_Straight_Flush(List<Card_Index> card_index)
    {
        bool check_shape = true;
        bool check_number = false;

        for (int i = 1; i < card_index.Count; i++)
        {
            if (card_index[0].shape != card_index[i].shape)
            {
                check_shape = false;
                break;
            }
        }

        if (check_shape)
        {
            if (card_index[4].number > 3 &&
                card_index[1].number - card_index[0].number == 1 &&
                card_index[2].number - card_index[0].number == 2 &&
                card_index[3].number - card_index[0].number == 3 &&
                card_index[4].number - card_index[0].number == 4)
            {
                check_number = true;
            }
         }

        if (check_shape &&
            check_number)
        {
            return true;
        }

        return false;
    }

    bool Check_Four_Card(List<Card_Index> card_index)
    {
        int check_count = 1;

        for(int i = 1; i < card_index.Count; i ++)
        {
            if(card_index[0].number == card_index[i].number)
            {
                check_count++;
            }
        }

        if(check_count < 4)
        {
            check_count = 1;

            for (int i = 0; i < card_index.Count; i++)
            {
                if (i != 1)
                {
                    if (card_index[1].number == card_index[i].number)
                    {
                        check_count++;
                    }
                }
            }

            if(check_count == 4)
            {
                return true;
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    bool Check_Full_House(List<Card_Index> card_index)
    {
        List<Card_Index> card_index_temp = new List<Card_Index>();

        for (int i = 0; i < card_index.Count; i++)
        {
            card_index_temp.Add(card_index[i]);
        }

        int[] threeCard_index = new int[3];
        int threeCard_count = 0;

        for (int i = 0; i < card_index_temp.Count; i++)
        {
            threeCard_count = 0;
            threeCard_index[0] = -1;
            threeCard_index[1] = -1;
            threeCard_index[2] = -1;

            for (int j = 0; j < card_index_temp.Count; j++)
            {
                if(card_index_temp[i].number == card_index_temp[j].number)
                {
                    threeCard_index[threeCard_count] = j;
                    threeCard_count++;

                    if(threeCard_count == 3)
                    {
                        break;
                    }
                }
            }


            if (threeCard_count == 3)
            {
                break;
            }
        }

        if(threeCard_count == 3)
        {
            card_index_temp.RemoveAt(threeCard_index[0]);
            card_index_temp.RemoveAt(threeCard_index[1] - 1);
            card_index_temp.RemoveAt(threeCard_index[2] - 2);

            if (card_index_temp[0].number == card_index_temp[1].number)
            {
                return true;
            }
        }

        return false;
    }

    bool Check_Flush(List<Card_Index> card_index)
    {
        bool check_shape = true;

        for (int i = 1; i < card_index.Count; i++)
        {
            if (card_index[0].shape != card_index[i].shape)
            {
                check_shape = false;
                break;
            }
        }

        if(check_shape)
        {
            return true;
        }

        return false;
    }

    bool Check_Mountain(List<Card_Index> card_index)
    {
        if (card_index[0].number == 8 &&
            card_index[1].number == 9 &&
            card_index[2].number == 10 &&
            card_index[3].number == 11 &&
            card_index[4].number == 12)
        {
            return true;
        }

        return false;
    }

    bool Check_Back_Straight(List<Card_Index> card_index)
    {
        if (card_index[0].number == 0 &&
            card_index[1].number == 1 &&
            card_index[2].number == 2 &&
            card_index[3].number == 3 &&
            card_index[4].number == 12)
        {
            return true;
        }

        return false;
    }

    bool Check_Straight(List<Card_Index> card_index)
    {

        if (card_index[4].number > 3 &&
            card_index[1].number - card_index[0].number == 1 &&
            card_index[2].number - card_index[0].number == 2 &&
            card_index[3].number - card_index[0].number == 3 &&
            card_index[4].number - card_index[0].number == 4)
        {
            return true;
        }

        return false;
    }

    bool Check_Triple(List<Card_Index> card_index)
    {
        int check_count = 0;

        for(int i = 0; i < 3; i ++)
        {
            check_count = 0;

            for(int j = i + 1; j < card_index.Count; j ++)
            {
                //Debug.Log("j check !! : " + j);

                if(card_index[i].number == card_index[j].number)
                {
                    check_count++;

                    if (check_count == 2)
                    {
                        break;
                    }
                }
            }

            if(check_count == 2)
            {
                break;
            }
        }

        if(check_count == 2)
        {
            return true;
        }

        return false;
    }

    bool Check_Two_Pair(List<Card_Index> card_index)
    {
        List<Card_Index> card_index_temp = new List<Card_Index>();

        for (int i = 0; i < card_index.Count; i++)
        {
            card_index_temp.Add(card_index[i]);
        }

        int[] twoCard_index = new int[2];
        int twoCard_count = 0;

        for (int i = 0; i < card_index_temp.Count; i++)
        {
            twoCard_count = 0;
            twoCard_index[0] = -1;
            twoCard_index[1] = -1;

            for (int j = 0; j < card_index_temp.Count; j++)
            {
                if (card_index_temp[i].number == card_index_temp[j].number)
                {
                    twoCard_index[twoCard_count] = j;
                    twoCard_count++;

                    if (twoCard_count == 2)
                    {
                        break;
                    }
                }
            }


            if (twoCard_count == 2)
            {
                break;
            }
        }

        if (twoCard_count == 2)
        {
            card_index_temp.RemoveAt(twoCard_index[0]);
            card_index_temp.RemoveAt(twoCard_index[1] - 1);

            if (card_index_temp[0].number == card_index_temp[1].number)
            {
                return true;
            }

            if (card_index_temp[0].number == card_index_temp[2].number)
            {
                return true;
            }

            if (card_index_temp[1].number == card_index_temp[2].number)
            {
                return true;
            }
        }

        return false;
    }

    bool Check_One_Pair(List<Card_Index> card_index)
    {
        int check_count = 0;

        for (int i = 0; i < 4; i++)
        {
            check_count = 0;

            for (int j = i + 1; j < card_index.Count; j++)
            {
                if (card_index[i].number == card_index[j].number)
                {
                    check_count++;

                    if (check_count == 1)
                    {
                        break;
                    }
                }
            }


            if (check_count == 1)
            {
                break;
            }
        }

        if (check_count == 1)
        {
            return true;
        }

        return false;
    }
}
