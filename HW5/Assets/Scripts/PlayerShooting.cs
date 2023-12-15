using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    private float shotguncooldownTime = 5f;
    private bool shotgunable = true;
    public Transform yourPlayerTransform;
    public Transform Lighting;
    public ParticleSystem muzzleFlash;
    void Update(){
        
        if (Input.GetKeyDown(KeyCode.Mouse0)&&!UIManager.instance.GameIsPaused)
        {
            ShootSingleBullet();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)&&shotgunable && !UIManager.instance.GameIsPaused)
        {
            ShootShotgun();
        }
    }

    private void ShootSingleBullet()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        GameObject bullet = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation);

        Vector3 playerDirection = yourPlayerTransform.forward;
        muzzleFlash.Play();
        // 총알의 방향을 플레이어의 방향으로 설정
        bullet.transform.forward = playerDirection;
        Lighting.forward = playerDirection;
    }
    private void ShootShotgun()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Vector3 playerDirection = yourPlayerTransform.forward;
        for (int i = 0; i < 5; i++)
        {

            Quaternion spreadRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));

            GameObject bullet = Instantiate(prefab, shootPoint.transform.position, shootPoint.transform.rotation * spreadRotation);
            bullet.transform.forward = playerDirection;
        }
        StartCoroutine(StartCooldown());

    }
    private IEnumerator StartCooldown()
    {
        shotgunable = false;

        yield return new WaitForSeconds(shotguncooldownTime);

        shotgunable = true;
    }
}
