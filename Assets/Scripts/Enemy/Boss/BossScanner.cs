using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScanner : MonoBehaviour
{
    public Transform target;
    public float targetDistance;
    public float viewingAngle; // �⺻���� �÷��̾ �ٶ󺸹Ƿ� �ʿ������
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
