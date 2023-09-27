using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootPoint;
    private float shotguncooldownTime = 5f;
    private bool shotgunable = true;
    void Update(){
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootSingleBullet();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)&&shotgunable){
            ShootShotgun();
        }
    }

    private void ShootSingleBullet()
    {
        GameObject clone = Instantiate(prefab);
        clone.transform.position = shootPoint.transform.position;
        clone.transform.rotation = shootPoint.transform.rotation;
    }
    private void ShootShotgun()
    {
        for(int i = 0; i < 5; i++)
        {
            Quaternion spreadRotation = Quaternion.Euler(0, 0, Random.Range(-10f, 10f));

            GameObject clone = Instantiate(prefab);
            clone.transform.position = shootPoint.transform.position;
            clone.transform.rotation = shootPoint.transform.rotation* spreadRotation;
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
