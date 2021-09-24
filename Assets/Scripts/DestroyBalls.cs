using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0, 1.176533f, -0.9f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.transform.position = startPosition;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
