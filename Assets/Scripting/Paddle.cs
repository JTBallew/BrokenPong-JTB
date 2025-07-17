using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerType 
{ 
    Player1, 
    Player2 
}

public class Paddle : MonoBehaviour
{
    public PlayerType currentPlayer;
    public float speed = 10f;
    public float borderDistance = 4f;

    [SerializeField] private InputAction input;
    private float movementDirection;

    void Awake()
    {
        input.performed += OnMovePerformed;
        input.canceled += OnMoveCanceled;
    }

    void OnEnable()
    {
        input.Enable();
    }
    
    void OnDisable()
    {
        input.Disable();
    }

    /// <summary>
    /// Updates the movement direction to positive or negative depending on the buttons pressed
    /// </summary>
    /// <param name="context">What buttons need to be pressed to provide an update</param>
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<float> ();
    }

    /// <summary>
    /// Sets the movement direction to 0 when no buttons are pressed
    /// </summary>
    /// <param name="context">What buttons should not be pressed to set the variable to 0</param>
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movementDirection = 0;
    }

    void Update()
    {
        float movement = movementDirection * speed * Time.deltaTime;
        transform.Translate(0f, movement, 0f);

        float clampedY = Mathf.Clamp(transform.position.y, -borderDistance, borderDistance);
        transform.position = new Vector3(transform.position.x, clampedY, 0f);
    }
}
