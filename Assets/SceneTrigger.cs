using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public string NewScene = string.Empty;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.CompareTag("Player"))
            SceneLoad(NewScene);
    }

    public void SceneLoad()
    {
        Debug.Log(NewScene);
        SceneManager.LoadScene(NewScene);
    }

    public void SceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
