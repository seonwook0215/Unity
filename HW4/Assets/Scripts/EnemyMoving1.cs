using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving1 : MonoBehaviour
{
    public float speed;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

    }
   
}
