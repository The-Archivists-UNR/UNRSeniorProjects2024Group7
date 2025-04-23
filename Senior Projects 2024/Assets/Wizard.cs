using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Wizard : MonoBehaviour
{

    public AOEWeapon weapon;
    public Animator animator;
    public float attackCooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Attacks every 4 seconds
        attackCooldown += Time.deltaTime;

        if (animator.GetBool("isAttacking"))
        {
            animator.SetBool("isAttacking", false);
        }

        if (attackCooldown > 5)
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(WaitThenDoSomething());
            attackCooldown = 0;
        }

        
    }

    private IEnumerator WaitThenDoSomething()
    {
        yield return new WaitForSeconds(1f);

        weapon.StartAttack();
    }
}
