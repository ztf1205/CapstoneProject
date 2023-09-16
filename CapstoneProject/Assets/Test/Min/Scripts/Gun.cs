using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range;         // 사정 거리
    public float accuracy;      // 정확도
    public float fireRate;      // 연사 속도
    public float reloadTime;    // 재장전 속도

    public int reloadBulletCount;   // 탄약 재장전 개수
    public int currentBulletCount;  // 현재 탄알집에 남아있는 총알 개수
    public int maxBulletCount;      // 소지 가능한 최대 총알 개수

    public float retroActionForce;          // 반동 세기
    public float retroActionFineSightForce; // 정조준시의 반동 세기

    public Vector3 fineSightOriginPos;      // 정조준시의 위치
    public Animator anim;
}
