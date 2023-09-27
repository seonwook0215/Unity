using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementreal : MonoBehaviour
{
    
    public float speed;
    public float runspeed;
    public float jumpforce=4.0f;
    
    public float forceGravity = 10.0f;
    public float rotationSpeed=4.0f;
    private float xRotate = 0.0f;
    private float maxVelocity = 10.0f;

    private bool isGround = true;

    Rigidbody body;
    // Update is called once per frame
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
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
        
        float yRotate = transform.eulerAngles.y + yRotateSize;
        float xRotateSize = -Input.GetAxis("Mouse Y") * rotationSpeed;
        //�����ϱ�
        xRotate = Mathf.Clamp(xRotate + xRotateSize, -45, 80);
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);


        // ���� y�� ȸ������ ���� ���ο� ȸ������ ���
        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� �� ���(�ϴ�, �ٴ��� �ٶ󺸴� ����)

        /*        float yRotateSize = Input.GetAxis("Mouse X") * rotationSpeed;

                float xRotateSize = -Input.GetAxis("Mouse Y") * rotationSpeed;
                
                transform.Rotate(xRotateSize* Time.deltaTime, yRotateSize * Time.deltaTime, 0);*/
    }
    void KeyboardMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0, v);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.AddRelativeForce(movement*runspeed, ForceMode.Force);
        }
        else
        {
            body.AddRelativeForce(movement * speed, ForceMode.Force);
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
    void limietMoveSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxVelocity = 15.0f;
        }
        else
        {
            maxVelocity = 10.0f;
        }
        if (Mathf.Abs(body.velocity.x) > maxVelocity)
        {
            body.velocity = new Vector3(Mathf.Sign(maxVelocity), body.velocity.y, body.velocity.z);
        }
        if (Mathf.Abs(body.velocity.z) > maxVelocity)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(maxVelocity));
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);

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
