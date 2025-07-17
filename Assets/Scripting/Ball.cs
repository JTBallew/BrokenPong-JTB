using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        ScoreWall,
        BounceWall,
        Player
    }

    [SerializeField] private float speed = 8f;
    [SerializeField] private List<string> tags;
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

    /// <summary>
    /// Resets the Ball to (0,0) and applies a random velocity
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int) CollisionTag.ScoreWall]))
        {
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
            PlayAudioClip(scoreClip);
        }
        else if (other.CompareTag(tags[(int) CollisionTag.BounceWall]))
        {
            velocity.y = -velocity.y;
            PlayAudioClip(playerClip);
        }
        else if (other.CompareTag(tags[(int) CollisionTag.Player]))
        {
            velocity.x = -velocity.x;
            velocity.y = transform.position.y - other.transform.position.y;
            velocity = velocity.normalized;
            PlayAudioClip(wallClip);
        }
    }

    /// <summary>
    /// Plays an audio clip whenever the ball collides with another object
    /// </summary>
    /// <param name="clip">Whcih audio clip should be played</param>
    private void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
