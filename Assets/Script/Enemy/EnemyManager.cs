using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    //Component
    public AudioClip[] enemyAudioClip;

    private Transform playerPos;

    private Animator enemyAni;

    private NavMeshAgent agent;

    //Health
    public float enemyCurrentHealth;

    private bool enemyIsDeath;


    //Animator
    private int idleBlendRange;

    private int attackBlendRange;

    private int deathBlendRange;

    private int beatenBlendRange;

    private int walkBlendRange;

    //Attack
    public static int enemyDamage;

    float aniTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        enemyAni =GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        enemyCurrentHealth = Random.Range(50, 100);
        enemyDamage = Random.Range(8, 15);
        idleBlendRange = Random.Range(0, 4);
        walkBlendRange = Random.Range(0, 3);
        deathBlendRange = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {              
        attackBlendRange = Random.Range(0, 3);
        //EnemyAI();
    }
    public void EnemyManagement()
    {
        EnemyAnimatorControl();
    }
    private void EnemyHealthManager(float currHealth)
    {
        if(currHealth==0)
        {
            
        }
    }
    private void EnemyDeath()
    {
        AudioSource.PlayClipAtPoint(enemyAudioClip[6], transform.position);
    }

    private void EnemyAnimatorControl()
    {
        enemyAni.SetFloat("IdleBlend", idleBlendRange);
        enemyAni.SetFloat("WalkBlend", walkBlendRange);
        enemyAni.SetFloat("DeathBlend", deathBlendRange);
    }
    private void EnemyAttack()
    {
        enemyDamage = Random.Range(10, 20);
    }
    public void EnemyBeatenManager()
    {
        if(PlayerGunManager.enemyIsBeaten)
        {
            beatenBlendRange = Random.Range(0, 2);
            enemyCurrentHealth -= PlayerGunManager.musketBulletDamage;
            AudioSource.PlayClipAtPoint(enemyAudioClip[Random.Range(19, 24)], PlayerGunManager.shootHit.point);
            enemyAni.SetFloat("BeatenBlend", beatenBlendRange);
            enemyAni.SetBool("IsBeaten", true);
        }       
    }
    //private void EnemyAI()
    //{
    //    StartCoroutine(FindPlayerPos());
    //    agent.destination = playerPos.position;
    //}
    //IEnumerator FindPlayerPos()
    //{
    //    playerPos = GameObject.FindWithTag("Player").transform;
    //    yield return null;
    //}
}
