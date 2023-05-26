using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicTrigger : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] private bool TriggerBaseBass;
    [SerializeField] private bool TriggerChoke;
    [SerializeField] private bool TriggerCreepyOrgan;
    [SerializeField] private bool TriggerDeepHitToBreath1;
    [SerializeField] private bool TriggerDeepHitToBreath2;
    [SerializeField] private bool TriggerDeepHitToBreath3;
    [SerializeField] private bool TriggerDeepHitToBreath4;
    [SerializeField] private bool TriggerEtherealTone;
    [SerializeField] private bool TriggerGradIncHighPitch;
    [SerializeField] private bool TriggerGrowl;
    [SerializeField] private bool TriggerLowCello;
    [SerializeField] private bool TriggerMusicBox1;
    [SerializeField] private bool TriggerMusicBox2;
    [SerializeField] private bool TriggerMusicBox3;
    [SerializeField] private bool TriggerPanFlute;
    [SerializeField] private bool TriggerPulseToSiren;
    [SerializeField] private bool TriggerShepardsTone;
    [SerializeField] private bool TriggerWawaDistortion;
    [SerializeField] private bool TriggerWhistleBreathing;

    [Header("Volumes")]
    [SerializeField] private float BaseBassVol = -1;
    [SerializeField] private float ChokeVol = -1;
    [SerializeField] private float CreepyOrganVol = -1;
    [SerializeField] private float DeepHitToBreath1Vol = -1;
    [SerializeField] private float DeepHitToBreath2Vol = -1;
    [SerializeField] private float DeepHitToBreath3Vol = -1;
    [SerializeField] private float DeepHitToBreath4Vol = -1;
    [SerializeField] private float EtherealToneVol = -1;
    [SerializeField] private float GradIncHighPitchVol = -1;
    [SerializeField] private float GrowlVol = -1;
    [SerializeField] private float LowCelloVol = -1;
    [SerializeField] private float MusicBox1Vol = -1;
    [SerializeField] private float MusicBox2Vol = -1;
    [SerializeField] private float MusicBox3Vol = -1;
    [SerializeField] private float PanFluteVol = -1;
    [SerializeField] private float PulseToSirenVol = -1;
    [SerializeField] private float WawaDistortionVol = -1;
    [SerializeField] private float WhistleBreathingVol = -1;

    private LevelMusicSystem levelMusicSystem;

    private void Start()
    {
        levelMusicSystem = FindObjectOfType<LevelMusicSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Dancer"))
        {
            if(TriggerBaseBass)
            {
                levelMusicSystem.BaseBass.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("BaseBassVol", BaseBassVol);
            }
            if (TriggerChoke)
            {
                levelMusicSystem.Choke.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("ChokeVol", ChokeVol);
            }
            if (TriggerCreepyOrgan)
            {
                levelMusicSystem.CreepyOrgan.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("CreepyOrganVol", CreepyOrganVol);
            }
            if (TriggerDeepHitToBreath1)
            {
                levelMusicSystem.DeepHitToBreath1.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("DeepHitToBreath1Vol", DeepHitToBreath1Vol);
            }
            if (TriggerDeepHitToBreath2)
            {
                levelMusicSystem.DeepHitToBreath2.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("DeepHitToBreath2Vol", DeepHitToBreath2Vol);
            }
            if (TriggerDeepHitToBreath3)
            {
                levelMusicSystem.DeepHitToBreath3.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("DeepHitToBreath3Vol", DeepHitToBreath3Vol);
            }
            if (TriggerDeepHitToBreath4)
            {
                levelMusicSystem.DeepHitToBreath4.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("DeepHitToBreath4Vol", DeepHitToBreath4Vol);
            }
            if (TriggerEtherealTone)
            {
                levelMusicSystem.EtherealTone.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("EtherealToneVol", EtherealToneVol);
            }
            if (TriggerGradIncHighPitch)
            {
                levelMusicSystem.GradIncHighPitch.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("GradIncHighPitchVol", GradIncHighPitchVol);
            }
            if (TriggerGrowl)
            {
                levelMusicSystem.Growl.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("GrowlVol", GrowlVol);
            }
            if (TriggerLowCello)
            {
                levelMusicSystem.LowCello.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("LowCelloVol", LowCelloVol);
            }
            if (TriggerMusicBox1)
            {
                levelMusicSystem.MusicBox1.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("MusicBox1Vol", MusicBox1Vol);
            }
            if (TriggerMusicBox2)
            {
                levelMusicSystem.MusicBox2.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("MusicBox2Vol", MusicBox2Vol);
            }
            if (TriggerMusicBox3)
            {
                levelMusicSystem.MusicBox3.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("MusicBox3Vol", MusicBox3Vol);
            }
            if (TriggerPanFlute)
            {
                levelMusicSystem.PanFlute.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("PanFluteVol", PanFluteVol);
            }
            if (TriggerPulseToSiren)
            {
                levelMusicSystem.PulseToSiren.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("PulseToSirenVol", PulseToSirenVol);
            }
            if (TriggerShepardsTone)
            {
                levelMusicSystem.ShepardsTone.Post(levelMusicSystem.gameObject);

            }
            if (TriggerWawaDistortion)
            {
                levelMusicSystem.WawaDistortion.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("WawaDistortionVol", WawaDistortionVol);
            }
            if (TriggerWhistleBreathing)
            {
                levelMusicSystem.WhistleBreathing.Post(levelMusicSystem.gameObject);
                AkSoundEngine.SetRTPCValue("WhistleBreathingVol", WhistleBreathingVol);
            }
            
            gameObject.SetActive(false);

        }
    }
}
