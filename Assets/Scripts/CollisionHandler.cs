using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
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

    void StartCrashSequence() {
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void StartLevelLoadSequence() {
        GetComponent<Movement>().enabled = false;
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
