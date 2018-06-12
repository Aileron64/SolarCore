using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CrossBomber : BaseEnemy
{
    public GameObject crossBomb;

    public GameObject[] arm = new GameObject[4];
    Vector3[] blastDirection = new Vector3[4];

    bool flag;

    int distanceMod;

    public override void OnSpawn()
    {
        distanceMod = 6;

        for (int i = 0; i <= 3; i++)
        {
            arm[i].gameObject.SetActive(true);
        }

        transform.rotation = Quaternion.identity;

        base.OnSpawn();
    }

    protected override void Normal()
    {
        transform.Rotate(new Vector3(0, 25, 0) * Time.deltaTime * timeDilation);
    }

    protected override void OnBeat()
    {
        if (!arm[0].activeSelf && !arm[1].activeSelf &&
            !arm[2].activeSelf && !arm[3].activeSelf)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject clone = Instantiate(crossBomb, 
                    new Vector3(Mathf.Sin(i * 3.14f * 0.5f + 3.14f / 4) * 150, -25, Mathf.Cos(i * 3.14f * 0.5f + 3.14f / 4) * 150)
                    + transform.position, Quaternion.identity) as GameObject;

                clone = Instantiate(crossBomb,
                    new Vector3(Mathf.Sin(i * 3.14f * 0.5f) * 300, -25, Mathf.Cos(i * 3.14f * 0.5f) * 300)
                    + transform.position, Quaternion.identity) as GameObject;
            }

            Invoke("Explode", BaseLevel.Instance.GetBeatTime() * 0.8f);
        }
        else
        {
            if (distanceMod <= 5)
                distanceMod++;
            else
            {
                distanceMod = 1;

                for (int i = 0; i <= 3; i++)
                {
                    if (arm[i].activeSelf)
                    {                       
                        blastDirection[i] = (arm[i].transform.position - transform.position).normalized;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (arm[i].activeSelf)
                {
                    Vector3 bombPos = transform.position + (blastDirection[i] * (250 * distanceMod + 100));
                    bombPos.y = -25;                   
                    GameObject clone = Instantiate(crossBomb, bombPos, Quaternion.identity) as GameObject;

                    if (flag)
                        arm[i].transform.DOLocalMove(arm[i].transform.localPosition.normalized * 35, 0.1f).SetEase(Ease.OutSine);
                    else
                        arm[i].transform.DOLocalMove(arm[i].transform.localPosition.normalized * 37, 0.1f).SetEase(Ease.OutSine);  
                }
            }

            flag = !flag;
        }
    }

    public override void Explode()
    {
        for (int i = 0; i <= 3; i++)
        {
            if (arm[i].activeSelf)
            {
                arm[i].GetComponent<BaseEnemy>().Explode();
            }
        }
        base.Explode();
    }

}
