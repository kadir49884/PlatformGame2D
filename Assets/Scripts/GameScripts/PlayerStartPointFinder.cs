using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPointFinder : Singleton<PlayerStartPointFinder>
{
    private void Awake()
    {
        PlayerController.Instance.transform.position = transform.position;
    }
}
