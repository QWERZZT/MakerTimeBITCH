using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float gravityScale = 9.8f, speedScale = 5f, jumpForce = 3.5f, turnspeed = 105f;
    private float verticalSpeed = 0f, mouseX = 0f, mouseY = 0f, CurrentAngleX = 0f;
    private CharacterController controller;
    [SerializeField] private Camera goCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
        MoveCharacter();
    }

    void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0f, mouseX * turnspeed * Time.deltaTime, 0f));
        CurrentAngleX += mouseY * turnspeed * Time.deltaTime * -1f;
        CurrentAngleX = Mathf.Clamp(CurrentAngleX, -60f, 60f);
        goCamera.transform.localEulerAngles = new Vector3(CurrentAngleX, 0f, 0f);
    }

    void MoveCharacter()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        velocity = transform.TransformDirection(velocity) * speedScale;
        if(controller.isGrounded)
        {
            verticalSpeed = 0f;
            if(Input.GetButton("Jump"))
            {
                verticalSpeed = jumpForce;
            }
        }
        verticalSpeed -= gravityScale * Time.deltaTime;
        velocity.y = verticalSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
