using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer }
    public EnemyState currentState;
    public Sight sightSensor;
    private Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    private NavMeshAgent agent;
    private Animator _animator;
    private float rotationSpeed = 5.0f;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;
    public GameObject shootPoint;
    public ParticleSystem muzzleFlash;
    private void Awake()
    {
        baseTransform = GameObject.Find("BaseDamagePoint").transform;
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>(); // 적에게 할당된 Animator를 가져옴
    }

    private void Update()
    {
        if (currentState == EnemyState.GoToBase)
        {
            GoToBase();
        }
        else if (currentState == EnemyState.AttackBase)
        {
            AttackBase();
        }
        else if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void GoToBase()
    {
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);

        if (sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);

        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
        UpdateAnimatorMovement();
    }

    private void AttackBase()
    {
        agent.isStopped = true;
        LookTo(baseTransform.position);
        ShootBase();
        UpdateAnimatorMovement();
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        agent.SetDestination(sightSensor.detectedObject.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }

        // 애니메이션 업데이트
        UpdateAnimatorMovement();
    }

    private void AttackPlayer()
    {
        agent.isStopped = true;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }

        
        // 애니메이션 업데이트
        UpdateAnimatorMovement();
    }

    private void UpdateAnimatorMovement()
    {
        float verticalInput = agent.velocity.normalized.z; // Y축으로 이동하는 속도
        float horizontalInput = agent.velocity.normalized.x; // X축으로 이동하는 속도

        _animator.SetFloat("MoveVertical", verticalInput, 0.1f, Time.deltaTime);
        _animator.SetFloat("MoveHorizontal", horizontalInput, 0.1f, Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);

            // 총알의 방향을 LookTo 메서드로 설정
            bullet.transform.forward = (sightSensor.detectedObject.transform.position - shootPoint.transform.position).normalized;
            muzzleFlash.Play();
        }
    }
    void ShootBase()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);

            // 총알의 방향을 LookTo 메서드로 설정
            bullet.transform.forward = (baseTransform.position - shootPoint.transform.position).normalized;
            muzzleFlash.Play();
        }
    }
    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(
            targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
