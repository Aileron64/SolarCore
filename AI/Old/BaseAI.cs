using UnityEngine;
using System.Collections;

public enum GameState
{
    NORMAL, WARP, STUNNED, REDSUN
}

public class BaseAI : MonoBehaviour
{
    bool expCubes = false;


    protected GameObject target;

    public Material flashMat;
    public Material stunMat;

    public Rigidbody exp;
    public ParticleSystem explosion;

    protected Material defaultMat;

    float flashTime = 0.05f;
    float flashTimer;
    protected bool isFlashed = false;

    protected Vector3 velocity;
    protected Vector3 rotationVelocity;
    protected Vector3 rotation;
    protected float rotateSpeed;

    protected float health = 100;
    protected float maxHealth = 100;

    protected float impactDamage = 10;
    protected int scoreValue = 100;

    protected float expAmount = 5;
    protected float expSpeed = 1000;

    protected float restHeight = 0;

    protected bool rotate = true;
    protected bool stunImmune = false;
    protected bool timeImmune = false;

    Vector3 forceImpact;
    Vector3 solarCoreLoc;

    bool gameOver = false;

    protected float stunTime;

    float warpSpeed = 3000;

    float timeDilation = 1;

    public GameState gameState = GameState.NORMAL;

    public float GetImpact()
    {
        return impactDamage;
    }

    protected void Awake()
    {
        //rotation = transform.localRotation.eulerAngles;
        defaultMat = GetComponent<Renderer>().material;
        TargetPlayer();

        solarCoreLoc = new Vector3(1100, -200, 1000);
    }

    protected virtual void Update()
    {
        if (health <= 0)
        {
            GivePoints();
            Explode();
        }

        if (isFlashed)
        {
            flashTimer += Time.deltaTime;

            if (flashTimer >= flashTime)
            {
                if (gameState != GameState.STUNNED)
                    ChangeMat(defaultMat);
                else
                    ChangeMat(stunMat);

                isFlashed = false;
                flashTimer = 0;
            }
        }


        switch (gameState)
        {
            default:
            case GameState.NORMAL:
                Normal();
                break;

            case GameState.WARP:
                Warp();
                break;

            case GameState.REDSUN:
                RedSun();
                break;

            case GameState.STUNNED:
                Stunned();
                break;
        }

        forceImpact.x *= 0.95f;
        forceImpact.y = 0;
        forceImpact.z *= 0.95f;

        if (gameState != GameState.STUNNED)
        {
            rotation.y += rotateSpeed * Time.deltaTime * timeDilation;
            rotation += rotationVelocity * Time.deltaTime * timeDilation;
            transform.position += velocity * Time.deltaTime * timeDilation + forceImpact;
            if(rotate)
                transform.rotation = Quaternion.Euler(rotation);
        
            if (timeDilation < 1)
                timeDilation += Time.deltaTime;
        }
        else
        {
            transform.position += forceImpact;
        }

        if(target)
        {
            if (!target.activeSelf && !gameOver)
                TargetPlayer();
        }
    }

    protected virtual void Normal()
    { }

    protected virtual void Warp()
    {
        velocity = new Vector3(0, warpSpeed, 0);

        if (transform.position.y >= restHeight)
        {
            gameState = GameState.NORMAL;
            velocity.y = 0;
            transform.position = new Vector3(transform.position.x, restHeight, transform.position.z);
            ChangeMat(defaultMat);
        }
    }

    protected virtual void RedSun()
    {
        float distance = Vector3.Magnitude(solarCoreLoc - transform.position);

        if (Vector3.Magnitude(solarCoreLoc - transform.position) <= 20)
        {
            Destroy(this.gameObject);
            //Explode();
        }    
        else
        {
            velocity = Vector3.Normalize(solarCoreLoc - transform.position) * 250;
        }
    }

    protected virtual void Stunned()
    {
        stunTime -= Time.deltaTime;

        if (stunTime <= 0)
        {
            gameState = GameState.NORMAL;
            ChangeMat(defaultMat);
        }
    }

    protected virtual void TargetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            int rand = Random.Range(0, players.Length);
            target = players[rand];
        }
        else
            gameOver = true;
    }

    protected float TargetDistance()
    {
        return Vector3.Magnitude(target.transform.position - transform.position);
    }

    protected void ChaseTarget(float speed)
    {
        velocity = Vector3.Normalize(target.transform.position - transform.position) * speed;
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

    public virtual void HailRedSun()
    {
        rotationVelocity = new Vector3(
            Random.Range(90, 180), Random.Range(90, 180), Random.Range(90, 180));

        gameObject.tag = "Untagged";
        gameState = GameState.REDSUN;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            health -= 200;

            if (flashMat)
            {
                ChangeMat(flashMat);
                isFlashed = true;
            }
        }

        if (col.gameObject.tag == "Laser")
        {
            health -= col.GetComponent<BaseAttack>().GetDamage();

            if (flashMat)
            {
                ChangeMat(flashMat);
                isFlashed = true;
            }
        }

        if (col.gameObject.tag == "Stun Laser")
        {
            health -= col.GetComponent<BaseAttack>().GetDamage();

            if (gameState == GameState.NORMAL && !stunImmune)
            {
                gameState = GameState.STUNNED;
                stunTime = col.GetComponent<BaseAttack>().GetStunTime();
            }

            if (flashMat)
                ChangeMat(stunMat);

        }
    }

    protected virtual void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Chrono" && !timeImmune)
        {
            timeDilation = 0.2f;
        }

        if (col.gameObject.tag == "Laser Beam")
        {
            health -= col.GetComponent<BaseAttack>().GetDamage() * Time.deltaTime;

            if (flashMat)
            {
                ChangeMat(flashMat);
                isFlashed = true;
            }
        }

        if (col.gameObject.tag == "Force Push")
        {
            float pushForce = 50;
            Vector3 pushDirection = Vector3.Normalize(col.transform.position - transform.position);
            forceImpact += pushDirection * pushForce * -1 * Time.deltaTime;

            health -= col.GetComponent<BaseAttack>().GetDamage() * Time.deltaTime;
        }
    }

    protected virtual void ChangeMat(Material mat)
    {
        GetComponent<Renderer>().material = mat;
    }

    protected virtual void GivePoints()
    {
        if (expCubes)
        {
            for (int i = 0; i <= expAmount; i++)
            {
                Rigidbody clone = Instantiate(exp, transform.position,
                    Quaternion.Euler(new Vector3(
                        Random.Range(-100, 100),
                        Random.Range(-100, 100),
                        Random.Range(-100, 100)))) as Rigidbody;

                clone.AddForce(new Vector3(
                    Random.Range(-expSpeed, expSpeed),
                    Random.Range(-10, 10),
                    Random.Range(-expSpeed, expSpeed)));
            }
        }
        else
        {
            //if (target != null)
            //    target.GetComponent<Player>().UpdateExp(scoreValue * 0.8f);
        }

    }

    protected virtual void Explode() 
    {
        Instantiate(explosion, transform.position, transform.localRotation);

     
        Destroy(this.gameObject);
    }
}
