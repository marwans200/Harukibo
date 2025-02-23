using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Rigidbody rb;
    public Camera _camera;

    private void Start()
    {
        if (!Object.HasInputAuthority) _camera.gameObject.SetActive(false);
    }
    

    public override void FixedUpdateNetwork()
    {
        if (Object.HasInputAuthority)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 force = new Vector3(h * speed, 0, v * speed);
            rb.AddRelativeForce(force);
        }
    }
}
