using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator _animator;
    public Camera _camera;
    CharacterController _controller;
    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public float smoothness = 10f;
    public bool toggleCameraRotation;
    public bool run;
    public bool jumpFlag = false;
    public float gravity = -18f;
    public float jumpForce = 18f;
    private Vector3 moveDirection;
    public AudioSource audiosource;
    public ParticleSystem runningEffect;
    public bool grounded = true;
    private bool isPlayingWalking = false;
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        
        _controller = this.GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if(verticalInput!=0 || horizontalInput != 0)
        {
            if (isPlayingWalking)
            {

            }
            else
            {
                isPlayingWalking = true;
                audiosource.Play();
            }

        }
        else
        {
            isPlayingWalking = false;
            audiosource.Stop();
        }
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
            runningEffect.Play();
        }
        else
        {
            run = false;
        }

        InputMovement();
    }

    private void LateUpdate()
    {
        if (toggleCameraRotation != true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }
    void InputMovement()
    {
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = forward * verticalInput + right * horizontalInput;
        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
        _animator.SetFloat("MoveVertical", verticalInput, 0.1f, Time.deltaTime);
        _animator.SetFloat("MoveHorizontal", horizontalInput, 0.1f, Time.deltaTime);


        if (!_controller.isGrounded)
        {
            _animator.SetBool("IsGround", true);
            moveDirection.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpTo();
        }

        _controller.Move(moveDirection.normalized* finalSpeed * Time.deltaTime);
    }
    public void JumpTo()
    {
        if (_controller.isGrounded == true)
        {
            
            _animator.SetBool("IsGround", false);
            moveDirection.y += jumpForce;
            _controller.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    /*    void InputMovement()
        {

            finalSpeed = (run) ? runSpeed : speed;

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            Vector3 left = transform.TransformDirection(Vector3.left);
            Vector3 back = transform.TransformDirection(Vector3.back);
            moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
            float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
            _animator.SetFloat("Blend", percent, 0.1f, Time.deltaTime);
            if (_controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("jmp");
                    moveDirection.y += jumpSpeed * Time.deltaTime;
                    _animator.SetBool("IsGround", false);
                }

            }
            else
            {
                _animator.SetBool("IsGround", true);
                moveDirection.y -= gravity * Time.deltaTime;
            }

            _controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);

        }*/
}
