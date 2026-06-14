using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem explosionParticles;
    AudioSource audioSource;
    bool isControllable = true;
    bool isCollided = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextScene();
        }else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollided = !isCollided;
        }
    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(!isControllable || !isCollided) return;
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
        successParticles.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke("LoadNextScene", 2f);
    }

    private void CrashSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        explosionParticles.Play();
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
