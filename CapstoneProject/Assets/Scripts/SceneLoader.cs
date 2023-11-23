using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneNameToLoad)
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // 에디터 모드에서는 플레이 모드를 중지합니다.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 런타임에서는 어플리케이션을 종료합니다.
        Application.Quit();
#endif
    }
}
