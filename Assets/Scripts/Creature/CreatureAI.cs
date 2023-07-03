using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureAI : MonoBehaviour
{

    [SerializeField] float chaseRange = 5f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float turnSpeed = 5f;

    bool isProvoked = false;
    float distanceToTarget = Mathf.Infinity;
    Transform target;

    Collider creatureCollider;
    NavMeshAgent navMeshAgent;
    Animator animator;
    
    private void Awake()
    {
        creatureCollider = GetComponent<Collider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<Character>().transform;
    }

    private void Start()
    {
        navMeshAgent.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
    }

    void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        } 
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    void ChaseTarget()
    {
        animator.SetTrigger("Move"); // TODO - only set trigger once. If more than once, will store a 2nd trigger causing a state loop.
        navMeshAgent.SetDestination(target.position);
    }

    void AttackTarget()
    {
        animator.SetBool("Attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void Die()
    {
        animator.SetBool("Die", true);
        creatureCollider.enabled = false;
        navMeshAgent.SetDestination(transform.position);
        enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
