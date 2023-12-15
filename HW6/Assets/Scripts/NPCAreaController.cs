using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCAreaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null)
            {
                return;
            }
            Transform transform = canvas.transform;
            GameObject panel = transform.Find("DialogPanel").gameObject;
            if (panel == null)
            {
                return;
            }

            panel.SetActive(true);
        };
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            GameObject canvas = GameObject.FindWithTag("Canvas");

            if (canvas == null)
            {
                return;
            }
            Transform transform = canvas.transform;
            GameObject panel = transform.Find("DialogPanel").gameObject;
            if (panel == null)
            {
                return;
            }

            panel.SetActive(false);
        }
    }
}
