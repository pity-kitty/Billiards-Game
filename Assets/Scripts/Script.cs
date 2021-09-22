using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lr;
    [SerializeField] float hitStrength = 100;
    [SerializeField] Transform target;
    Vector3 heading;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        heading = (target.position - transform.position).normalized;
        heading.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, target.position);

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
