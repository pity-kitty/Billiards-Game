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

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (!areMoving)
        {            
            CalculateDistance_Editor();

            hitStrengthIndicator.value = distance;

            if (Input.GetMouseButtonUp(0))
            {
                HitBall(distance);
            }

            RotateBall();
        }
    }

    //Method to calculate the distance between mouse/finger position to apply it as strangth multiplier.
    //Distance from camera to ball is needed to apply the position exactly on the same Y position.
    //Then we transform mouse screen position to world point and clamp it to be between 0.1 and 1.
    void CalculateDistance_Editor()
    {
        float distFromCamToBall = Mathf.Abs(transform.position.y - Camera.main.transform.position.y);
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distFromCamToBall);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        distance = Vector3.Distance(mousePos, transform.position);
        distance = Mathf.Clamp(distance, 0.1f, 1.0f);
    }

    //This method do exatly the same as CalculateDistance_Editor, but its purpose to work in android build.
    //But I ralize that editor method works fine on devise, so...
    float CalculateDistance_Android()
    {        
        Touch touch = Input.GetTouch(0);
        float distFromCamToBall = Mathf.Abs(transform.position.y - Camera.main.transform.position.y);
        Vector3 fingerPos = new Vector3(touch.position.x, touch.position.y, distFromCamToBall);
        fingerPos = Camera.main.ScreenToWorldPoint(fingerPos);
        float dist = Vector3.Distance(fingerPos, transform.position);
        dist = Mathf.Clamp(distance, 0.1f, 1.0f);
        return dist;
    }

    private void FixedUpdate()
    {
        if (!areMoving)
        {
            ShowTrajectory();
        }
        else
        {
            lr.positionCount = 0;
        }
    }

    private void LateUpdate()
    {
        CheckMoving();
    }

    //This method shows the trajectroy of a hit and the trajectory of rebound of hited ball.
    //We're casting the ray to find the border or a ball to place there the aim sphere and get an end of trajectory.
    void ShowTrajectory()
    {
        if (Physics.Raycast(transform.position, transform.right, out hit, 3))
        {
            lr.positionCount = 2;
            aim.position = hit.point;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, aim.position);
            if (hit.collider.gameObject.CompareTag("Ball"))
            {
                lr.positionCount += 1;
                Ray ray = new Ray();
                ray.origin = hit.collider.gameObject.transform.position;
                ray.direction = hit.collider.gameObject.transform.position - hit.point;
                Vector3 reboundTrajectory = ray.GetPoint(0.5f);
                lr.SetPosition(2, reboundTrajectory);
            }
        }
    }

    //Apply a force to the ball in the forward direction of the scene and ball's local right direction. 
    void HitBall(float strengthMultiplier)
    {
        aim.gameObject.SetActive(false);
        hitStrengthIndicator.gameObject.SetActive(false);
        rb.AddForce(transform.right * Time.deltaTime * hitStrength * strengthMultiplier * 10, ForceMode.Impulse);
    }

    //Rotate the ball with mouse/finger.
    void RotateBall()
    {
        Vector3 ballVector = Camera.main.WorldToScreenPoint(transform.position);
        ballVector = Input.mousePosition - ballVector;
        float angle = Mathf.Atan2(ballVector.y, ballVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.up);
    }

    //This method checks if at least one ball is moving by checking its velocity (speed).
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
