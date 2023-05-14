using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Transform playerTransform;
    private bool isAnimPlay;
    private Animator itemAnim;
    private float playerDistance;
    private float distanceDetectorValue = 1;

    private void Awake()
    {
        itemAnim = transform.GetComponent<Animator>();
    }

    private void Start()
    {
        playerTransform = PlayerController.Instance.transform;
    }


    private void LateUpdate()
    {
        playerDistance = Vector3.Distance(playerTransform.position, transform.position);

        if (!isAnimPlay && playerDistance < distanceDetectorValue)
        {
            AnimController(true);
        }
        else if (isAnimPlay && playerDistance > distanceDetectorValue)
        {
            AnimController(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CanvasManager.Instance.UpdateScore(1);
            Destroy(gameObject);
            SoundManager.Instance.PlaySound(0);
        }
    }

    private void AnimController(bool getAnimValue)
    {
        itemAnim.SetBool("ItemAnimPlay", getAnimValue);
        isAnimPlay = getAnimValue;
    }


}
