using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private GameManager gameManager;

    [SerializeField] private Transform footPoint;
    [SerializeField] private float playerSpeed = 1;
    [SerializeField] private float jumpSpeed = 1;
    [SerializeField] private LayerMask layerMask;

    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private bool isOnGround;

    private SoundManager soundManager;


    void Start()
    {
        gameManager = GameManager.Instance;
        playerRb = transform.GetComponent<Rigidbody2D>();
        playerAnim = transform.GetComponent<Animator>();
        soundManager = SoundManager.Instance;
    }


    void Update()
    {
        if (!gameManager.RunGame)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(new Vector2(playerRb.velocity.x, jumpSpeed));
            StartCoroutine(WaitForJump());
        }

        if (transform.position.y < -2)
        {
            PlayerDead();
        }
    }

    private void FixedUpdate()
    {
        if (!gameManager.RunGame)
        {
            return;
        }


        if (Input.GetKey(KeyCode.D))
        {
            MoveControl(1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveControl(-1);
        }
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y);
            AnimControl(2);
        }
        if (Physics2D.Raycast(footPoint.position, transform.TransformDirection(Vector3.down), 0.07f, layerMask))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

    }

    IEnumerator WaitForJump()
    {
        AnimControl(3);
        yield return new WaitForSeconds(0.3f);
        AnimControl(5);
        yield return new WaitForSeconds(0.3f);
        AnimControl(6);

    }

    private void MoveControl(int getMoveValue)
    {
        playerRb.velocity = new Vector2(getMoveValue * playerSpeed, playerRb.velocity.y);
        transform.localScale = new Vector3(getMoveValue * 1, 1, 1);
        AnimControl(1);
    }

    private void AnimControl(int getAnimValue)
    {
        playerAnim.SetInteger("PlayerAnimStatus", getAnimValue);
    }
    public void PlayerDead()
    {
        if (!gameManager.RunGame)
        {
            return;
        }
        soundManager.PlaySound(1);
        gameManager.GameFail();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chest") && gameManager.RunGame)
        {
            gameManager.GameWin();
            collision.transform.GetComponent<Animator>().SetTrigger("ChestTrigger");
            soundManager.PlaySound(2);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                LevelManager.Instance.NextLevel();
            });
        }

    }

}
