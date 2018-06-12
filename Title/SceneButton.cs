using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ItsHarshdeep.LoadingScene.Controller;

public class SceneButton : MonoBehaviour
{
	void Start ()
    {
        GetComponent<Button>().onClick.AddListener(() => { LoadScene(gameObject.name); });
    }

    void LoadScene(string name)
    {
        //SceneManager.LoadScene(name);

        SceneController.LoadLevel(name, 0);
    }


    //public void LoadScene(string sceneName)
    //{
    //    if (requiredCustomDelay)
    //        SceneController.LoadLevel(sceneName, 1.5f);
    //    else
    //        SceneController.LoadLevel(sceneName);

    //    print("Previous Scene name was : " + SceneController.previousScene);
    //}
}
