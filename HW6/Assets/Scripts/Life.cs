using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Life : MonoBehaviour
{

    public float amount;
    public UnityEvent onDeath;
    Animator _animator;
    private void Start()
    {
        _animator = this.GetComponent<Animator>();

    }
    void Update()
    {
        if (amount <= 0)
        {
            _animator.SetInteger("HP", -10);
            if (this.transform.GetComponent<NavMeshAgent>() != null)
            {
                this.transform.GetComponent<NavMeshAgent>().enabled = false;
            }
            Transform aiTransform = this.transform.Find("AI");
            if (aiTransform != null)
            {
                Destroy(aiTransform.gameObject);
            }
            StartCoroutine(WaitSeconds());
        }
    }
    IEnumerator WaitSeconds()
    {

        yield return new WaitForSecondsRealtime(4f);
        onDeath.Invoke();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            print(amount);
            amount--;   
        };
    }
}
