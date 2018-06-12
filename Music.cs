using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour 
{
    static Music instance;
    public static Music Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Music>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<Music>();
                }
            }
            return instance;
        }
    }


    //   public bool music;
    //   public static bool muteMusic;

    //   GameObject bossMusic;
    //   GameObject warningSound;

    //void Start () 
    //   {

    //       bossMusic = transform.FindChild("Boss Music").gameObject;
    //       warningSound = transform.FindChild("Warning").gameObject;


    //       if (Application.isEditor)
    //           muteMusic = !music;

    //       if (!muteMusic)
    //           GetComponent<AudioSource>().Play();

    //}

    //   public void PlaySiren()
    //   {
    //       GetComponent<AudioSource>().Stop();
    //       warningSound.GetComponent<AudioSource>().Play();
    //   }

    //   public void PlayBossMusic()
    //   {
    //       if (!muteMusic)
    //           bossMusic.GetComponent<AudioSource>().Play();
    //   }


}
