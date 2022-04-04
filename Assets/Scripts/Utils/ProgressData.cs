using System;
using System.Collections.Generic;

namespace Utils
{
    [Serializable]
    public class ProgressData
    {
        public List<LevelData> levelsProgress = new List<LevelData>()
        {
            new LevelData(0, new bool[] {false, false, false, false, false}),
            new LevelData(1, new bool[] {false, false, false, false, false})
        };
    }
}