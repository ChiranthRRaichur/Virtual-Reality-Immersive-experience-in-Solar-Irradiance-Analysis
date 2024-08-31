using UnityEngine;

public class HumanController : MonoBehaviour
{
    public float speed = 8.0f;
    private Animator animator;
    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float strafe = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(strafe, 0.0f, move);
        movement = movement.normalized * speed;

        // Set the animation parameters
        animator.SetFloat("Speed", movement.magnitude);

        // Use Animator's root motion to move the character
        if (movement != Vector3.zero)
        {
            transform.forward = movement; // Ensure the character faces the direction of movement
        }

        Debug.Log("Movement vector: " + movement);
    }

    void OnAnimatorMove()
    {
        if (animator)
        {
            // Apply root motion while preserving the current Y position
            Vector3 newPosition = rb.position + animator.deltaPosition;
            newPosition.y = rb.position.y;
            rb.MovePosition(newPosition);
        }


        Debug.Log("Animator Delta Position: " + animator.deltaPosition);
    }
   

}
