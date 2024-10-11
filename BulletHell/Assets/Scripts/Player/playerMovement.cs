using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    PlayerStats player;
    private Rigidbody2D body;
    [HideInInspector]
    public float lastXValue;
    [HideInInspector]
    public float lastYValue;
    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector2 lastMovedDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
        body = GetComponent<Rigidbody2D>();
        lastMovedDirection = new Vector2(1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(x, y).normalized; 

        if (moveDirection.x != 0)
        {
            lastXValue = moveDirection.x;
            lastMovedDirection = new Vector2(lastXValue, 0);
        }
        if (moveDirection.y != 0)
        {
            lastYValue = moveDirection.y;
            lastMovedDirection = new Vector2(0, lastYValue);
        }
        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedDirection = new Vector2(lastXValue, lastYValue);
        }
    }

    void Move()
    {
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        body.velocity = moveDirection * player.CurrentMoveSpeed;
    }
}
