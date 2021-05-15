using UnityEngine;

namespace ScrambleKit
{
    public class IntervalPlacement : MonoBehaviour
    {
        public Transform prefab;

        public int xCount=1;
        public int yCount=1;
        public int zCount=1;

        public float xInterval;
        public float yInterval;
        public float zInterval;

        void Start()
        {
            for (int x = 0; x < xCount; x++)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int z = 0; z < zCount; z++)
                    {
                        Vector3 position = transform.position + new Vector3(x * xInterval, y * yInterval, z * zInterval);
                        PlaceOneAt(position);
                    }
                }
            }
        }

        private void PlaceOneAt(Vector3 pos)
        {
            Transform t = Instantiate(prefab);
            t.position = pos;
        }
    }
}
