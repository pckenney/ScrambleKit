using UnityEngine;

namespace ScrambleKit
{
    [System.Serializable]
    public class FXTrigger
    {
        public Transform spawnPrefab;
        public string animState;
        public string soundName;
        public float hitStop;
        public float cameraShake;

        public void Trigger(Transform owner)
        {
            if (hitStop != 0)
                HitStopFX.HitStop(hitStop);

            if(cameraShake != 0)
            {
                CameraFX.Shake(cameraShake);
            }

            if (spawnPrefab)
            {
                Transform t = GameObject.Instantiate(spawnPrefab);
                t.position = owner.position;
            }

            if (animState != "")
            {
                Animator anim = owner.GetComponent<Animator>();
                if (anim != null)
                    anim.Play(animState);
            }

            if(soundName != "")
            {
                SoundFX.PlaySound(soundName);
            }
        }
    }
}
