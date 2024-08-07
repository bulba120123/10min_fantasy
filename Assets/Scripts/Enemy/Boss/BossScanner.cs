using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScanner : MonoBehaviour
{
    public Transform target;
    public float targetDistance;
    public float viewingAngle; // 기본으로 플레이어를 바라보므로 필요없을듯
    public float viewingDistance;

    private void OnEnable()
    {
        target = GameManager.instance.player.transform;
    }
    private void FixedUpdate()
    {
        targetDistance = Vector2.Distance(transform.position, target.position);
    }
}
