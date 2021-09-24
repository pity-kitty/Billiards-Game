using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        //Restart the level on R key.
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        //Check if all balls are destroyed to forcibly restart the game. 
        if (GameObject.FindGameObjectsWithTag("Ball").Length < 2)
            StartCoroutine(RestartWhenNoBalls());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator RestartWhenNoBalls()
    {
        yield return new WaitForSeconds(1);
        RestartLevel();
    }
}
