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
    [field: Header("StandardCamera")]
    [field: SerializeField] public EventReference changeDimension { get; private set; }
    [field: Header("DollyZoom2D")]
    [field: SerializeField] public EventReference onDolly2D { get; private set; }
    [field: Header("DollyZoom3D")]
    [field: SerializeField] public EventReference onDolly3D { get; private set; }
    [field: Header("JumpPad")]
    [field: SerializeField] public EventReference jumpPad { get; private set; }
    [field: Header("SuperJumpPad")]
    [field: SerializeField] public EventReference superJumpPad { get; private set; }
    [field: Header("TimeStop")]
    [field: SerializeField] public EventReference timeStop { get; private set; }
    

    private void Awake()
    {
        EventManager.Subscribe("GainCube", PlayCubeSound);
        EventManager.Subscribe("GainCrystal", PlayCrystalSound);
        EventManager.Subscribe("CubP", PlayCubePedestalSound);
        EventManager.Subscribe("CrysP", PlayCrystalPedestalSound);
        EventManager.Subscribe("DollyZoom2D", PlayDolly2DSound);
        EventManager.Subscribe("DollyZoom3D", PlayDolly3DSound);
        EventManager.Subscribe("JumpPad", PlayJumpPadSound);
        EventManager.Subscribe("SuperJumpPad", PlaySuperJumpPadSound);
        EventManager.Subscribe("TimeStop", PlayTimeStopSound);
        EventManager.Subscribe("StandardCamera", PlayChangeDimensionSound);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe("GainCube", PlayCubeSound);
        EventManager.Unsubscribe("GainCrystal", PlayCrystalSound);
        EventManager.Unsubscribe("CubP", PlayCubePedestalSound);
        EventManager.Unsubscribe("CrysP", PlayCrystalPedestalSound);
        EventManager.Unsubscribe("DollyZoom2D", PlayDolly2DSound);
        EventManager.Unsubscribe("DollyZoom3D", PlayDolly3DSound);
        EventManager.Unsubscribe("JumpPad", PlayJumpPadSound);
        EventManager.Unsubscribe("SuperJumpPad", PlaySuperJumpPadSound);
        EventManager.Unsubscribe("TimeStop", PlayTimeStopSound);
        EventManager.Unsubscribe("StandardCamera", PlayChangeDimensionSound);
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
        //FMODUnity.RuntimeManager.PlayOneShot(onDolly2D, transform.position);
    }
    private void PlayDolly3DSound()
    {
        Debug.Log("돌리줌 3D!");
        //FMODUnity.RuntimeManager.PlayOneShot(onDolly3D, transform.position);
    }
    private void PlayJumpPadSound()
    {
        Debug.Log("강력 점프!");
        //FMODUnity.RuntimeManager.PlayOneShot(jumpPad, transform.position);
    }
    private void PlaySuperJumpPadSound()
    {
        Debug.Log("초강력 점프!");
        //FMODUnity.RuntimeManager.PlayOneShot(superJumpPad, transform.position);
    }
    private void PlayTimeStopSound()
    {
        Debug.Log("시간 정지!");
        //FMODUnity.RuntimeManager.PlayOneShot(timeStop, transform.position);
    }
    private void PlayChangeDimensionSound()
    {
        Debug.Log("차원 변환!");
        //FMODUnity.RuntimeManager.PlayOneShot(changeDimension, transform.position);
    }
}
