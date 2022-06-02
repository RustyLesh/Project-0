using UnityEngine;
using UnityEngine.InputSystem;

public class CSS_PlayerMovement : MonoBehaviour
{
    private Vector2 rawInput;

    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector2 worldMinBounds;
    private Vector2 worldMaxBounds;

    private Camera mainCamera;

    [SerializeField] private float moveSpeed = 15f;

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        Move();
    }

    //Converts the min and max bounderies to world points
    private void InitBounds()
    {
        mainCamera = Camera.main;

        worldMinBounds = mainCamera.ViewportToWorldPoint(minBounds);
        worldMaxBounds = mainCamera.ViewportToWorldPoint(maxBounds);
    }

    //Movement control
    private void Move()
    {
        Vector2 delta = moveSpeed * Time.deltaTime * rawInput;
        Vector2 newPos = transform.position;

        newPos.x = Mathf.Clamp(transform.position.x + delta.x, worldMinBounds.x, worldMaxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, worldMinBounds.y, worldMaxBounds.y);

        transform.position = newPos;
    }

    //Called through player controller input script, connected through inspector
    private void OnPlayerMovement(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnDrawGizmosSelected()
    {
        Camera gizmoCam = Camera.main;
        // Draws a blue line from this transform to the target
        //Gizmo for showing player bounderies.
        Gizmos.color = Color.blue;

        Vector2 botLeft = gizmoCam.ViewportToWorldPoint(new Vector2(0, minBounds.y));
        Vector2 botRight = gizmoCam.ViewportToWorldPoint(new Vector2(1, minBounds.y));

        Vector2 topLeft = gizmoCam.ViewportToWorldPoint(new Vector2(0, maxBounds.y));
        Vector2 TopRight = gizmoCam.ViewportToWorldPoint(new Vector2(1, maxBounds.y));

        Vector2 leftTop = gizmoCam.ViewportToWorldPoint(new Vector2(minBounds.x, 0));
        Vector2 leftBot = gizmoCam.ViewportToWorldPoint(new Vector2(minBounds.x, 1));

        Vector2 rightTop = gizmoCam.ViewportToWorldPoint(new Vector2(maxBounds.x, 1));
        Vector2 rightBot = gizmoCam.ViewportToWorldPoint(new Vector2(maxBounds.x, 0));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(topLeft, TopRight);
        Gizmos.DrawLine(leftTop, leftBot);
        Gizmos.DrawLine(rightTop, rightBot);
    }
}