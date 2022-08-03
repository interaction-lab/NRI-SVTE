using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NRISVTE {
    [System.Serializable]
    public class PolyLineJSON {
        public string identifier;
        public int score;
        public Dictionary<string, int> robot;
        public List<Dictionary<string, float>> humans;
        public List<Dictionary<string, float>> objects;
        public List<List<string>> links;
        public List<List<int>> room;

    }
}
