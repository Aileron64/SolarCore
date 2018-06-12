using UnityEngine;
using System.Collections;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;

public enum EnemyState
{
    NORMAL, WARP, STUNNED
}

public class BaseEnemy : MonoBehaviour
{
    #region Variables

    public EnemyState gameState;
    public string enemyTag;
    //GameMan GM;

    // Movement Values
    protected Rigidbody rB;
    protected GameObject target;
    protected Vector3 velocity;
    protected Vector3 forceImpact;
    protected float speed;
    protected Quaternion targetRotation;
    protected float rotationSpeed;
    protected float defaultDrag;
    protected float chronoDrag;

    // Default Stats
    public float value = 10;
    public float impactDamage = 10; 
    public float health = 100;
    public float size = 1;
    protected float maxHealth = 100;
    protected float dotDamage;

    // Immunities
    protected bool stunImmune;
    protected bool timeImmune;
    protected bool slowImmune;
    protected bool forceImmune;

    // Other Values
    public Mesh ghostMesh;
    //public GameObject explosionPrefab;
    protected GameObject explosion;
    //bool useOldExplosion = false;
    //protected ParticleSystem defaultExplosionPrefab;
    //protected ParticleSystem defaultExplosion;

    protected Material defaultMat;
    protected Material flashMat;
    protected Material stunMat;

    protected float flashTime = 0.05f;
    protected float flashTimer;
    protected bool isFlashed = false;
    public float restHeight = 0;
    protected float stunTime;
    protected float timeDilation = 1;
    public bool heightLock;
    public bool warpOnEnable;
    public float cameraInfluence;

    protected GameObject warpImage;

    public float GetImpact() { return impactDamage; }

    //public RigidbodyConstraints defaultConstraints;


    #endregion

    #region StartUp Methods

    protected virtual void Awake()
    {
        maxHealth = health;

        warpImage = Instantiate(Resources.Load("Prefabs/WarpImage") as GameObject) as GameObject;

        //if(warpMesh && false) //Turned off for now
        //    warpImage.GetComponent<WarpImage>().SetModel(warpMesh);
        //else
        warpImage.GetComponent<WarpImage>().SetModel(this.gameObject);

        warpImage.SetActive(false);

        if (GetComponent<Renderer>())
            defaultMat = GetComponent<Renderer>().material;
        else
            defaultMat = (Material)Resources.Load("Red Enemy", typeof(Material));


        rB = GetComponent<Rigidbody>();
        //defaultConstraints = GetComponent<Rigidbody>().constraints;

        defaultDrag = rB.drag;
        chronoDrag = defaultDrag * 2;

    }

    protected virtual void Start()
    {       
        FindTarget();

        if(Object.FindObjectOfType<PlayerManager>())
        {
            if (Object.FindObjectOfType<PlayerManager>().coop)
            {
                health *= 1.5f;
            }
        }

        flashMat = (Material)Resources.Load("Flash", typeof(Material));
        stunMat = (Material)Resources.Load("Red Enemy Stunned", typeof(Material));


        explosion = Instantiate(Resources.Load("Prefabs/Ship_Explosion")) as GameObject;
        explosion.gameObject.SetActive(false);
               
        explosion.GetComponent<EnemyExplosion>().Setup(ghostMesh, size);      


        //GM = Object.FindObjectOfType<GameMan>();
        

        if (stunMat == null || flashMat == null)
            Debug.Log("Missing enemy material"); 

    }

    protected virtual void OnEnable()
    {
        ChangeMat(defaultMat);
        health = maxHealth;
        
        timeDilation = 1;
        dotDamage = 0;

        rB.drag = defaultDrag;

        if (warpOnEnable)
        {
            transform.DOMove(new Vector3(transform.position.x, restHeight, transform.position.z),
                BaseLevel.Instance.GetBeatTime() * 5, true)
                .SetEase(Ease.Linear)
                .OnComplete(EndWarp);
        }

        BaseLevel.OnBeat += BeatEvent;
        BaseLevel.OnLevelEnd += LevelEnd;
    }

