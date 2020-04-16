using UnityEngine;

public class InputSample : MonoBehaviour
{
    private BitBrainSampleManager sampleManager;

    private void Start()
    {
        sampleManager = new BitBrainSampleManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            sampleManager.CheckProcess();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            sampleManager.SetupThreads();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            sampleManager.SendFirstMessage();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            sampleManager.SendLastMessage();
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            sampleManager.CloseThread();
        }
    }
}
