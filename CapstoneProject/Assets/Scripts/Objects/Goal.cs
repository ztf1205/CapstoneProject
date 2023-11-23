using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
public class Goal : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private void Awake()
    {
        sceneLoader = GetComponent<SceneLoader>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName == "4")
            {
                sceneLoader.LoadScene("GameClear");
                EventManager.TriggerEvent("GameClear");
            }
            else
            {
                EventManager.TriggerEvent("LevelClear");
                int nextScene = int.Parse(currentSceneName) + 1;
                sceneLoader.LoadScene(nextScene.ToString());
            }
        }
    }
}
