using UnityEngine;

public class Script : MonoBehaviour
{
    private Rigidbody rb;
    private LineRenderer lr;
    private RaycastHit hit;
    [SerializeField] float hitStrength = 100;
    [SerializeField] Transform Target;
    private Vector3 rayPos = new Vector3(2, 1.171687f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 heading = transform.position - Cue.transform.position;
        //float distance = heading.magnitude;
        //Vector3 direction = heading / distance;

        //if (Physics.Raycast(transform.position, direction, out hit, 2))
        //{
        //    lr.SetPosition(0, transform.position);
        //    lr.SetPosition(1, hit.transform.position);
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.right * Time.deltaTime * hitStrength, ForceMode.Impulse);
        }


        if (Input.touchCount == 1)
        {

            //rb.drag = 0;
            //Touch touch = Input.GetTouch(0);
            //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            //Vector3 direction = (touchPosition - transform.position).normalized;
            //direction.y = 0;
            //rb.AddForce(direction * Time.deltaTime * hitStrength, ForceMode.Impulse);            
        }
        else if (Input.touchCount > 1)
        {
            //rb.drag = float.PositiveInfinity;
        }
    }
}
