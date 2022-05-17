using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private Vector2 moveDirection;

    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    
    private Vector2 worldMinBounds;
    private Vector2 worldMaxBounds;

    [SerializeField ]private float moveSpeed = 100;
    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        moveDirection = playerControls.PlayerShipControls.PlayerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, worldMinBounds.x, worldMaxBounds.x);
        pos.y = Mathf.Clamp(transform.position.y, worldMinBounds.y, worldMaxBounds.y);

        transform.position = pos;
    }

    private void InitBounds()
    {
        Camera mainCamera = Camera.main;

        worldMinBounds = mainCamera.ViewportToWorldPoint(minBounds);
        worldMaxBounds = mainCamera.ViewportToWorldPoint(maxBounds);
    }
}