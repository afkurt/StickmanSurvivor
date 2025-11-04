using UnityEngine;

public class Player : Entity
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private FloatingJoystick _joystick;

    [Header("Attributes")]
    [SerializeField] private Vector3 _direction;
    [SerializeField] private float _speed = 6f;
    private float _gravity = -3f;

    private Animator _animator;

    public GameObject Protectile;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerMove();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onDamageTake?.Invoke(10);
            Debug.Log($"{CurrentHealth}");
        }
    }


    public void PlayerMove()
    {
        _direction.x= _joystick.Horizontal;
        _direction.z = _joystick.Vertical;
        
        _controller.Move(_direction * _speed * Time.deltaTime);
        transform.LookAt(transform.position + new Vector3 (_direction.x , 0, _direction.z));

        float animSpeed = new Vector3(_direction.x, 0 , _direction.z).magnitude;
        _animator.SetFloat("Speed", animSpeed);
    }
    public void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _direction.y += _gravity * Time.deltaTime;
            
        }
        else
        {
            _direction.y = -1f;
        }
    }

    public void SpawnProtectile()
    {
        Instantiate(Protectile, transform.position, Quaternion.identity);
    }

}
