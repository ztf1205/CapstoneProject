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
    [field: Header("OnDollyZoom")]
    [field: SerializeField] public EventReference onDolly { get; private set; }
    [field: Header("OffDollyZoom")]
    [field: SerializeField] public EventReference offDolly { get; private set; }

    private void Awake()
    {
        EventManager.Subscribe("GainCube", PlayCubeSound);
        EventManager.Subscribe("GainCrystal", PlayCrystalSound);
        EventManager.Subscribe("CubP", PlayCubePedestalSound);
        EventManager.Subscribe("CrysP", PlayCrystalPedestalSound);
        EventManager.Subscribe("DollyZoom2D", PlayDolly2DSound);
        EventManager.Subscribe("DollyZoom3D", PlayDolly3DSound);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("GainCube", PlayCubeSound);
        EventManager.Unsubscribe("GainCrystal", PlayCrystalSound);
        EventManager.Unsubscribe("CubP", PlayCubePedestalSound);
        EventManager.Unsubscribe("CrysP", PlayCrystalPedestalSound);
        EventManager.Unsubscribe("DollyZoom2D", PlayDolly2DSound);
        EventManager.Unsubscribe("DollyZoom3D", PlayDolly3DSound);
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
    private void PlayDolly2DSound()
    {
        Debug.Log("돌리줌 2D!");
        //FMODUnity.RuntimeManager.PlayOneShot(onDolly, transform.position);
    }
    private void PlayDolly3DSound()
    {
        Debug.Log("돌리줌 3D!");
        //FMODUnity.RuntimeManager.PlayOneShot(offDolly, transform.position);
    }
}
