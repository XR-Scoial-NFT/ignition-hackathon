﻿using UnityEngine;
using UnityEngine.Audio;

namespace UtilityCode.CodeLibrary.Extensions.ExtensionsInSaperateFiles
{
    public static class AudioClipExtensions
    {
        public static void Play(this AudioClip audioClip)
        {
            audioClip.PlayClipAtPoint(Vector3.zero, 1.0f, 1.0f, 0.0f);
        }

        public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position)
        {
            audioClip.PlayClipAtPoint(position, 1.0f, 1.0f, 0.0f);
        }

        public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position, float volume)
        {
            audioClip.PlayClipAtPoint(position, volume, 1.0f, 0.0f);
        }

        public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position, AudioMixerGroup mixerGroup)
        {
            audioClip.PlayClipAtPoint(position, 1f, 1.0f, 0.0f, mixerGroup);
        }

        public static void PlayClipAtPointWithMixer(this AudioClip clip, Vector3 playAtLocation,
            AudioMixerGroup mixerGroup)
        {
            clip.PlayClipAtPoint(playAtLocation, mixerGroup);
        }

        public static void PlayClipAtPoint(this AudioClip audioClip, Vector3 position, float volume, float pitch,
            float pan, AudioMixerGroup mixerGroup = null)
        {
            float originalTimeScale = Time.timeScale;
            Time.timeScale = 1.0f; // ensure that all audio plays

            GameObject go = new GameObject("One-shot audio");
            AudioSource goSource = go.AddComponent<AudioSource>();

            if (mixerGroup != null)
            {
                goSource.outputAudioMixerGroup = mixerGroup;
            }

            goSource.clip = audioClip;
            go.transform.position = position;
            goSource.volume = volume;
            goSource.pitch = pitch;
            goSource.panStereo = pan;

            goSource.Play();
            Object.Destroy(go, audioClip.length);

            Time.timeScale = originalTimeScale;
        }
    }
}