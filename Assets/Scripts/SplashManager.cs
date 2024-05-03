using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [SerializeField] string assetUrl;
    [SerializeField] Slider loadingBar;
    [SerializeField] Text loadingPercentage;
    void Start()
    {
        StartCoroutine(DownloadAssetBundleFromServer());
    }

  
    private IEnumerator DownloadAssetBundleFromServer()
    {
        while (!Caching.ready)
            yield return null;

        Debug.Log("Downloading asset bundle from path " + assetUrl);
        using (var www = WWW.LoadFromCacheOrDownload(assetUrl, 2))
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log("Error downloading asset bundle: " + www.error);
                yield break; // Exit the coroutine on error
            }


            while (!www.isDone)
            {
                float downloadProgress = www.progress * 100;
                loadingPercentage.text = downloadProgress.ToString("0.00") + "%";
                loadingBar.value = downloadProgress;
                yield return null;
            }

            loadingBar.value = 100f;
            loadingPercentage.text ="100%";
            var myLoadedAssetBundle = www.assetBundle;
            if (myLoadedAssetBundle.isStreamedSceneAssetBundle)
            {

                string[] scenePaths = myLoadedAssetBundle.GetAllScenePaths();
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);
                SceneManager.LoadScene(sceneName);
            }

        }

        Invoke("HideLoadingBar", .5f); // This function call is not defined, you may need to implement it
    }
}
