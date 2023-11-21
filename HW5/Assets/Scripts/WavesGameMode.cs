using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WavesGameMode : MonoBehaviour
{
    [SerializeField] Life playerLife;
    [SerializeField] Life playerBaseLife;

    void Start()
    {
        //playerLife.onDeath.AddListener(OnPlayerOrBaseDied);
        //playerBaseLife.onDeath.AddListener(OnPlayerOrBaseDied);
       // EnemiesManager.instance.onChanged.AddListener(CheckWinCondition);
        //WavesManager.instance.onChanged.AddListener(CheckWinCondition);
    }
/*    void OnPlayerOrBaseDied()
    {
        SceneManager.LoadScene("LoseScreen");
    }*/

    void Update()
    {
        if (EnemiesManager.instance.enemies.Count <= 0 && WavesManager.instance.waves.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
        if (playerBaseLife.amount <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }

    }

}
