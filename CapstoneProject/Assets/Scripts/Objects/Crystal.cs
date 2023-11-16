using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public bool IsAcquired { get; private set; } = false;
    [SerializeField] private Transform crystalHolder;

    private void Start()
    {
        EventManager.Subscribe("ActivateCrystal", ActivateCrystal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !IsAcquired)
        {
            IsAcquired = true;
            crystalHolder.gameObject.GetComponent<CrystalHolder>().CanHoldCrystal = true;
            MoveCrystal();
            gameObject.SetActive(false);
            EventManager.TriggerEvent("GainCrystal");
        }
    }

    private void MoveCrystal()
    {
        float crystalY = transform.localScale.y / 2;
        float holderY = crystalHolder.localScale.y / 2;
        float targetY = crystalHolder.position.y + crystalY + holderY;
        transform.position = new Vector3(crystalHolder.position.x, targetY, crystalHolder.position.z);
    }

    private void ActivateCrystal()
    {
        gameObject.SetActive (true);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("ActivateCrystal", ActivateCrystal);
    }
}
