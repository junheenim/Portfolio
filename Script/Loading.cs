using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    static string nextScene;
    [SerializeField]
    Image progressBar;
    AsyncOperation aop;
    static GameObject ScenemovePlayer;
    static Vector3 position;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    public static void LoadScene(string scneneName,GameObject player, Vector3 pos)
    {
        ScenemovePlayer = player;
        position = pos;
        nextScene = scneneName;
        SceneManager.LoadScene("Loading");
    }
    IEnumerator LoadScene()
    {
        yield return null;
        aop = SceneManager.LoadSceneAsync(nextScene);
        
        aop.allowSceneActivation = false;
        float time = 0f;
        while(!aop.isDone)
        {
            yield return null;
            time += Time.deltaTime;
            if (aop.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, aop.progress, time);
               if (progressBar.fillAmount >= aop.progress)
               {
                   time = 0f;
               }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, time);
                if (progressBar.fillAmount == 1.0f)
                {
                    aop.allowSceneActivation = true;
                    ScenemovePlayer.transform.position = position;
                    yield break;
                }
            }
        }
    }
}
