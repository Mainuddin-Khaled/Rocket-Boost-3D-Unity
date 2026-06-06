using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Launch":
                Debug.Log("The ship just launched");
                break;
            case "Land":
                NextLevel();
                break;
            default:
                CrashSequence();
                break;
        }
    }

    private void NextLevel()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextScene", 2f);
    }

    private void CrashSequence()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("ReloadScene", 2f);
    }

    void LoadNextScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = currentScene + 1;
            if (nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene = 0;
            }
            SceneManager.LoadScene(nextScene);
        }

        void ReloadScene()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(sceneIndex);
        }
}
