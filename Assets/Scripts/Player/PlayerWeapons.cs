using UnityEngine;

using System.Collections;

//Author: J.Anderson

public class PlayerWeapons : MonoBehaviour
{
    [HideInInspector]
    ScoreManager scoreManager;
    public AmmoManager secondaryAmmo = new AmmoManager();
    public Transform instantiatePoint;
    [SerializeField]
    int throwForce;

    [Header("WeaponSelected")]
    bool dagger;
    bool stopWatch;
    bool holyCross;

    Vector3 playerVelocity;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Update()
    {
        playerVelocity = gameObject.GetComponent<Rigidbody>().velocity; //Grabs the current velocity of the player
        MainWeapon();
        SecondaryWeapons();
    }

    void MainWeapon()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("main weapon active");
        }
    }

    void SecondaryWeapons()
    {
        SecondaryWeaponDagger();
        SecondaryWeaponHolyCross();
        SecondaryWeaponStopWatch();
    }

    public void CheckSelectedWeapon(string currentWep)
    {
        dagger = currentWep == "Dagger" ? true : false;
        stopWatch = currentWep == "StopWatch" ? true : false;
        holyCross = currentWep == "HolyCross" ? true : false;
    }

    void SecondaryWeaponDagger()
    {
        if (dagger)
        {
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() > 0)
            {
                GameObject temp = Instantiate(Resources.Load("Dagger", typeof(GameObject)),
                                                instantiatePoint.position,
                                                    instantiatePoint.rotation) as GameObject;
                Rigidbody rb = temp.AddComponent<Rigidbody>();
                Physics.IgnoreCollision(temp.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                rb.useGravity = false;
                rb.velocity = playerVelocity; // Sets the velocity of the projectile to the current player velocity
                rb.AddForce(transform.forward * throwForce); // Adds forward force to the projectile 

                secondaryAmmo.RemoveAmmo(1);
            }
        }
    }
    void SecondaryWeaponStopWatch()
    {
        if (stopWatch)
        {
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() >= 3)
            {
                StartCoroutine(StopWatchCorout());
                secondaryAmmo.RemoveAmmo(3);
                print("Stop-Watch Fire");
            }
        }
    }
    void SecondaryWeaponHolyCross()
    {
        if (holyCross)
        {
            if (Input.GetButtonDown("Fire2") && secondaryAmmo.CheckAmmo() >= 5)
            {
                EnemyHealthManager[] enemy = FindObjectsOfType<EnemyHealthManager>();
                int enemiesKilled = 0;
                for (int i = 0; i < enemy.Length; i++)
                {
                    if (enemy[i].RespawnCheck() == false)
                    {
                        StartCoroutine(enemy[i].Respawn());
                        enemiesKilled++;
                    }
                }
                scoreManager.AddScore(100 * enemiesKilled);
                secondaryAmmo.RemoveAmmo(5);
                print("Holy Cross Fire");
            }
        }
    }

    IEnumerator StopWatchCorout()
    {
        EnemyMove[] enemies = FindObjectsOfType<EnemyMove>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].FreezeEnemy(true);
        }
        yield return new WaitForSeconds(5);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].FreezeEnemy(false);
        }
        yield return null;
    }
}
