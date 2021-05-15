using UnityEngine;

namespace ScrambleKit
{
    public class CameraFX : MonoBehaviour
    {
        public float traumaReductionRate = 1.5f;
        public float shakeRotation = 5f;
        public float shakeTranslate = 0.5f;
        public float shakeSpeed = 10f;

        // Based on talk from Squirrel Eiserloh https://www.youtube.com/watch?v=tu-Qe66AvtY
        private static float trauma = 0f;  // range [0..1]
        
        public static void Shake(float newTrauma)
        {
            trauma = Mathf.Min(1f, trauma + newTrauma);
        }

        void Update()
		{
			ShakeCameraBasedOnTrauma();
			DecayTrauma();
		}

		private void ShakeCameraBasedOnTrauma()
        {
			float shake = trauma * trauma;

			float rot = shake * shakeRotation * UnitRand(0);
            transform.localEulerAngles = new Vector3(0, 0, rot);

            float xoff = shake * shakeTranslate * UnitRand(0.3f);
            float yoff = shake * shakeTranslate * UnitRand(0.6f);

            transform.localPosition = new Vector3(xoff, yoff, 0);
        }

        private float UnitRand(float channel = 0f)
        {
            float perl = Mathf.PerlinNoise(Time.time * shakeSpeed, channel);  // [0..1]
            return (perl * 2f) - 1f;  // [-1..1]
        }

		private void DecayTrauma()
		{
			trauma = Mathf.Max(0, trauma - Time.deltaTime * traumaReductionRate);
		}
	}
}
