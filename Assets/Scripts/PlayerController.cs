using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float hitStrength = 100; 

    private LineRenderer lr;
    [SerializeField] Transform aim;
    private RaycastHit hit;
    [SerializeField] Slider hitStrengthIndicator;

    [SerializeField] bool areMoving = false;

    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!areMoving)
        {
            float distFromCamToBall = Mathf.Abs(transform.position.y - Camera.main.transform.position.y);
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distFromCamToBall);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            distance = Vector3.Distance(mousePos, transform.position);
            distance = Mathf.Clamp(distance, 0.1f, 1.0f);
            hitStrengthIndicator.value = distance;

            if (Input.GetMouseButtonDown(0) && !areMoving)
            {
                HitBall(distance);
            }

            if (!areMoving)
                RotateBall();
        }
    }

    private void FixedUpdate()
    {
         ShowTrajectory();
    }

    private void LateUpdate()
    {
        CheckMoving();
    }

    void ShowTrajectory()
    {
        if (Physics.Raycast(transform.position, transform.right, out hit, 3) && !areMoving)
        {
            lr.positionCount = 2;
            aim.position = hit.point;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, aim.position);
        }
        else
        {
            lr.positionCount = 0;
        }
    }

    void HitBall(float strengthMultiplier)
    {
        aim.gameObject.SetActive(false);
        hitStrengthIndicator.gameObject.SetActive(false);
        rb.AddForce(transform.right * Time.deltaTime * hitStrength * strengthMultiplier * 10, ForceMode.Impulse);
    }

    void RotateBall()
    {
        Vector3 ballVector = Camera.main.WorldToScreenPoint(transform.position);
        ballVector = Input.mousePosition - ballVector;
        float angle = Mathf.Atan2(ballVector.y, ballVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.up);
    }

    void CheckMoving()
    {
        areMoving = false;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            if (ballRb == null)
                continue;

            if (ballRb.velocity.magnitude > 0.1f)
                areMoving = true;            
        }
        if (areMoving)
        {
            aim.gameObject.SetActive(false);
            hitStrengthIndicator.gameObject.SetActive(false);
        }
        else
        {
            aim.gameObject.SetActive(true);
            hitStrengthIndicator.gameObject.SetActive(true);
        }
    }
}
