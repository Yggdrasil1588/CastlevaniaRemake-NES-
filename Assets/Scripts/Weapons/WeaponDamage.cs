using UnityEngine;

using System.Collections;


//Author: J.Anderson

public class WeaponDamage : MonoBehaviour
{
    int enemyLayerMask = 10;
    public int damage;


    public void OnCollisionEnter(Collision wepCollision)
    {
        if (wepCollision.gameObject.layer == enemyLayerMask)
        {
            print("SLAP!!!!");
            wepCollision.gameObject.GetComponent<EnemyHealthManager>().ReduceHealth(1);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == enemyLayerMask)
        {
            print("Main Hit");
            other.gameObject.GetComponent<EnemyHealthManager>().ReduceHealth(1);
        }
    }
}