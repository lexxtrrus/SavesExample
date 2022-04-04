using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Level/Config", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public Vector3 startPosition;
        public int index;
        public GameObject levelBasePrefab;
        public GameObject levelObstacles;
        public GameObject levelCollectables;
    }
}