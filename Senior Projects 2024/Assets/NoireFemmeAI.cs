using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoireFemmeAI : EnemyAI
{
    public List<GameObject> enemies = new List<GameObject>();
    public Animator animator;
    public float checkForEnemiesTimer;
    public float checkForEnemiesThreshold;
    public float attackCooldown;
    public float attackCooldownThreshold;
    public float attackingTime;
    public float attackingTimeThreshold;
    public bool attacking;
    public float attackRange;
    Entity player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = PlayerMgr.inst.player;
    }

    // Update is called once per frame
    void Update()
    {
        if(checkForEnemiesTimer == 0)
            enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        enemies.RemoveAll(x => x == null);

        Vector3 movePos = Vector3.zero;
        foreach(GameObject go in enemies)
        {
            movePos += go.transform.position;
        }

        if(enemies.Count > 0)
            movePos /= enemies.Count;

        if (movePos != Vector3.zero)
            SetMove(movePos);
        else
            SetMove(GetClosestPointInRadius(player.transform.position, 5));

        if (agent.velocity.magnitude > 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        if (animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", false);
        }

        checkForEnemiesTimer += Time.deltaTime;
        if(!attacking)
            attackCooldown += Time.deltaTime;

        if(attackCooldown > attackCooldownThreshold && !attacking)
        {
            attacking = true;
            animator.SetBool("isAttacking", true);
            attackCooldown = 0;
            foreach(GameObject go in enemies)
            {
                if (Vector3.Distance(transform.position, go.transform.position) < attackRange)
                    go.GetComponent<Entity>().TakeDamage(-10);
            }

            
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
                player.TakeDamage(10);
        }

        if (attacking)
        {
            attackingTime += Time.deltaTime;
            if(attackingTime > attackingTimeThreshold)
            {
                attacking = false;
                attackingTime = 0;
            }
        }

        if (checkForEnemiesTimer > checkForEnemiesThreshold)
            checkForEnemiesTimer = 0;
        //Debug.Log(Vector3.Distance(player.transform.position, transform.position));
    }
}
