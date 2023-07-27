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
    public Animator fox_walk;
    public Animator blink;

    public AudioSource Walk;
    public AudioSource fox;
    public AudioSource fox2;
    public AudioSource env;



    void Start()
    {
        StartCoroutine(Started());

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "walk")
        {
            Walk.Play();
        }
        if (other.gameObject.tag == "fox")
        {
            fox_walk.SetBool("fox", true);
            StartCoroutine(Started());
            fox.Play();
            fox2.Play();
            StartCoroutine(Blink());

        }
    }

    IEnumerator Started()
    {

        playerSpeed = 0;
        yield return new WaitForSeconds(33);
        playerSpeed = 2;

    }
    IEnumerator Blink()
    {
        yield return new WaitForSeconds(15);
        blink.SetBool("blink", true);
        fox.Stop();
        env.Stop();
        
    }
}

