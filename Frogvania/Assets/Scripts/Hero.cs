using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float jumpVelocity;
    public Vector2 velocity;
    public LayerMask wallMask;

    private bool walk, walk_left, walk_right, jump;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPayerInout();

        UpdatePlayerPosition();
    }

    void UpdatePlayerPosition()
    {
        Vector3 pos = transform.localPosition;
        Vector3 scale = transform.localScale;

        if (walk)
        {
            if (walk_left)
            {
                pos.x -= velocity.x = Time.deltaTime * 5;

                scale.x = -2;
            }

            if (walk_right)
            {
                pos.x += velocity.x = Time.deltaTime * 5;

                scale.x = 2;
            }

            //pos = CheckWallRays(pos, scale.x);

        }
        if (jump)
        {
            rb2D.AddForce(new Vector2(rb2D.velocity.y, 40), ForceMode2D.Impulse);
        }

        transform.localPosition = pos;
        transform.localScale = scale;
    }

    void CheckPayerInout()
    {
        bool input_left = Input.GetKey(KeyCode.LeftArrow);
        bool input_right = Input.GetKey(KeyCode.RightArrow);
        bool input_space = Input.GetKeyDown(KeyCode.UpArrow);

        walk = input_left || input_right;

        walk_left = input_left;

        walk_right = input_right;

        jump = input_space;
    }
    Vector3 CheckWallRays(Vector3 pos, float direction)
    {
        Vector2 originTop = new Vector2(pos.x + direction * .4f, pos.y + 1f - 0.2f);
        Vector2 originMiddle = new Vector2(pos.x + direction * .4f, pos.y);
        Vector2 originBottom = new Vector2(pos.x + direction * .4f, pos.y - 1f + 0.2f);

        RaycastHit2D wallTop = Physics2D.Raycast(originTop, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallMiddle = Physics2D.Raycast(originMiddle, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);
        RaycastHit2D wallBottom = Physics2D.Raycast(originBottom, new Vector2(direction, 0), velocity.x * Time.deltaTime, wallMask);

        if (wallTop.collider != null || wallMiddle.collider != null || wallBottom != null)
        {
            pos.x -= velocity.x * Time.deltaTime * direction;
        }

        return pos;
    }
}
