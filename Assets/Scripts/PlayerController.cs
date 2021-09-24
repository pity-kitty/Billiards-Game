using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float hitStrength = 100; 

    private LineRenderer lr;
    [SerializeField] Transform aim;
    private RaycastHit hit;

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
        if (Input.GetKeyDown(KeyCode.Space) && !areMoving)
        {
            HitBall();
        }

        if(!areMoving)
            RotateBall();
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

    void HitBall()
    {
        distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
        distance = Mathf.Lerp(0.1f, 1, distance);
        rb.AddForce(transform.right * Time.deltaTime * hitStrength, ForceMode.Impulse);
        //lr.gameObject.SetActive(false);
        aim.gameObject.SetActive(false);
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
        if (!areMoving)
        {
            aim.gameObject.SetActive(true);
        }
    }
}
