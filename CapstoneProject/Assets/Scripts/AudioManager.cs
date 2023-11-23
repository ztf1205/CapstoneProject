using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [field: Header("Gain Cube")]
    [field: SerializeField] public EventReference cube { get; private set; }
    [field: Header("Get Item")]
    [field: SerializeField] public EventReference getItem { get; private set; }
    [field: Header("Respawn")]
    [field: SerializeField] public EventReference respawn { get; private set; }
    [field: Header("Landing Item")]
    [field: SerializeField] public EventReference landingItem { get; private set; }
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
    [field: Header("TimeStop")]
    [field: SerializeField] private FMODUnity.EventReference _timeStop;
    private FMOD.Studio.EventInstance timeStop;

    [field: Header("Running")]
    [field: SerializeField] private FMODUnity.EventReference _running;
    private FMOD.Studio.EventInstance running;
    [field: Header("Jump")]
    [field: SerializeField] public EventReference jump { get; private set; }
    [field: Header("Landing")]
    [field: SerializeField] public EventReference landing { get; private set; }
    [field: Header("SwitchDimensionFail")]
    [field: SerializeField] public EventReference switchDimensionFail { get; private set; }


    private void Awake()
    {
        EventManager.Subscribe("GainCube", PlayCubeSound);
        EventManager.Subscribe("GainCrystal", PlayCrystalSound);
        EventManager.Subscribe("CubP", PlayCubePedestalSound);
        EventManager.Subscribe("CrysP", PlayCrystalPedestalSound);
        EventManager.Subscribe("DollyZoom2D", PlayDolly2DSound);
        EventManager.Subscribe("DollyZoom3D", PlayDolly3DSound);
        EventManager.Subscribe("JumpPad", PlayJumpPadSound);
        EventManager.Subscribe("TimeStop", PlayTimeStopSound);
        EventManager.Subscribe("StandardCamera", PlayChangeDimensionSound);
        EventManager.Subscribe("OffTimeStop", OffTimeStopSound);
        EventManager.Subscribe("OnPlayerLanding", PlayLandingSound);
        EventManager.Subscribe("OnPlayerWalkStart", PlayWalkingSound);
        EventManager.Subscribe("OnPlayerWalkEnd", OffWalkingSound);
        EventManager.Subscribe("OnPlayerJump", PlayJumpSound);
        EventManager.Subscribe("SwitchDimensionFailSound", PlaySwitchDimensionFailSound);
        EventManager.Subscribe("GetItem", PlayGetItemSound);
        EventManager.Subscribe("LandingItem", PlayLandingItemSound);
        EventManager.Subscribe("Respawn", PlayRespawnSound);

        if (_timeStop.IsNull == false)
        {
            timeStop = FMODUnity.RuntimeManager.CreateInstance(_timeStop);
        }

        if (_running.IsNull == false)
        {
            running = FMODUnity.RuntimeManager.CreateInstance(_running);
        }
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
        EventManager.Unsubscribe("TimeStop", PlayTimeStopSound);
        EventManager.Unsubscribe("StandardCamera", PlayChangeDimensionSound);
        EventManager.Unsubscribe("OffTimeStop", OffTimeStopSound);
        EventManager.Unsubscribe("OnPlayerLanding", PlayLandingSound);
        EventManager.Unsubscribe("OnPlayerWalkStart", PlayWalkingSound);
        EventManager.Unsubscribe("OnPlayerWalkEnd", OffWalkingSound);
        EventManager.Unsubscribe("OnPlayerJump", PlayJumpSound);
        EventManager.Unsubscribe("SwitchDimensionFailSound", PlaySwitchDimensionFailSound);
        EventManager.Unsubscribe("GetItem", PlayGetItemSound);
        EventManager.Unsubscribe("GetItem", PlayLandingItemSound);
        EventManager.Unsubscribe("Respawn", PlayRespawnSound);

        OffTimeStopSound();
        OffWalkingSound();

        timeStop.release();
        running.release();
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
        FMODUnity.RuntimeManager.PlayOneShot(onDolly2D, transform.position);
    }
    private void PlayDolly3DSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(onDolly3D, transform.position);
    }
    private void PlayJumpPadSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(jumpPad, transform.position);
    }
    private void PlayTimeStopSound()
    {
        timeStop.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        timeStop.start();
    }
    private void OffTimeStopSound()
    {
        timeStop.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    private void PlayChangeDimensionSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(changeDimension, transform.position);
    }
    private void PlayWalkingSound()
    {
        running.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        running.start();
    }
    private void OffWalkingSound()
    {
        running.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    private void PlayJumpSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(jump, transform.position);
    }
    private void PlayLandingSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(landing, transform.position);
    }
    private void PlaySwitchDimensionFailSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(switchDimensionFail, transform.position);
    }
    private void PlayGetItemSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(getItem, transform.position);
    }
    private void PlayLandingItemSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(landingItem, transform.position);
    }
    private void PlayRespawnSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(respawn, transform.position);
    }
}
