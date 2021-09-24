using UnityEngine;

public class DestroyBalls : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0, 1.176533f, -0.9f);
   
    private void OnTriggerEnter(Collider other)
    {
        //Don't destroy white ball but respawn it in the start position.
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.transform.position = startPosition;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        //Destroy the ball if it hits the hole.
        else
        {
            Destroy(other.gameObject);
        }
    }    
}
