using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun currentGun;

    private float currentFireRate;

    private bool isReload = false;
    private bool isFineSightMode = false;

    private Vector3 originPos;     // 본래 포지션 값

    [SerializeField] private ObiEmitter emitter;
    [SerializeField] private float emissionSpeed = 10;

    private void Start()
    {
        originPos = transform.localPosition;
    }

    void Update()
    {
        GunFireRateCalc();
        TryFire();
        TryReload();
        TryFineSight();
    }

    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        if (Input.GetMouseButton(0))
        {
            if (currentFireRate <= 0 && !isReload)
                //Fire();
                Shoot();
        }
        else
        {
            emitter.speed = 0;
        }
    }

    private void Fire()
    {
        if (currentGun.currentBulletCount > 0)
            Shoot();
        else
        {
            emitter.speed = 0;
            Debug.Log("탄약 없음");
        }
    }

    private void Shoot()
    {
        currentGun.currentBulletCount--;
        currentFireRate = currentGun.fireRate;
        emitter.speed = emissionSpeed;
    }

    private void TryReload()
    {
        if (!isReload && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadCoroutine());
            emitter.KillAll();
        }
    }

    IEnumerator ReloadCoroutine()
    {
        Debug.Log("재장전");
        isReload = true;
        currentGun.anim.SetTrigger("Reload");

        yield return new WaitForSeconds(currentGun.reloadTime);

        currentGun.currentBulletCount = currentGun.maxBulletCount;

        isReload = false;
    }

    private void TryFineSight()
    {
        if (Input.GetMouseButtonDown(1))
        {
            FineSight();
        }
    }

    private void FineSight()
    {
        isFineSightMode = !isFineSightMode;
        currentGun.anim.SetBool("FineSightMode", isFineSightMode);

        if (isFineSightMode)
        {
            StopAllCoroutines();
            StartCoroutine(FineSightActivateCoroutine());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(FineSightDeactivateCoroutine()); 
        }
    }

    IEnumerator FineSightActivateCoroutine()
    {
        while (currentGun.transform.localPosition != currentGun.fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, currentGun.fineSightOriginPos, 0.1f);
            yield return null;
        }
    }

    IEnumerator FineSightDeactivateCoroutine()
    {
        while (currentGun.transform.localPosition != originPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, originPos, 0.1f);
            yield return null;
        }
    }
}
