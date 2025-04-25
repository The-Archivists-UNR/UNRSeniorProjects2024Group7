//Script: PlayerController.cs
//Contributors: Liam Francisco and Fenn Edmonds
//Summary: Called by InputMgr to convert inputs to player actions
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Windows;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    public Entity playerEnt;
    public Animator playerAni;
    public AudioSource dashSound;


    public float dashSpeed;
    public float dashDistance;
    public float dashCooldown;
    public float dashCooldownTimer;
    public float attackCooldown;
    public float attackCooldownTimer;
    public float attackingTime;
    public float attackingTimeThreshold;
    public bool attacking;
    public string attackSoundID;

    private Vector3 dashStartPosiiton;
    private Vector3 dashEndPosiiton;
    private bool dashing;

    // Start is called before the first frame update
    void Start()
    {
        playerEnt = GetComponent<Entity>();
        playerAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashing)
            Dash();
        dashCooldownTimer += Time.deltaTime;
        attackCooldownTimer += Time.deltaTime;
        if (attacking)
        {
            //
            attackingTime += Time.deltaTime;
            if(attackingTime > attackingTimeThreshold)
            {
                attacking = false;
                attackingTime = 0;
                playerAni.SetBool("isAttacking", false);
            }
        }

        if(UnityEngine.Input.GetKeyDown(KeyCode.Q))
        {
            playerEnt.weapons[1].StartAttack();
        }
    }

    public void MovePlayer(Vector2 input)
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        Vector3 moveVector = Vector3.zero;
        //new Vector3(input.x * Time.deltaTime, 0, input.y * Time.deltaTime) * playerEnt.speed;
        forward.y = 0;
        right.y = 0;

        if (input.x >= .5)
        {
            playerAni.SetBool("isForward", true);
            moveVector += right*Time.deltaTime;
        }
            
        if (input.x <= -.5)
        {
            //playerAni.SetBool("isBackward", true);
            moveVector -= right * Time.deltaTime;
        }

        if (input.y >= .5)
        {
            playerAni.SetBool("isForward", true);
            moveVector += forward * Time.deltaTime;
        }
            
        if (input.y <= -.5)
        {
            //playerAni.SetBool("isBackward", true);
            moveVector -= forward * Time.deltaTime;
        }
            
        if (input.y == 0 && input.x == 0)
        {
            playerAni.SetBool("isForward", false);
            //playerAni.SetBool("isBackward", false);
        }

        // playerAni.SetBool("isrunning", true);
        if (dashing)
            return;

        moveVector *= playerEnt.speed;
        transform.position += moveVector;
    }

    public void ChangeDirection(Vector2 mousePos)
    {
        if (dashing)
            return;

        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, float.MaxValue);
        Vector3 lookAtVector = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        transform.LookAt(lookAtVector, Vector3.up);
    }


    public void StartDash(Vector2 mousePos, Vector2 moveInput)
    {
        AudioMgr.Instance.PlaySFX("Dash");
        if (dashCooldown < dashCooldownTimer)
        {
            Vector3 dashDirection;
            if (moveInput != Vector2.zero)
            {
                Vector3 moveVector = Vector3.Normalize(new Vector3(moveInput.x, 0, moveInput.y));
                dashDirection = moveVector;
            }
            else
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit, float.MaxValue);
                Vector3 mouseWorld = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                dashDirection = Vector3.Normalize(mouseWorld - transform.position);
            }

            dashStartPosiiton = transform.position;
            dashEndPosiiton = transform.position + dashDirection * dashDistance;
            dashing = true;
            dashCooldownTimer = 0;
        }
    }

    public void StartAttack()
    {
        //Debug.Log("attack");
        if(attackCooldown < attackCooldownTimer)
        {
            //playerEnt.weapons[0].StartAttack();
            playerAni.SetBool("isAttacking", true);
            // print("here");
            attacking = true;
            AudioMgr.Instance.PlaySFX(attackSoundID, AudioMgr.Instance.sfxSource);
            attackCooldownTimer = 0;
        }
    }

    float dashTimer = 0;
    public void Dash()
    {
        dashTimer += Time.deltaTime;
        if (dashTimer < dashSpeed) 
        { 
            Vector3 newPos = Vector3.Lerp(dashStartPosiiton, dashEndPosiiton, dashTimer/dashSpeed);
            transform.position = newPos;
        }
        else
        {
            dashTimer = 0;
            dashing = false;
        }

    }

    public void Interact()
    {
        foreach(Interactable inter in PlayerMgr.inst.interactables)
        {
            if(inter.interactable)
                inter.Interact();
        }
    }
}
