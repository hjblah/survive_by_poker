using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject player_map_group;
    [SerializeField] private GameObject enemy_map_group;
    [SerializeField] private GameObject floor_block_1;
    [SerializeField] private GameObject floor_block_2;

    int block_count_max_x = 15;
    int block_count_max_y = 10;
    float player_start_x = -0.35f;
    float player_start_y = 0;
    float enemy_start_x = 0.35f;
    float enemy_start_y = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateMap(floor_block_1, floor_block_2);
    }

    public void CreateMap(GameObject block_1, GameObject block_2)
    {
        for (int i = 0; i < block_count_max_y; i++)
        {
            for (int j = 0; j < block_count_max_x; j++)
            {
                GameObject ins = Instantiate(block_1, new Vector3(player_start_x, player_start_y, 0), Quaternion.identity, player_map_group.transform);
                ins.transform.localScale = new Vector3(2f, 2f, 1f);
                ins.GetComponent<SpriteRenderer>().sortingOrder = i;
                player_start_x -= 0.72f;
            }
            player_start_x = -0.35f;
            player_start_y -= 0.5f;
        }

        for (int i = 0; i < block_count_max_y; i++)
        {
            for (int j = 0; j < block_count_max_x; j++)
            {
                GameObject ins = Instantiate(block_2, new Vector3(enemy_start_x, enemy_start_y, 0), Quaternion.identity, enemy_map_group.transform);
                ins.transform.localScale = new Vector3(2f, 2f, 1f);
                ins.GetComponent<SpriteRenderer>().sortingOrder = i;
                enemy_start_x += 0.72f;
            }
            enemy_start_x = 0.35f;
            enemy_start_y -= 0.5f;
        }
    }

}
