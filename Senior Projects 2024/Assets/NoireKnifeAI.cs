using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NoireKnifeAI : EnemyAI
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    public float attackCooldown;
    public float attackingTime;
    public float attackingTimeThreshold;
    public float attackCooldownThreshold;
    public float distanceThreshold;
    public bool attacking;
    public string attackSoundID;
    void Update()
    {
        attackCooldown += Time.deltaTime;
        Transform player = PlayerMgr.inst.player.transform;
        SetMove(player.position);

        if (agent.velocity.magnitude > 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        if (animator.GetBool("isAttacking"))
        {
            attacking = true;
            animator.SetBool("isAttacking", false);
        }

        if (attacking)
        {
            agent.isStopped = true;
            attackingTime += Time.deltaTime;
            if(attackingTime > attackingTimeThreshold)
            {
                agent.isStopped = false;
                attacking = false;
                attackingTime = 0;
            }
        }


        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
        if (Vector3.Distance(player.transform.position, transform.position) < distanceThreshold && attackCooldown > attackCooldownThreshold)
        {
            animator.SetBool("isAttacking", true);
            AudioMgr.inst.PlaySFX(attackSoundID);
            attackCooldown = 0;
        }
    }
}
