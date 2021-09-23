using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject whiteBall;
    private LineRenderer lr;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        lr = whiteBall.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        ShowTrajectory();
    }

    void ShowTrajectory()
    {
        if (Physics.Raycast(whiteBall.transform.position, whiteBall.transform.right, out hit, 2))
        {
            lr.SetPosition(0, whiteBall.transform.position);
            lr.SetPosition(1, hit.transform.position);
        }
    }
}
