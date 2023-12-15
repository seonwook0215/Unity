using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemiesManager.instance.AddEnemy(this);
    }

    void OnDestroy()
    {
        EnemiesManager.instance.RemoveEnemy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
