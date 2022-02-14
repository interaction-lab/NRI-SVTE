using System.Collections;

namespace NLUDataTypes
{
    [System.Serializable]
    public class NLUDialogue
    {
        public NLUMessage[] Messages { get; set; }
    }

    [System.Serializable]
    public class NLUMessage
    {
        public int Timestamp { get; set; }
        public NLUData Message { get; set; }
    }

    [System.Serializable]
    public class NLUData
    {
        public string Text { get; set; }
        public NLUIntent[] Intents { get; set; }
        public int NLUState { get; set; }
    }

    [System.Serializable]
    public class NLUIntent
    {
        public string Name { get; set; }
        public float Confidence { get; set; }

        public string[] Keywords { get; set; }

        public override string ToString()
        {
            return "<u>" + Name + "</u>"+ " with confidence: " + Confidence.ToString();
        }
    }

    public class NLUIntentComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            float difference = ((NLUIntent)x).Confidence - ((NLUIntent)y).Confidence;
            if(difference < 0)
                return +1;
            else if(difference > 0)
                return -1;
            else 
                return 0;
        }
    }
}