using UnityEngine;

namespace ScrambleKit
{
    public class Aimer : MonoBehaviour
    {
		public bool flipFacingLeft = true;

		[HideInInspector]
		public Vector2 aimingAt;

        private Vector3 normScale;
        private Vector3 flipScale;

        void FixedUpdate()
        {
            FaceTowardAim();
            if(flipFacingLeft)
                CheckFlip();
        }

        private void FaceTowardAim()
        {
            float aimangle = ((Vector2)transform.position).AngleTowardPosition(aimingAt);
            transform.eulerAngles = new Vector3(0, 0, aimangle);
        }

        private void CheckFlip()
        {
            if ((transform.localEulerAngles.z+360)%360 > 180)
                transform.localScale = normScale;
            else
                transform.localScale = flipScale;
        }

		void Start()
		{
			normScale = transform.localScale;
			flipScale = new Vector3(-normScale.x, normScale.y, normScale.z);
		}
	}
}
