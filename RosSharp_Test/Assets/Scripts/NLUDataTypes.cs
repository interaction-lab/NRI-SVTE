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

        public override string ToString()
        {
            return "<u>" + Name + "</u>"+ " with confidence: " + Confidence.ToString();
        }
    }
}