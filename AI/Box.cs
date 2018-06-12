using UnityEngine;
using System.Collections;

public class Box : BaseEnemy
{

    public GameObject Top;
    public GameObject Bot;
    public GameObject[] Bars;

    float boxSize;
    const float maxSize = 20;

    public int xIndex;
    public int yIndex;

    protected override void OnEnable()
    {
        base.OnEnable();

        boxSize = 0;

        Top.transform.localPosition = new Vector3(0, 5, 0);
        Bot.transform.localPosition = new Vector3(0, -5, 0);

        for (int i = 0; i < 4; i++)
        {
            Bars[i].transform.localScale = new Vector3(5, 6, 5);
        }

        gameState = EnemyState.NORMAL;
    }

    override protected void Normal()
    {
        if (boxSize < maxSize)
            boxSize += Time.deltaTime * 100;
        else
            boxSize = maxSize;


        Top.transform.localPosition = new Vector3(0, boxSize + 5, 0);
        Bot.transform.localPosition = new Vector3(0, -boxSize - 5, 0);

        for(int i = 0; i < 4; i++)
        {
            Bars[i].transform.localScale = new Vector3(5, boxSize * 2 + 6, 5);
        }        
    }

    protected override void ChangeMat(Material mat)
    {
        Top.GetComponent<Renderer>().material = mat;
        Bot.GetComponent<Renderer>().material = mat;
    }

    public override void TakeDamage(float damage, Vector3 pos)
    {
        float damageRange = 0.1f;
        damage *= Random.Range(1 - damageRange, 1 + damageRange);

        health -= damage;

        //if (damage > 1)
        //    TextManager.Instance.DamageText((int)damage, pos, transform.GetComponent<Collider>().bounds);


        if (flashMat)
        {
            ChangeMat(flashMat);
            isFlashed = true;
        }
    }

    public override void Explode(bool reward)
    {
        BoxManager.Instance.OnBoxEnd(xIndex, yIndex);

        base.Explode(false);
        //this.gameObject.SetActive(false);
    }
}
