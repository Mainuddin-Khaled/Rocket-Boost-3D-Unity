using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip crashSound;
    AudioSource audioSource;
    bool isControllable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!isControllable) return;
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
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextScene", 2f);
    }

    private void CrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
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
