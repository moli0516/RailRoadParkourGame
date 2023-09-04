using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float turnSpeed = 0.1f;

    [Header("Lane Setting")]
    [SerializeField] private float distance = 2.0f;
    [SerializeField] private int direction = 1;

    [Header("Jump Setting")]
    [SerializeField] private float gravity = 12.0f;
    [SerializeField] private float jumpForce = 10.0f;

    [Header("Roll Settings")]
    [SerializeField] private float rollingTime = 1.0f;
    [SerializeField] private float targetCapsuleHeight = 0.5f;
    [SerializeField] private Vector3 targetCapsuleCenter = new Vector3(0, 0.95f, 0);

    [Header("Grounding")]
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private float groundCheckOffset = 0.45f;
    [SerializeField] private float groundCheckDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("SFX")]
    [SerializeField] private AudioSource swipeSFX;
    [SerializeField] private AudioSource rollSFX;
    [SerializeField] private AudioSource jumpSFX;

    private float verticalVelocity = -0.1f;
    private CharacterController charCtrl;
    private Animator ani;
    private float capsuleHeight;
    private Vector3 capsuleCenter;
    private Vector3 groundNormal = Vector3.up;
    private bool isRolling;
    private float rollingElapsedTime;

    private void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
        capsuleHeight = charCtrl.height;
        capsuleCenter = charCtrl.center;
    }

    private void SetMoveDirection(bool isRight)
    {
        direction += isRight ? 1 : -1;
        direction = Mathf.Clamp(direction, 0, 2);
    }

    private void SetLookDirection()
    {
        Vector3 lookDirection = charCtrl.velocity;
        if (lookDirection == Vector3.zero) return;
        lookDirection.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, lookDirection, turnSpeed);
        
    }

    private void MoveTo()
    {
        Vector3 targetPos = transform.position.z * Vector3.forward;
        if (direction == 0) targetPos += Vector3.left * distance;
        else if (direction == 2) targetPos += Vector3.right * distance;

        Vector3 movement = Vector3.zero;
        movement.x = (targetPos - transform.position).normalized.x * speed;
        movement.y = verticalVelocity;
        movement.z = speed;

        charCtrl.Move(movement * Time.deltaTime);
    }

    private bool CheckGrounded()
    {
        Vector3 start = transform.position + Vector3.up * groundCheckOffset;
        if (Physics.SphereCast(start,groundCheckRadius,Vector3.down,out RaycastHit hit, groundCheckDistance, groundMask))
        {
            groundNormal = hit.normal;
            return true;
        }
        groundNormal = Vector3.up;
        return false;
        

    }

    private void Jump()
    {
        if (CheckGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpSFX.Play();
                verticalVelocity = jumpForce;            
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);
        }
        ani.SetBool("isGrounded", CheckGrounded());
    }
    
    private void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isRolling || !CheckGrounded()) return;
            rollSFX.Play();
            rollingElapsedTime = 0f;
            isRolling = true;
            charCtrl.height = targetCapsuleHeight;
            charCtrl.center = targetCapsuleCenter;
        }
        rollingElapsedTime += Time.deltaTime;
        if (rollingElapsedTime >= rollingTime && isRolling)
        {
            isRolling = false;
            rollingElapsedTime = 0f;
            charCtrl.height = capsuleHeight;
            charCtrl.center = capsuleCenter;
        }
        ani.SetBool("isRolling", isRolling);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetMoveDirection(false);
            swipeSFX.Play();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetMoveDirection(true);
            swipeSFX.Play();
        }
        MoveTo();
        SetLookDirection();
        Jump();
        Roll();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (CheckGrounded())
        {
            Gizmos.color = Color.green;
        }

        Vector3 start = transform.position + Vector3.up * groundCheckOffset;
        Vector3 end = start + Vector3.down * groundCheckDistance;
        Gizmos.DrawWireSphere(start, groundCheckRadius);
        Gizmos.DrawWireSphere(end, groundCheckRadius);
    }
}
