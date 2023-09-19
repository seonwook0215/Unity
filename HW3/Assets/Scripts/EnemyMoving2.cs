using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving2 : MonoBehaviour
{
    public float speed;
    public float delay;
    private float accTime = 0f;
    private bool life = true;
    private bool isFirst = true, isSecond = true, isThird = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        accTime += Time.deltaTime;
        if (isFirst && accTime >= 10f)
        {
            transform.Rotate(0, 270f, 0);
            isFirst = false;
        }
        else if (isSecond && accTime >= 20f)
        {
            transform.Rotate(0, 270f, 0);
            isSecond = false;
        }
        else if (isThird && accTime >= 30f)
        {
            transform.Rotate(0, 270f, 0);
            isThird = false;
        }
        transform.Translate(0, 0, speed * Time.deltaTime);

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        };
    }
}
