using UnityEngine;

namespace ScrambleKit
{
    public class SpawnFromLibrary : MonoBehaviour
    {
        public SpawnLibrary Library;
        public int Level;
        public Vector2 PositionVariance = Vector2.zero;

        void Start()
        {
            Transform prefab = Library.PickPrefab(Level);
            if (prefab == null)
                return;

            Transform spawn = Instantiate(prefab);
            Vector2 offset = new Vector2(PositionVariance.x * Random.Range(-1f, 1f), PositionVariance.y * Random.Range(-1f, 1f));
            spawn.position = transform.position + (Vector3)offset;
        }
    }
}
