using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [field: Header("Gain Cube")]
    [field: SerializeField] public EventReference cube { get; private set; }
    [field: Header("Gain Crystal")]
    [field: SerializeField] public EventReference crystal { get; private set; }

    private void Awake()
    {
        EventManager.Subscribe("GainCube", PlayCubeSound);
        EventManager.Subscribe("GainCrystal", PlayCrystalSound);
        EventManager.Subscribe("CubP", PlayCubePedestalSound);
        EventManager.Subscribe("CrysP", PlayCrystalPedestalSound);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("GainCube", PlayCubeSound);
        EventManager.Unsubscribe("GainCrystal", PlayCrystalSound);
        EventManager.Unsubscribe("CubP", PlayCubePedestalSound);
        EventManager.Unsubscribe("CrysP", PlayCrystalPedestalSound);
    }

    private void PlayCubeSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(cube, transform.position);
    }

    private void PlayCrystalSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(crystal, transform.position);
    }

    private void PlayCubePedestalSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(cube, transform.position);
    }
    private void PlayCrystalPedestalSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(crystal, transform.position);
    }
}
