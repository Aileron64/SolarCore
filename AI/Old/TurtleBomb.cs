using UnityEngine;
using System.Collections;

public class TurtleBomb : BaseAI 
{
    float explosionTime = 5;
    float timer;

    float isFlashTime = 1f;
    float isFlashBackTime = 0.1f;
    float isFlashTimer;
    bool isFlash = false;

    public GameObject bombExplosion;

    void Start()
    {
        health = 1;
        impactDamage = 0;
        scoreValue = 0;
        rotation.y = 45;
        expAmount = 0;
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        isFlashTimer += Time.deltaTime;


        if (isFlash)
        {
            if (isFlashTimer >= isFlashBackTime)
            {
                ChangeMat(defaultMat);
                isFlashTimer = 0;
                isFlash = false;
            }
        }
        else
        {
            if (isFlashTimer >= isFlashTime)
            {
                ChangeMat(flashMat);
                isFlashTimer = 0;
                isFlashTime *= 0.6f;
                isFlash = true;
            }
        }


        if (timer >= explosionTime)
            Explode();

        base.Update();
    }

    void flash()
    {

    }

    protected override void Explode()
    {
        Instantiate(bombExplosion, transform.position, Quaternion.identity);
        //Instantiate(explosion, transform.position, transform.localRotation);
        Destroy(this.gameObject);
    }
}
