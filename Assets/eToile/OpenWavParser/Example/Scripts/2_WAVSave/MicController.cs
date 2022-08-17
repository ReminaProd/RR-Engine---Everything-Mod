using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Encender y apagar el micrófono para poder grabar la reacción del jugador.
 */

[RequireComponent(typeof(AudioSource))]

public class MicController : MonoBehaviour
{
    public bool IsWorking = true;
    bool _lastValueOfIsWorking;

    public bool RaltimeOutput = true;
    bool _lastValueOfRaltimeOutput;

    AudioSource _audioSource;
    float _lastVolume = 0;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (IsWorking)
        {
            WorkStart();
        }
    }

    void Update()
    {
        CheckIfIsWorkingChanged();
        CheckIfRealtimeOutputChanged();
    }

    void CheckIfIsWorkingChanged()
    {
        if (_lastValueOfIsWorking != IsWorking)
        {
            if (IsWorking)
            {
                WorkStart();
            }
            else
            {
                WorkStop();
            }
        }

        _lastValueOfIsWorking = IsWorking;
    }

    void CheckIfRealtimeOutputChanged()
    {
        if (_lastValueOfRaltimeOutput != RaltimeOutput)
        {
            DisableSound(RaltimeOutput);
        }

        _lastValueOfRaltimeOutput = RaltimeOutput;
    }

    void DisableSound(bool SoundOn)
    {
        if (SoundOn)
        {
            if (_lastVolume > 0)
            {
                _audioSource.volume = _lastVolume;
            }
            else
            {
                _audioSource.volume = 1f;
            }
        }
        else
        {
            _lastVolume = _audioSource.volume;
            _audioSource.volume = 0f;
        }
    }

    public void WorkStart()
    {
#if !UNITY_WEBGL
        IsWorking = true;
        _audioSource.clip = Microphone.Start(null, true, 10, 44100);
        _audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0))
        {
            _audioSource.Play();
        }
#endif
    }

    public void WorkStop()
    {
#if !UNITY_WEBGL
        IsWorking = false;
        Microphone.End(null);
        _audioSource.loop = false;
#endif
    }
}
