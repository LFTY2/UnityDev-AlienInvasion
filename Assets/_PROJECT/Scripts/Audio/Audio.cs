using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace _PROJECT.Scripts
{
    [Serializable]
    public class Audio
    {
        [FormerlySerializedAs("soundType")] [FormerlySerializedAs("_clipType")] [SerializeField] private AudioClipType clipType;
        [SerializeField] private AudioClip _audioClip;
        public AudioClipType ClipType => clipType;
        public AudioClip AudioClip => _audioClip;
    }
}