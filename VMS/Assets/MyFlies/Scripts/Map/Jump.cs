using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {

    public static CharacterController controller;
    public float speed = 1;

    public float jumpSpeed = 10;

    public float gravity = 20;

    public float margin = 0.1f;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }


    // Update is called once per frame  
    void Update()
    {
        if (!PlayerAllController.mviewbool) return;

        // 控制移动  
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(h, 0, v);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            // 空格键控制跳跃  
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
