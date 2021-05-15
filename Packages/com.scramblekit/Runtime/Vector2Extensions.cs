using UnityEngine;

namespace ScrambleKit
{
    public static class Vector2Extensions
    {
        public static float AngleDirectionFaces(this Vector2 dir)
        {
            if (dir == Vector2.zero)
                return 0;

            Vector2 v = dir.normalized;
            return Mathf.Atan2(v.x, v.y) * -180 / Mathf.PI;
        }

        public static float AngleTowardPosition(this Vector2 fromPosition, Vector2 towardPosition)
		{
			return (towardPosition - fromPosition).AngleDirectionFaces();
		}

		public static float AngleBetweenDirections(this Vector2 startDir, Vector2 endDir)
		{
			float result = endDir.AngleDirectionFaces() - startDir.AngleDirectionFaces();
			if (result >= 180f)
			{
				result -= 360f;
			}
			else if (result < -180f)
			{
				result += 360f;
			}

			return result;
		}
	}
}
