using System.Collections.Generic;
using UnityEngine;

namespace ScrambleKit
{
    [System.Serializable]
    public class SoundEntry
    {
        public string name;
        public List<AudioClip> sounds;
        public bool optional;
        public float pitchVariance;
        public float volume = 1.0f;
    }

    [CreateAssetMenu]
    public class SoundLibrary : ScriptableObject
    {
        public float masterVolume = 1.0f;
        public List<SoundEntry> sounds;
    }
}
