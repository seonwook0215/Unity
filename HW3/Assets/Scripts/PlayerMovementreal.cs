using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementreal : MonoBehaviour
{
    
    public float speed = 8.0f;
    public float runspeed = 16.0f;
    public float jumpforce=4.0f;
    public float forceGravity = 8.0f;
    public float rotationSpeed=4.0f;
    private float xRotate = 0.0f;
    
    private bool isGround = true;

    Rigidbody body;
    // Update is called once per frame
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    void Update()
    {

    }
    void FixedUpdate()
    {
        body.AddForce(Vector3.down * forceGravity);
        MouseRotation();
        KeyboardMove();
        Jump();
    }
    void MouseRotation()
    {
        float yRotateSize = Input.GetAxis("Mouse X") * rotationSpeed;
        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)
        float xRotateSize = -Input.GetAxis("Mouse Y") * rotationSpeed;
        //�����ϱ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        /*        float yRotateSize = Input.GetAxis("Mouse X") * rotationSpeed;

                float xRotateSize = -Input.GetAxis("Mouse Y") * rotationSpeed;
                
                transform.Rotate(xRotateSize* Time.deltaTime, yRotateSize * Time.deltaTime, 0);*/
    }
    void KeyboardMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate((new Vector3(h, 0, v) * runspeed) * Time.deltaTime);
        }
        else
        {
            transform.Translate((new Vector3(h, 0, v) * speed) * Time.deltaTime);
        }
        
/*        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(0, 2, 0);

        }*/

    }

    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            body.AddRelativeForce(Vector3.up * jumpforce, ForceMode.Impulse);

            isGround = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

}
