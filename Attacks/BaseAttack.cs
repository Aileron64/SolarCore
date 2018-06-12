using UnityEngine;
using System.Collections;

public class BaseAttack : MonoBehaviour 
{
    public enum Team
        { BLUE, RED }
    public Team side;

    public string atkTag;
    protected Vector3 velocity;
    protected Rigidbody rB;
    protected float timeDilation = 1;

    // Stats
    public float damage;
    protected float stunTime;
    protected float force;
    protected float power;

    public float lifeTime;
    protected float lifeTimer;

    public bool lockedInPlace;

    protected virtual void Awake()
    {   
        rB = GetComponent<Rigidbody>();
	}

    protected virtual void Start() { }
 
    public float GetDamage() { return damage; }
    public float GetStunTime() { return stunTime; }
    public float GetForce() { return force; }
    public Rigidbody GetRigidBody() { return rB; }
    public Vector3 GetVelocity() { return velocity; }

    public virtual void SetPower(float _power)
    {
        power = _power;
        //damage *= 0.5f + (0.1f * power);
    }

    public void SetVelocity(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    protected virtual void OnEnable()
    {
        lifeTimer = 0;
        rB.velocity = Vector3.zero;
    }

    protected virtual void FixedUpdate()
    {
        if(!lockedInPlace)
            rB.MovePosition(transform.position + velocity * timeDilation * Time.deltaTime);
    }

	protected virtual void Update () 
    {
        lifeTimer += Time.deltaTime;

        if (lifeTimer >= lifeTime && lifeTime != 0)
        {
            lifeTimer = 0;
            OnEnd();     
        }
	}

    protected virtual void OnEnd()
    {
        this.gameObject.SetActive(false);
    }
}
