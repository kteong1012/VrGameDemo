using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 3f;
    public GameOverMenu GameOverPanel;
    public int score;

    private void Start()
    {
        MessageCenter.Instance.RegisterListner(GameMessageType.GameOver, OnGameOver);
        MessageCenter.Instance.RegisterListner(GameMessageType.KillEnemy, OnKillEnemy);
        Restart();
    }
    private void OnDestroy()
    {
        MessageCenter.Instance.UnRegisterListner(GameMessageType.GameOver, OnGameOver);
        MessageCenter.Instance.UnRegisterListner(GameMessageType.KillEnemy, OnKillEnemy);
    }
    private IEnumerator ClearAllEnemies(bool startGenerate = false)
    {
        EnemyController enemy = null;
        while ((enemy = FindObjectOfType<EnemyController>()) != null)
        {
            Destroy(enemy.gameObject);
            yield return null;
        }
        if (startGenerate)
        {
            StartCoroutine(StartGenerateEnemies());
        }
    }

    private void OnGameOver(MessageBase messageBase)
    {
        StopAllCoroutines();
        StartCoroutine(ClearAllEnemies());
        GameOverPanel.SetScore(score);
        GameOverPanel.gameObject.SetActive(true);
    }

    private void GenerateEnemy()
    {
        float x = Random.Range(-10, 10);
        float z = Random.Range(-10, 10);
        while (new Vector2(x, z).magnitude < 5)
        {
            x = Random.Range(-10, 10);
            z = Random.Range(-10, 10);
        }
        Instantiate(enemyPrefab, new Vector3(x, 0, z), Quaternion.identity);
    }

    public void Restart()
    {
        score = 0;
        StopAllCoroutines();
        StartCoroutine(ClearAllEnemies(true));
        GameOverPanel.gameObject.SetActive(false);
    }

    private IEnumerator StartGenerateEnemies()
    {
        while (true)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(interval);
        }
    }


    private void OnKillEnemy(MessageBase messageBase)
    {
        score++;
    }

}
