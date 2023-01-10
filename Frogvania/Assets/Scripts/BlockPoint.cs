using System.Collections;
using UnityEngine;

public class BlockPoint : MonoBehaviour
{
    public static GameManager game;
    private bool animating;

    private void Start()
    {
        game = GameManager.Instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(!animating && collision.gameObject.name == "Hero")
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                game.AddPoint();

                StartCoroutine(Animate());
            }
        }
    }
        private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        Destroy(gameObject);

        animating = false;

    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.25f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}