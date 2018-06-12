using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyExplosion : MonoBehaviour
{
    public ParticleSystem explosion;

    float lifeTime = 1f;

    public GameObject ghost;
    Color ghostColor;

    public float meshSize = 4;

    void Awake()
    {
        ghostColor = ghost.GetComponent<Renderer>().material.color;
    }

    public void Setup(Mesh mesh, float size)
    {      
        if(mesh)
            ghost.GetComponent<MeshFilter>().mesh = mesh;
        else
            ghost.SetActive(false);


        explosion.maxParticles = (int)(10 + (size * 2));
        explosion.startSize = size * 80;
    }

    void OnEnable()
    {      
        Invoke("Disable", lifeTime);

        explosion.Play();

        ghost.transform.localScale = new Vector3(meshSize, meshSize, meshSize);

        ghost.transform.DOScale(meshSize * 1.25f, lifeTime * 0.5f);

        //iTween.ScaleTo(ghost, iTween.Hash("scale", new Vector3(meshSize * 1.25f, meshSize, meshSize * 1.25f),
        //    "time", lifeTime * 0.5f, "easeType", easeType));

        ghost.GetComponent<Renderer>().material.color = ghostColor;



        ghost.GetComponent<Renderer>().material.DOFade(0, lifeTime * 1.3f);

        //iTween.ColorTo(ghost, iTween.Hash("a", 0, //"namedcolorvalue", "_TintColor",
        //    "time", lifeTime));

    }


    void Disable()
    {
        //iTween.Stop(ghost);
        this.gameObject.SetActive(false);
    }
}

//useOldExplosion = true;
//defaultExplosionPrefab = (ParticleSystem)Resources.Load("Prefabs/Old_Ship_Explosion", typeof(ParticleSystem));

//if (defaultExplosionPrefab)
//{
//    defaultExplosion = Instantiate(defaultExplosionPrefab) as ParticleSystem;
//    defaultExplosion.gameObject.SetActive(false);
//    defaultExplosion.maxParticles = (int)(10 + (size * 2));
//    defaultExplosion.startSize = size * 80;

//    //defaultExplosion.main.maxParticles = size * 80;
//}
//else
//    Debug.Log("Default Explosion Missing");