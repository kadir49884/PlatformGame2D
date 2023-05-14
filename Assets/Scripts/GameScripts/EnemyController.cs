using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;

    private bool isMoveStart;
    private float rightLimitPosX;
    private float leftLimitPosX;
    private float direction = 0.3f;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.GameStart += GameStarter;
        rightLimitPosX = transform.position.x + 0.2f;
        leftLimitPosX = transform.position.x - 0.2f;

    }

    public void GameStarter()
    {
        transform.GetComponent<Animator>().SetInteger("EnemyStatus", 1);
        isMoveStart = true;
    }

    private void Update()
    {
        if (isMoveStart)
        {
            if (transform.position.x > rightLimitPosX)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (transform.position.x < leftLimitPosX)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            transform.Translate(Vector2.right * direction * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController playerController))
        {
            if (playerController.transform.position.y > transform.position.y + 0.2f)
            {
                EnemyDead();
            }
            else
            {
                playerController.PlayerDead();
            }
        }
    }

    private void EnemyDead()
    {
        CanvasManager.Instance.UpdateScore(10);
        SoundManager.Instance.PlaySound(0);
        Destroy(gameObject);
    }
}
