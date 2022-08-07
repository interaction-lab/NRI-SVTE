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
        public List<List<List<float>>> objects;
        public List<List<string>> links;
        public List<List<float>> room; // [[x0, y0], [x1, y1], ...]
        public string requestType;
        public string sampleType;
    }
}
