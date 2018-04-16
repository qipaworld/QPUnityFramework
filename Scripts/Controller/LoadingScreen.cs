using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour {

    
    public Image loading;
    //public 
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = null;
        //float progress = 0;

        if (DataManager.instance == null)
        {
            asyncOperation = SceneManager.LoadSceneAsync("StartScene");
        }
        else {
            asyncOperation = SceneManager.LoadSceneAsync(DataManager.Instance.getData("GameStatus").GetStringValue("GameScreenName"));
        }
        // u3d 5.3之后使用using UnityEngine.SceneManagement;加载场景
        // 不允许加载完毕自动切换场景，因为有时候加载太快了就看不到加载进度条UI效果了
        //asyncOperation.allowSceneActivation = false;
        // mAsyncOperation.progress测试只有0和0.9(其实只有固定的0.89...)
        // 所以大概大于0.8就当是加载完成了
        //while (!asyncOperation.isDone && asyncOperation.progress < 0.8f)
        //{
        //yield return asyncOperation;
        //}
        while (!asyncOperation.isDone)
        {
            //SetLoadingPercentage(op.progress * 100);
            //if (asyncOperation.progress > 0.95 && progress <= asyncOperation.progress)
            //{
            //    progress += 0.01f;
            //}
            //else if (progress <= asyncOperation.progress)
            //{
            //    progress += 0.001f;
            //}
            loading.fillAmount = asyncOperation.progress;
            yield return new WaitForEndOfFrame();
        }


        //while (asyncOperation.progress < 0.9f)
        //{

        //    while (progress < asyncOperation.progress)
        //    {
        //        progress += 0.01f;
        //        loading.fillAmount = progress;
        //        yield return new WaitForEndOfFrame();
        //    }
        //}
        //asyncOperation.allowSceneActivation = true;

        //while (progress < 1)
        //{
        //    progress += 0.01f;
        //    loading.fillAmount = progress;
        //    yield return new WaitForEndOfFrame();
        //}
    }

    void Update()
    {
       

        //loading.text = string.Format("{0}%", (int)(asyncOperation.progress * 100));
        // 必须等进度条跑到100%才允许切换到下一场景
        //if (asyncOperation.progress >= 0.85) asyncOperation.allowSceneActivation = true;
    }
}
