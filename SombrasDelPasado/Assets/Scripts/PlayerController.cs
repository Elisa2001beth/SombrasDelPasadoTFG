
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;


    // Animator reference
    private Animator _animator;

    // Control de movimiento
    public bool puedeMoverse = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (puedeMoverse) 
        {
            GatherInput();
            Look();
            UpdateAnimator();
        }
    }

    private void FixedUpdate()
    {
        if (puedeMoverse) 
        {
            Move();
        }
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        float moveSpeed = _input.magnitude;
        _animator.SetFloat("Speed", moveSpeed);

        bool isMoving = moveSpeed > 0.1f;
        _animator.SetBool("IsMoving", isMoving);


    }

    // Método público para permitir o restringir el movimiento del jugador
    public void PermitirMovimiento(bool permitir)
    {
        puedeMoverse = permitir;
        if (!puedeMoverse)
        {
            _animator.SetFloat("Speed", 0);
            _animator.SetBool("IsMoving", false);
        }

    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
