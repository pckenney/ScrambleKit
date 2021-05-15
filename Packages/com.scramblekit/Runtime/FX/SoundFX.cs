using UnityEngine;
using System.Linq;

namespace ScrambleKit
{
    public class SoundFX : MonoBehaviour
    {
        public static void PlaySound(string name)
        {
            if (instance == null)
            {
                Debug.LogError("You must have an instance of ScrambleKit.SoundFX to play sounds.  Cannot play sound: " + name);
                return;
            }

            if(instance.soundLibrary == null)
            {
                Debug.LogError("Your instance of ScrambleKit.SoundFX has no ScrambleKit.SoundLibrary assigned.  Cannot play sound: " + name);
                return;
            }

            SoundEntry s = instance.soundLibrary.sounds.Where(e => e.name == name).FirstOrDefault();
            if (s == null)
            {
                Debug.LogError("Your ScrambleKit.SoundLibrary does not countain a sound named " + name);
                return;
            }

            if (s.pitchVariance == 0)
            {
                instance.asrc.PlayOneShot(s.sounds.DrawFromAtRandom(), s.volume);
            }
            else
            {
                float pitch = 1f + Random.Range(-s.pitchVariance, s.pitchVariance);
                instance.varysrc.pitch = pitch;
                instance.varysrc.PlayOneShot(s.sounds.DrawFromAtRandom(), s.volume);
            }
        }

        public SoundLibrary soundLibrary;
        public static SoundFX instance;

        private AudioSource asrc;
        private AudioSource varysrc;

        void Start()
        {
            if (instance != null)
                Destroy(instance.gameObject);
            DontDestroyOnLoad(gameObject);

            instance = this;
            asrc = gameObject.AddComponent<AudioSource>();
            varysrc = gameObject.AddComponent<AudioSource>();
        }
    }
}
