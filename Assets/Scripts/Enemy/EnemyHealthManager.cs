﻿using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class EnemyHealthManager : MonoBehaviour
{
    ScoreManager scoreManager;
    EnemyMove enemyMove;
    Collider enemyCollider;
    SkinnedMeshRenderer[] enemeyRenderer;
    Vector3 respawnPos;
    [SerializeField]
    int enemyMaxHealth;
    int enemyHealth;
    bool isRespawning;

    void Start()
    {
        respawnPos = gameObject.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
        enemyHealth = enemyMaxHealth;
        enemyCollider = gameObject.GetComponent<Collider>();
        enemeyRenderer = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void ReduceHealth(int amount)
    {
        enemyHealth = enemyHealth - amount;
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            scoreManager.AddScore(100);
            StartCoroutine(Respawn());
            enemyHealth = enemyMaxHealth;
            //Death coroutine
        }
    }

    public bool RespawnCheck()
    {
        return isRespawning;
    }

    public IEnumerator Respawn()
    {
        isRespawning = true;
        enemyMove.enabled = !enemyMove.enabled;
        for (int i = 0; i < enemeyRenderer.Length; i++)
        {
            enemeyRenderer[i].enabled = !enemeyRenderer[i].enabled;
        }
        enemyCollider.enabled = !enemyCollider.enabled;
        yield return new WaitForSeconds(4f);
        enemyCollider.enabled = true;
        for (int x = 0; x < enemeyRenderer.Length; x++)
        {
            enemeyRenderer[x].enabled = !enemeyRenderer[x].enabled;
        }
        transform.position = respawnPos;
        enemyMove.enabled = !enemyMove.enabled;
        isRespawning = !isRespawning;
        yield return null;
    }
}
