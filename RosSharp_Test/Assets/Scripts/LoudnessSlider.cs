using RosSharp.RosBridgeClient;
using UnityEngine;
using UnityEngine.UI;

public class LoudnessSlider : AudioProvider
{
    readonly Slider[] sliders = new Slider[4];
    private bool IsCreated = false;

    private void Create()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i] = GameObject.FindGameObjectWithTag(ResourcePathManager.sliderTag + i.ToString()).GetComponent<Slider>();
            sliders[i].maxValue = loudnessMax;
            sliders[i].minValue = loudnessMin;
            sliders[i].normalizedValue = 0;
            sliders[i].onValueChanged.AddListener(delegate { SetMessage(); });

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (IsCreated == false)
        {
            Create();
            IsCreated = true;
        }
        audioRecording.data = new double[4]{ loudnessMin, loudnessMin, loudnessMin, loudnessMin};
        audioRecording.layout.dim = new RosSharp.RosBridgeClient.MessageTypes.Std.MultiArrayDimension[1];
        audioRecording.layout.dim[0] = new RosSharp.RosBridgeClient.MessageTypes.Std.MultiArrayDimension
        {
            size = 4
        };
        //Adding audio visualizers
        audioVisualizers = GetComponents<AudioVisualizer>();
       
    }

    protected override void SetMessage()
    {   
        for (int i = 0; i < sliders.Length; i++)
        {
            audioRecording.data[i] = sliders[i].value;
        }
        IsMessageSet = true;
    }

    void Update()
    {
        if (IsMessageSet)
        {
            for (int i = 0; i < audioVisualizers.Length; i++)
            {
                audioVisualizers[i].Visualize(audioRecording);
            }
            IsMessageSet = false;
        }
    }
}
