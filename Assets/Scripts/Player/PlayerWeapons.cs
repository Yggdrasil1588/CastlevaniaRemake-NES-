using UnityEngine;

using System.Collections;

//Author: J.Anderson

public class PlayerWeapons : MonoBehaviour
{
    [System.Serializable]
    public class WeaponVariables
    {
        #region Fields
        [SerializeField]
        float lerpTime;
        [SerializeField]
        float phase1Length;
        [SerializeField]
        float phase2Length;
        [SerializeField]
        float throwForce;
        [SerializeField]
        bool wepPhase1 = true;
        [SerializeField]
        bool wepPhase2;
        [SerializeField]
        bool wepPhase3;
        #endregion
        #region Properties
        public float getThrowForce
        {
            get
            {
                return throwForce;
            }
        }
        public float getPhase1Length
        {
            get
            {
                return phase1Length;
            }
        }
        public float getPhase2Length
        {
            get
            {
                return phase2Length;

            }
        }
        public float getLerpTime
        {
            get
            {
                return lerpTime;
            }
        }
        public bool getPhase1
        {
            get
            {
                return wepPhase1;
            }
        }
        public bool getPhase2
        {
            get
            {
                return wepPhase2;
            }
        }
        public bool getPhase3
        {
            get
            {
                return wepPhase3;
            }
        }

        public void PhaseReset()
        {
            wepPhase1 = true;
            wepPhase2 = wepPhase3 = false;
        }
        #endregion
        #region Methods
        public void WeponPhaseSwap(bool phase)
        {
            if (phase)
            {
                wepPhase2 = !wepPhase2;
                wepPhase1 = !wepPhase1;
            }
        }
        public void Phase3(bool setValue)
        {
            wepPhase3 = setValue;
        }
        #endregion
    }

    public WeaponVariables weaponVariables = new WeaponVariables();
    public AmmoManager secondaryAmmo = new AmmoManager();

    ScoreManager scoreManager;
    PlayerMovement playerMovement;

    public Transform instantiatePoint;
    [Header("MainWepTriger")]
    [SerializeField]
    GameObject startPos;
    [SerializeField]
    GameObject endPos;
    [SerializeField]
    GameObject mainWep;

    Collider wepCollider
    {
        get
        {
            return mainWep.GetComponent<Collider>();
        }
        set
        {
            wepCollider = wepCollider;
        }
    }

    bool canFire = true;
    bool facingLeft
    {
        get
        {
            return playerMovement.facingLeftCheck;
        }
    }

    // Weapon selected
    bool dagger;
    bool stopWatch;
    bool holyCross;

    Vector3 playerVelocity;
    Vector3 whipTransTemp;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        MainWeapon();
        SecondaryWeapons();
        WhipLength();
        VelocityCheck();
        if (Input.GetKeyDown(KeyCode.R))
            weaponVariables.PhaseReset();
    }

    void VelocityCheck()
    {
        //Grabs the current velocity of the player to add to weapon throw force
        if (facingLeft)
            playerVelocity = gameObject.GetComponent<Rigidbody>().velocity;
        else if (!facingLeft)
            playerVelocity = -gameObject.GetComponent<Rigidbody>().velocity;
    }

    void MainWeapon()
    {
        if (Input.GetButtonDown("Fire1") && canFire)
        {
            StartCoroutine(WepColliderLoop());
        }
    }

    void WhipLength()
    {

        whipTransTemp = startPos.transform.position;
        Vector3 whipLength = new Vector3(whipTransTemp.x, whipTransTemp.y, whipTransTemp.z);
        // sets distance the collider can travel based on what lvl the whip upgrade is
        if (weaponVariables.getPhase1)
        {
            // moves based on direction
            if (facingLeft)
                whipLength.x = whipLength.x + weaponVariables.getPhase1Length;
            else if (!facingLeft)
                whipLength.x = whipLength.x - weaponVariables.getPhase1Length;

            endPos.transform.position = whipLength;
        }

        if (weaponVariables.getPhase2)
        {
            if (facingLeft)
                whipLength.x = whipLength.x + weaponVariables.getPhase2Length;
            else if (!facingLeft)
                whipLength.x = whipLength.x - weaponVariables.getPhase2Length;

            endPos.transform.position = whipLength;
        }

        // on phase 3 increases weapon damage
        if (weaponVariables.getPhase3)
        {
            mainWep.GetComponent<WeaponDamage>().damage = 3;
        }
    }


    IEnumerator WepColliderLoop()
    {
        // lerps the collider to its phase distance and back again
        canFire = false;
        wepCollider.enabled = true;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / weaponVariables.getLerpTime)
        {
            mainWep.transform.position = Vector3.Lerp(startPos.transform.position, endPos.transform.position, t);
            yield return null;
        }
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / weaponVariables.getLerpTime)
        {
            mainWep.transform.position = Vector3.Lerp(endPos.transform.position, startPos.transform.position, t);
            yield return null;
        }
        wepCollider.enabled = false;
        canFire = true;
        print("main wep coroutine finished");
    }

    void SecondaryWeapons()
    {
        SecondaryWeaponDagger();
        SecondaryWeaponHolyCross();
        SecondaryWeaponStopWatch();
    }

    // sets weapon bool based on current weapon selected in submenu
    public void CheckSelectedWeapon(string currentWep)
    {
        dagger = currentWep == "Dagger" ? true : false;
        stopWatch = currentWep == "StopWatch" ? true : false;
        holyCross = currentWep == "HolyCross" ? true : false;
    }

    // instantiates the dagger prefab at spawn point
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
                Vector3 velocity = new Vector3(playerVelocity.x + weaponVariables.getThrowForce, rb.velocity.y, rb.velocity.z);
                if (facingLeft)
                    rb.velocity = velocity;
                else if (!facingLeft)
                    rb.velocity = -velocity;
                secondaryAmmo.RemoveAmmo(1);
            }
        }
    }

    // starts coroutine to freeze enemies
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

    // finds all spawned enemies and kills them, needs to be changed to enemies on screen
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

    // coroutine finds all enemies and freezes their movement for set time
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
