using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class char_control : MonoBehaviour
{
    private CharacterController controler;

    [SerializeField]
    private float playerSpeed = 5f;

    [SerializeField]
    private float rotateSpeed = 10f;

    [SerializeField]
    private Camera followCamera;

    private Animator anim;
    

    void Start()
    {

        controler = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movement();
        
    }

    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.localEulerAngles.y ,0) * -new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = movementInput.normalized;


        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("feed", true);
        }
        else
        {
            anim.SetBool("feed", false);
        }
        
        if (movementDirection != Vector3.zero)
        {
            anim.SetBool("move", true);

            Quaternion desiredDirection = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredDirection, rotateSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("move", false);
        }

        controler.Move(movementDirection * -playerSpeed * Time.deltaTime);

    }
}
