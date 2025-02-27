using QuanticBrains.MotionMatching.Scripts.Tags;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoireTommyGuy : EnemyAI

{
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public float minRange; // min distance between Sniper and player
    public float maxRange; // max distance between Sniper and player
    Vector3 movePosition; // where the tommy is going to move
    public float attackCooldown;
    public float attackingTime;
    public float attackingTimeThreshold;
    public float attackCooldownThreshold;
    public bool attacking;
    public Animator animator;
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {
        Transform player = PlayerMgr.inst.player.transform;

        //updates move position to make the Sniper be in between minRange and maxRange units away from the player
        float dist = Vector3.Distance(player.position, movePosition);
        if (dist < minRange || dist > maxRange)
        {
            movePosition = GetClosestPointInRadius(player.transform.position, (maxRange + minRange) / 2);
        }
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.transform.position.z), Vector3.up);
        SetMove(movePosition);

        if (agent.velocity.magnitude > 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        //Attacks every 2 seconds
        attackCooldown += Time.deltaTime;

        if (animator.GetBool("isAttacking"))
        {
            attacking = true;
            animator.SetBool("isAttacking", false);
        }

        if (attackCooldown > attackCooldownThreshold)
        {
            entity.weapons[0].StartAttack();
            attacking = true;
            attackCooldown = 0;
            animator.SetBool("isAttacking", true);
        }

        if (attacking)
        {
            agent.isStopped = true;
            attackingTime += Time.deltaTime;
            gun.transform.localPosition = new Vector3(0.0235419f, 0.04015442f, 0.01592415f);
            gun.transform.localEulerAngles = new Vector3(-17.657f, -174.526f, 0);
            //gun.transform.LookAt(new Vector3(player.position.x, transform.position.y, player.transform.position.z), Vector3.up);
            if (attackingTime > attackingTimeThreshold)
            {
                agent.isStopped = false;
                attacking = false;
                attackingTime = 0;
            }
        }

        if(animator.GetBool("isMoving") && !attacking)
        {
            gun.transform.localPosition = new Vector3(0.0018f, 0.0416f, 0.0186f);
            gun.transform.localEulerAngles = new Vector3(-17.657f, -174.526f, -11.5f);
        }

        if (!animator.GetBool("isMoving") && !attacking)
        {
            gun.transform.localPosition = new Vector3(0.0048f, 0.0467f, 0.0198f);
            gun.transform.localEulerAngles = new Vector3(-17.657f, -174.526f, -4.85f);
        }
    }
}