    public virtual void OnSpawn()
    {
        gameObject.tag = "Untagged";
        gameState = EnemyState.WARP;

        warpOnEnable = true;

        warpImage.transform.position = new Vector3(transform.position.x, restHeight, transform.position.z);
        warpImage.transform.rotation = gameObject.transform.rotation;
        warpImage.SetActive(true);
    }

    #endregion

    #region UpdateMethods

    protected virtual void FixedUpdate()
    {
        if (health <= 0)
        {
            Explode();
        }      

        switch (gameState)
        {
            default:
            case EnemyState.NORMAL:
                Normal();
                break;

            case EnemyState.WARP:
                Warp();
                break;

            case EnemyState.STUNNED:
                Stunned();
                break;
        }

        if (isFlashed)
        {
            flashTimer += Time.deltaTime;

            if (flashTimer >= flashTime)
            {
                if (gameState != EnemyState.STUNNED)
                    ChangeMat(defaultMat);
                else
                    ChangeMat(stunMat);

                isFlashed = false;
                flashTimer = 0;
            }
        }

        if(target)
        {
            if (!target.activeSelf)
                FindTarget();
        }

        rB.MovePosition(transform.position + velocity * timeDilation * Time.deltaTime);

        if (gameState == EnemyState.NORMAL)
            Rotate();
        

    }

    protected virtual void Normal() { }

    protected virtual void Warp() { }

    protected virtual void EndWarp()
    {
        warpImage.SetActive(false);

        transform.position = new Vector3(
            transform.position.x, restHeight, transform.position.z);

        //rB.constraints = defaultConstraints;

        gameState = EnemyState.STUNNED;
        stunTime = 0.5f;

        gameObject.tag = "Enemy";

        //if (cameraInfluence != 0)
        //    Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(
        //        transform, cameraInfluence, cameraInfluence, 1f);
    }


    protected virtual void Stunned()
    {
        stunTime -= Time.deltaTime;
        velocity = Vector3.zero;

        if (stunTime <= 0)
        {
            gameState = EnemyState.NORMAL;
            ChangeMat(defaultMat);
        }
    }

