using System;
using System.Collections.Generic;

namespace Utils
{
    [Serializable]
    public class LevelData
    {
        public int index;
        public bool[] isCollected;

        public LevelData(int index, bool[] isCollected)
        {
            this.index = index;
            this.isCollected = isCollected;
        }
    }
}