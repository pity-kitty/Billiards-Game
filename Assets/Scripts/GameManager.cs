using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();
        if (GameObject.FindGameObjectsWithTag("Ball").Length < 1)
            StartCoroutine(RestartWhenNoBalls());
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator RestartWhenNoBalls()
    {
        yield return new WaitForSeconds(1);
        RestartLevel();
    }
}
