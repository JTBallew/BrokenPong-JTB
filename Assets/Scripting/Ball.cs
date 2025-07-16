using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> goalTags;
    [SerializeField] private string borderTag;
    private Vector2 velocity;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip playerClip;
    [SerializeField] private AudioClip scoreClip;
    [SerializeField] private AudioClip wallClip;
    void Start()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(goalTags[0]))
        {
            ResetBall();
        }
        else if (other.CompareTag(borderTag))
        {
            velocity.y = -velocity.y;
        }
        else if (other.CompareTag("Player"))
        {
            velocity.x = -velocity.x;
            velocity.y = transform.position.y - other.transform.position.y;
            velocity = velocity.normalized;
        }
    }
}
