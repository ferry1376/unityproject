using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private float shotTimer;
    public Camera playerCamera; // Assign this in the Unity Inspector
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool lerpCrouch;
    public float crouchTimer;
    public bool crouching;
    public bool sprinting;
    public GameObject Player { get => player;}
    public GameObject Enemy { get => enemy;}
    [Header("Weapon Values")]

    public Transform gunBarrel;
    [Range(0.1f,10f)]
    public float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");


    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        
            
        }

        if(Input.GetKey(KeyCode.C))
        {
            playerShoot();
        }
    }
    //recieve the inputs from our InputManager.cs and apply the to our chracter controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(playerVelocity * Time.deltaTime);
        // Debug.Log(playerVelocity.y);

    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

     public void Crouch()
     {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
     }

     public void Sprint()
     {
      sprinting = !sprinting;
      if (sprinting)
          speed = 8;

      else
          speed = 5;   
     }
     public void playerShoot()
    {
        // Check if the shotTimer is greater than the fireRate
        if (shotTimer < fireRate) {
        shotTimer += Time.deltaTime;
        return;
        }

        //store reference to the gun barrel.
        Transform gunbarrel = gunBarrel;
        //instantiate a new bullet.
        //  GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet")as GameObject, gunbarrel.position, player.transform.rotation);
        //Vector3 shootDirection = gunbarrel.transform.forward;                                                       

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, playerCamera.transform.rotation);
        Vector3 shootDirection = playerCamera.transform.forward;
        
                                                           
        //add force rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f,3f),Vector3.up) * shootDirection *  40; 
        
        //Debug.Log("Shooting");
        shotTimer = 0;
    }
}
