using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : Entity
{
    [Header("Settings")]
    [SerializeField] LayerMask shootMask;
    [SerializeField] float movementSpeed = 100f;
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] float runMultiplier = 2f;
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float jumpForce = 15f;

    [Header("References")]
    [SerializeField] Transform defaultAimPos;
    [SerializeField] Transform ADSPos;
    [SerializeField] GameObject reticle;
    [SerializeField] Camera fpsCamera;
    [SerializeField] PlayerWeaponController weaponController;
    [SerializeField] GameObject hitParticle;
    [SerializeField] GameObject wallHitParticle;



    Animator _anim;
    CharacterController _controller;
    Vector3 _moveDirection = Vector3.zero;
    Vector3 _lookVector = Vector3.zero;
    float _lookRotationX = 0f;
    bool _isFiring = false;
    float _shootTimer = 0f;


    public override void Awake() 
    {
        base.Awake();

        _controller = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.visible = false;
    }

    private void Update() 
    {
        Movement();
        Look();

        Fire();

        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            weaponController.ReloadAnimation();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            weaponController.SetAimingPosition(ADSPos.position);
            reticle.SetActive(false);
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            weaponController.SetAimingPosition(defaultAimPos.position);
            reticle.SetActive(true);
        }

        // if(Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     Jump();
        // }
    }

    void Jump()
    {
        Vector3 p = transform.position;
        p.y += jumpForce * Time.deltaTime;
        transform.position = p;
    }

    void Look()
    {
        float mouseX = _lookVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = _lookVector.y * mouseSensitivity * Time.deltaTime;

        _lookRotationX -= mouseY;
        _lookRotationX = Mathf.Clamp(_lookRotationX, -90, 90);

        fpsCamera.transform.localRotation = Quaternion.Euler(_lookRotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        Vector3 move = transform.right * _moveDirection.x + transform.forward * _moveDirection.z;
        bool isRunKeyPressed = Keyboard.current.shiftKey.IsPressed();
        float speed = isRunKeyPressed ? movementSpeed * runMultiplier : movementSpeed;
        _controller.Move(move * speed * Time.deltaTime);

        int i = move.x != 0 || move.y != 0 ? 1 : 0;
        weaponController.SetMovementAnimation(isRunKeyPressed ? i * runMultiplier : i);

        if (!Physics.Raycast(transform.position, Vector3.down, 1.2f))
        {
            Vector3 p = transform.position;
            p.y -= gravity * Time.deltaTime;
            transform.position = p;
        }
    }   

    void Fire()
    {
        if(weaponController.CurrentAmmo <= 0)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _isFiring = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _isFiring = false;
            weaponController.ShootEnd();
        }

        if (_isFiring)
        {
            _shootTimer += Time.deltaTime;
            if(_shootTimer >= weaponController.FireRate)
            {
                weaponController.ShootAnimation();
                if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, Mathf.Infinity, shootMask))
                {
                    if (hit.transform.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.TakeDamage(weaponController.Damage);
                        GameObject obj = Instantiate(hitParticle, hit.point, Quaternion.identity);
                        obj.transform.SetParent(hit.transform, true);
                    }
                    else
                    {
                        if(hit.transform.CompareTag("Wall"))                        
                        {
                            GameObject obj = Instantiate(wallHitParticle, hit.point, Quaternion.identity);
                            obj.transform.SetParent(hit.transform, true);
                        }
                    }
                }

                _shootTimer = 0f;

                if(weaponController.CurrentAmmo <= 0)
                {
                    weaponController.ShootEnd();
                    _isFiring = false;
                }                
            }
        }

        // Debug.DrawLine(fpsCamera.transform.position, fpsCamera.transform.forward * 1000f, Color.magenta);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(inputVec.x, 0, inputVec.y);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookVector = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }
}