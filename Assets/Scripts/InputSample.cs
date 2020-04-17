using UnityEngine;
using WhateverDevs.Core.Runtime.Configuration;
using WhateverDevs.ExternalCommunication.Runtime;
using Zenject;

/// <summary>
///     Sample class to test some bitbrain features
/// </summary>
public class InputSample : MonoBehaviour
{
    private BitBrainSampleManager sampleManager;

    [Inject]
    public void Construct(IConfigurationManager configurationManager)
    {
        sampleManager = new BitBrainSampleManager();
        configurationManager.GetConfiguration(out ExternalCommunicationConfigurationData a);
        sampleManager.SetConfigurationData(a);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) sampleManager.CheckProcess();

        if (Input.GetKeyDown(KeyCode.S)) sampleManager.SetupThreads();

        if (Input.GetKeyDown(KeyCode.D)) sampleManager.SendFirstMessage();

        if (Input.GetKeyDown(KeyCode.F)) sampleManager.SendLastMessage();

        if (Input.GetKeyDown(KeyCode.G)) sampleManager.CloseThread();
    }
}