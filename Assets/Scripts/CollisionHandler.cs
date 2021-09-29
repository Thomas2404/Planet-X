using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool collisionDisabled = false;

    void OnCollisionEnter(Collision collision) {
        
        switch (collision.gameObject.tag) {
            case "Friendly":

                break;
            case "Finish":
                StartLevelLoadSequence();
                break;
            case "Fuel":

                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void Update() {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void StartCrashSequence() {
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel", 1f);
    }

    void StartLevelLoadSequence() {
        GetComponent<Movement>().enabled = false;
        successParticles.Play();
        Invoke("LoadNextLevel", 1f);
    }

    void LoadNextLevel() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
