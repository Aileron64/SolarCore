using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;

public class Bomb : BaseAttack 
{
    public int ownerNum = 1;
    public GameObject bombExplosion;

    float pushForce = 5000;

    float pushTimer;
    float pushTime = 0.5f;

    public float novaImune;

    public GameObject nova_prefab;
    GameObject nova;

    public GameObject orb;
    public GameObject[] part;

    GameObject owner;
    public void SetOwner(GameObject _owner) { owner = _owner; }

    protected override void Awake()
    {
        base.Awake();

        nova = Instantiate(nova_prefab);
        nova.gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        pushTimer = 0.5f;
        BaseLevel.OnBeat += OnBeat;
        base.OnEnable();
    }

    protected override void Update()
    {
        pushTimer += Time.deltaTime;
        novaImune -= Time.deltaTime;
        base.Update();
    }

    void OnBeat()
    {
        orb.transform.DOScale(45f, 0.2f).From();

        for (int i = 0; i < 4; i++)
        {
            part[i].transform.DOLocalMove(part[i].transform.localPosition.normalized * 10, 0.2f).From();
        }
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Block")
        {
            OnEnd();
        }

        if (col.gameObject.tag == "Laser")
        {
            if(col.GetComponent<BaseAttack>().atkTag == "G_Laser" && pushTimer >= pushTime)
            {
                Vector3 pushDirection = Vector3.Normalize(col.GetComponent<BaseAttack>().GetRigidBody().velocity);
                rB.AddForce(pushDirection * pushForce * 200);
                pushTimer = 0;
            }
            else if(col.GetComponent<BaseAttack>().atkTag == "G_Nova" && novaImune <= 0)
            {              
                //nova.transform.position = transform.position;
                //nova.SetActive(true);

                OnEnd();
            }
        }
    }

    void OnDisable()
    {
        BaseLevel.OnBeat -= OnBeat;
    }

    protected override void OnEnd()
    {
        Instantiate(bombExplosion, transform.position, Quaternion.identity);

        ProCamera2DShake.Instance.Shake(
            0.3f, //duration
            new Vector3(55, 55), //strength
            2, //vibrato
            0.5f, //randomness
            -1, //initialAngle (-1 = random)
            new Vector3(0, 0, 0), //rotation
            0.1f); //smoothness

        gameObject.SetActive(false);
    }
}