    protected virtual void Rotate()
    {
        rB.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed));
    }

    protected virtual void FindTarget()
    {
        target = PlayerManager.Instance.GetPlayer(0);

        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //if (players.Length > 0)
        //{
        //    int rand = Random.Range(0, players.Length);
        //    target = players[rand];
        //}
    }

    protected virtual void BeatEvent()
    {
        if (gameState == EnemyState.NORMAL)
        {
            OnBeat();

            //Fail Safe
            if (transform.position.y != restHeight && heightLock)
                transform.position = new Vector3(transform.position.x, restHeight, transform.position.z);
        }
    }

    protected virtual void OnBeat() { }

    #endregion

    #region Utility Methods

    protected float TargetDistance()
    {
        if (target)
            return Vector3.Magnitude(target.transform.position - transform.position);
        else
            return 0;
    }

    protected void ChaseTarget(float speed)
    {
        velocity = Vector3.Normalize(target.transform.position - transform.position) * speed;
    }

    protected void Oribit(Transform oribitTar, float distance, float speed, bool clockWise)
    {
        Vector3 orbit = oribitTar.position - transform.position;

        if (clockWise)
            orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
                                      orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));
        else
            orbit = new Vector3(orbit.x * Mathf.Cos(-90) - orbit.z * Mathf.Sin(-90), 0,
                          orbit.x * Mathf.Sin(-90) + orbit.z * Mathf.Cos(-90));

        orbit = Vector3.Normalize(orbit) * distance;
        orbit = oribitTar.position + orbit;

        velocity = Vector3.Normalize(orbit - transform.position) * speed;
    }

    protected void OribitTarget(float distance, float speed, bool clockWise)
    {
        Vector3 orbit = target.transform.position - transform.position;

        if (clockWise)
            orbit = new Vector3(orbit.x * Mathf.Cos(90) - orbit.z * Mathf.Sin(90), 0,
                                      orbit.x * Mathf.Sin(90) + orbit.z * Mathf.Cos(90));
        else
            orbit = new Vector3(orbit.x * Mathf.Cos(-90) - orbit.z * Mathf.Sin(-90), 0,
                          orbit.x * Mathf.Sin(-90) + orbit.z * Mathf.Cos(-90));

        orbit = Vector3.Normalize(orbit) * distance;
        orbit = target.transform.position + orbit;

        velocity = Vector3.Normalize(orbit - transform.position) * speed;
    }

    protected void FaceTarget(Vector3 offset, float rotationSpeed)
    {
        transform.rotation = Quaternion.LookRotation(
            Vector3.RotateTowards(transform.forward,
            Quaternion.Euler(offset) * (target.transform.position - transform.position),
            rotationSpeed * Time.deltaTime, 0.0F));
    }

    #endregion

    #region Collider Methods

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            TakeDamage(200, col.transform.position);

        }

        if (col.gameObject.tag == "Laser")
        {
            TakeDamage(col.GetComponent<BaseAttack>().GetDamage(),
                col.transform.position);
        }

        if (col.gameObject.tag == "Stun Laser")
        {
            TakeDamage(col.GetComponent<BaseAttack>().GetDamage(),
                col.transform.position);

            if (gameState == EnemyState.NORMAL && !stunImmune)
            {
                gameState = EnemyState.STUNNED;
                stunTime = col.GetComponent<BaseAttack>().GetStunTime();

                if (flashMat)
                    ChangeMat(stunMat);
            }
        }

        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 0.2f;
            rB.drag = chronoDrag;
        }
    }

    protected virtual void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Laser Beam")
        {
            dotDamage += col.GetComponent<BaseAttack>().GetDamage() * Time.deltaTime;

            if (dotDamage > 100)
            {
                TakeDamage(dotDamage, transform.position - new Vector3(0, 50, 0));
                dotDamage = 0;
            }
            //TakeDamage(col.GetComponent<BaseAttack>().GetDamage() * Time.deltaTime, col.transform.position);
        }
    }

    protected virtual void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Chrono")
        {
            timeDilation = 1;
            rB.drag = defaultDrag;
        }
    }

    #endregion

    protected virtual void DotDamage()
    {
        if (dotDamage > 10)
        {
           //Debug.Log("DOT TEST");

            TakeDamage(dotDamage, transform.position - new Vector3(0, 100, 0));
            dotDamage = 0;
        }
    }

    public virtual void TakeDamage(float damage, Vector3 pos)
    {
        float damageRange = 0.1f;
        damage *= Random.Range(1 - damageRange, 1 + damageRange) * 1.5f;

        health -= damage;
        
        if(damage > 1)
            TextManager.Instance.DamageText((int)damage, pos, transform.GetComponent<Collider>().bounds);


        if (flashMat)
        {
            ChangeMat(flashMat);
            isFlashed = true;
        }
    }

    protected virtual void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;
    }

    protected virtual void LevelEnd()
    {
        gameState = EnemyState.STUNNED;
        stunTime = 10000 * 10000;

        //Explode(false);
    }

    public virtual void Explode()
    {    
        Explode(true);
    }

    public virtual void Explode(bool reward) 
    {        
        //Camera.main.GetComponent<ProCamera2D>().RemoveCameraTarget(transform, 0);

        //ProCamera2DShake.Instance.Shake(
        //    0.2f, //duration
        //    new Vector3(10, 10) * value, //strength
        //    10, //vibrato
        //    1, //randomness
        //    -1, //initialAngle (-1 = random)
        //    new Vector3(0, 0, 0), //rotation
        //    0.1f); //smoothness

        explosion.transform.position = transform.position;
        explosion.transform.rotation = transform.rotation;
        explosion.gameObject.SetActive(true);
        
        if(reward)
        {
            if (value > 0)
            {
                ScoreManager.Instance.AddEnemyKill(value, transform.GetComponent<Collider>().bounds);
                ObjectPool.Instance.ExpExplosion(value, transform.position);
            }
        }

        
        this.gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
        BaseLevel.OnLevelEnd -= LevelEnd;
    }

}
