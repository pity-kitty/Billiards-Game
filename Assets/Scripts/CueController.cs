using UnityEngine;

public class CueController : MonoBehaviour
{
    [SerializeField] Transform whiteBall;

    private Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        pivot = whiteBall.transform;
        transform.parent = pivot;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 ballVector = Camera.main.WorldToScreenPoint(whiteBall.position);
        ballVector = Input.mousePosition - ballVector;
        float angle = Mathf.Atan2(ballVector.y, ballVector.x) * Mathf.Rad2Deg;    
        pivot.position = whiteBall.position;
        pivot.rotation = Quaternion.AngleAxis(-angle + 180, Vector3.up);

    }
}
