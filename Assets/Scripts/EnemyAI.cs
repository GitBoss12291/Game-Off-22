using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    private Vector3 maxVec = new Vector3(40, 40, 1);
    private Vector3 minVec = new Vector3(-40, -40, 1);
    private Vector3 startingPos;
    private Vector3 newPos;
    private Vector3 roamPos;
    [Tooltip("This is how close the enemy must be to its destination to have reached it")]
    [SerializeField]
    private float reachedPositionDistance = 3f;

    [Tooltip("This is the transform of the player")]
    public GameObject player;
    [Tooltip("This is the transform of the sight range")]
    public Transform sightCircle;
    [Tooltip("This is the transform of the attack range")]
    public Transform attackCircle;
    [Tooltip("This is the script that finds if the player is within the sight range")]
    public SightAndAttackRanges sightScript;
    [Tooltip("This is the script that finds if the player is within the attack range")]
    public SightAndAttackRanges attackScript;
    private Rigidbody2D rb;
    [Tooltip("This is how fast the enemy moves")]
    public float moveSpeed = 5f;
    private Vector2 direction;

    [Tooltip("This is the sight range of the enemy")]
    [SerializeField]
    private float sightRange = 6;
    [Tooltip("This is the attack range of the enemy")]
    [SerializeField]
    private float attackRange = 2;
    private float timeBetweenAttacks;
    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool alreadyAttacked;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        startingPos = transform.position;
        sightCircle.localScale = new Vector3(sightRange, sightRange, 1);
        attackCircle.localScale = new Vector3(attackRange, attackRange, 1);
    }

    private void Update()
    {
        playerInSightRange = sightScript.isTriggered;
        playerInAttackRange = attackScript.isTriggered;
        direction = roamPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        playerInSightRange = sightScript.isTriggered;
        playerInAttackRange = attackScript.isTriggered;

        GetNewPos();  

        if (playerInSightRange == false && playerInAttackRange == false)
        {
	        Patrolling();
        }   
        if (playerInSightRange == true && playerInAttackRange == false)
        {   
            Chasing();
        }
        if (playerInSightRange == true && playerInAttackRange == true)
        {   
            Attacking();    
        }
        
        
    }

    private void Patrolling()
    {
        roamPos = newPos;
    }

    private void Chasing()
    {
        roamPos = player.transform.position;
    }

    private void Attacking()
    {
        roamPos = transform.position;

        if (!alreadyAttacked)
        {
            /*

            Attack Code Here

            */

           alreadyAttacked = true;
           Invoke("ResetAttack", timeBetweenAttacks);
        }
    }

    private void FixedUpdate()
    { 
            moveCharacter();
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void moveCharacter()
    {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private void GetNewPos()
    {
        float x = ((Random.value) * (maxVec.x - minVec.x)) + minVec.x;
        float y = ((Random.value) * (maxVec.y - minVec.y)) + minVec.y;
        float distance = Vector3.Distance(transform.position, newPos);
        if (distance < reachedPositionDistance)
        {
            newPos = startingPos + new Vector3(x, y, 1);
        }
    }
}
