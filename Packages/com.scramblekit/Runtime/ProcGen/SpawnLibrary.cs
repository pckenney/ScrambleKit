using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ScrambleKit
{
    [System.Serializable]
    public class LibraryEntry
    {
        public Transform prefab;
        public int earliestLevel;
        public int lastLevel;
        public float pickWeight;
    }

    [CreateAssetMenu]
    public class SpawnLibrary : ScriptableObject
    {

        public List<LibraryEntry> entries;

        public Transform PickPrefab(int level)
        {
            try
            {
                LibraryEntry entry = entries
                                    .Where(o => level >= o.earliestLevel)
                                    .Where(q => level <= q.lastLevel)
                                    .DrawFromAtRandomByWeight(e => e.pickWeight);

                return entry.prefab;
            }
            catch(System.Exception)
            {
                Debug.LogError("Error: SpawnLibrary " + name + " was unable to find an entry for Level " + level);
                return null;
            }
        }
    }
}
