using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;
    private Vector3 _velocity;

    private CharacterController _controller;
    public Camera _camera;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (!Object.HasInputAuthority) _camera.gameObject.SetActive(false);
    }
    

    public override void FixedUpdateNetwork()
    {
        if (Object.HasInputAuthority)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(h, 0, v).normalized * moveSpeed;
            _controller.Move(move * Runner.DeltaTime);

            // Apply gravity
            if (_controller.isGrounded)
            {
                _velocity.y = -0.1f; // Small downward force to keep grounded
                if (Input.GetKey(KeyCode.Space))
                {
                    _velocity.y = jumpForce; // Jumping
                }
            }
            else
            {
                _velocity.y -= gravity * Runner.DeltaTime; // Apply gravity
            }

            _controller.Move(_velocity * Runner.DeltaTime);
        }
    }
}
