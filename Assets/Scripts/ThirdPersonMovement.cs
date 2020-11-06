using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        // get horizontal and vertical inputs
        float horizontal = Input.GetAxisRaw("LeftStickX");
        float vertical = Input.GetAxisRaw("LeftStickY");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // rotate character to the direction he is moving
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            // smooth out the rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // implement rotation
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // get the direction that we want to move considering camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            // move the character
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
