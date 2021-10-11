using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    bool collisionDisabled = false;
    bool isTransitioning = false;

    void OnCollisionEnter(Collision collision) {

        if (isTransitioning || collisionDisabled) { return; }
        
        switch (collision.gameObject.tag) {
            case "Friendly":

                break;
            case "Finish":

                Vector3 impulse = collision.impulse;
                Debug.Log(impulse);

                if (impulse.y > 1f) {
                    StartCrashSequence();
                    Debug.Log("You were moving too fast and crashed!");
                } else {
                    StartLevelLoadSequence();
                }
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
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadLevel", 1f);
    }

    void StartLevelLoadSequence() {
        isTransitioning = true;
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
        isTransitioning = false;
    }

    void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        isTransitioning = false;
    }
}
