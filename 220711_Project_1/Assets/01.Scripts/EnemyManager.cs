using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Transform[] createPose;
    [SerializeField] private GameObject[] enemyIns;
    [SerializeField] private GameObject enemyPoseParent;
    [SerializeField] private GameObject enemyParent;
    [SerializeField] private CanvasManager canvasManager;
    private bool isWaveStart = false;
    private float stagePlayTime = 0f;
    private float stageCreateEnemyTime = 0f;
    public int currentWaveStage = 0;

    private void Update()
    {
        if (isWaveStart) {
            stagePlayTime += Time.deltaTime;
        }

        if (stagePlayTime > 10)
        {
            CancelInvoke("CreateEnemy");

            if (enemyParent.transform.childCount == 0)
            {
                isWaveStart = false;
                stagePlayTime = 0f;
                canvasManager.StageEnd();
                Debug.Log("Stage End");
            }
        }
    }

    private void CreateEnemy() {
        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, createPose.Length);
            int rand2 = Random.Range(0, enemyIns.Length);
            GameObject ins = Instantiate(enemyIns[rand2], createPose[rand].position, createPose[rand].rotation);
            ins.transform.SetParent(enemyParent.transform);
        }
    }

    public void StartStage()
    {
        if (!isWaveStart)
        {
            currentWaveStage++;
            Debug.Log("Start Stage " + currentWaveStage);
            for (int i = 0; i < enemyPoseParent.transform.childCount; i++)
            {
                createPose[i] = enemyPoseParent.transform.GetChild(i).transform;
            }
            InvokeRepeating("CreateEnemy", 5, 3);

            switch (currentWaveStage)
            {
                case 1:
                    stageCreateEnemyTime = 10f;
                    break;
                case 2:
                    stageCreateEnemyTime = 15f;
                    break;
                case 3:
                    stageCreateEnemyTime = 20f;
                    break;
                case 4:
                    stageCreateEnemyTime = 25f;
                    break;
                case 5:
                    stageCreateEnemyTime = 30f;
                    break;
                case 6:
                    stageCreateEnemyTime = 35f;
                    break;
                case 7:
                    stageCreateEnemyTime = 40f;
                    break;
                case 8:
                    stageCreateEnemyTime = 45f;
                    break;
                case 9:
                    stageCreateEnemyTime = 50f;
                    break;
                case 10:
                    stageCreateEnemyTime = 55f;
                    break;
            }
            isWaveStart = true;
        }
    }
}