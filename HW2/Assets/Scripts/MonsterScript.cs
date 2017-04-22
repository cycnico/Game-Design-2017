using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MonsterScript : MonoBehaviour
{
    public GameObject FollowTarget;
    public GameObject bulletCandidate;
    public CollisionListScript PlayerSensor;
    public CollisionListScript AttackSensor;
    public float CurrentHP = 100;
    public float MoveSpeed;

    private Animator animator;
    private Rigidbody rigidBody;
    private float MinimumHitPeriod = 1f;
    private float MinimumFirePeriod = 1f;
    private float MinimumShootPeriod = 1f;
    private float HitCounter = 0;
    private float ShootCounter = 0;


    public void AttackPlayer()
    {
        if (AttackSensor.CollisionObjects.Count > 0)
        {
            AttackSensor.CollisionObjects[0].transform.GetChild(1).GetChild(0).SendMessage("Hit", 10);
        }
    }

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidBody = this.GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (PlayerSensor.CollisionObjects.Count > 0)
        {
            FollowTarget = PlayerSensor.CollisionObjects[0].gameObject;
        }

        if (CurrentHP > 0 && HitCounter > 0)
        {
            HitCounter -= Time.deltaTime;
        }
        else
        {
            if (CurrentHP > 0)
            {
                if (FollowTarget != null)
                {
                    Vector3 lookAt = FollowTarget.gameObject.transform.position;
                    lookAt.y = this.gameObject.transform.position.y;
                    this.transform.LookAt(lookAt);
                    animator.SetBool("Run", true);

                    if (AttackSensor.CollisionObjects.Count > 0)
                    {
                        animator.SetBool("Attack", true);
                        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    else
                    {
                        animator.SetBool("Attack", false);
                        rigidBody.velocity = this.transform.forward * MoveSpeed;

                        Vector3 ShootDirection = (FollowTarget.gameObject.transform.position - this.transform.position).normalized;
                        if (ShootCounter <= 0)
                        {
                            ShootCounter = MinimumShootPeriod;
                            GameObject newBullet = GameObject.Instantiate(bulletCandidate);
                            EnemyBulletScript bullet = newBullet.GetComponent<EnemyBulletScript>();
                            bullet.transform.position = this.transform.position;
                            bullet.transform.rotation = this.transform.rotation;
                            bullet.transform.position = new Vector3 (bullet.transform.position.x, bullet.transform.position.y + 1.5f,bullet.transform.position.z);
                            bullet.InitAndShoot(ShootDirection);
                        }
                        else
                        {
                            ShootCounter -= Time.deltaTime;
                        }
                    }
                }
            }
            else
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    public void Hit(float value)
    {
        if (HitCounter <= 0)
        {
            FollowTarget = GameObject.FindGameObjectWithTag("Player");
            HitCounter = MinimumHitPeriod;
            CurrentHP -= value;

            animator.SetFloat("HP", CurrentHP);
            animator.SetTrigger("Hit");

            if (CurrentHP <= 0) { BuryTheBody(); }
        }
    }

    public void OnFire(float value)
    {
        CurrentHP -= value;
        if (HitCounter <= 0)
        {
            FollowTarget = GameObject.FindGameObjectWithTag("Player");
            HitCounter = MinimumHitPeriod;
            animator.SetTrigger("Hit");
        }
        animator.SetFloat("HP", CurrentHP);
        if (CurrentHP <= 0) { BuryTheBody(); }
    }

    void BuryTheBody()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().enabled = false;
        this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(1).OnComplete(() =>
        {
            this.transform.DOMoveY(-0.8f, 1f).SetRelative(true).SetDelay(3).OnComplete(() =>
            {
                GameObject.Destroy(this.gameObject);
            });
        });
    }

}
