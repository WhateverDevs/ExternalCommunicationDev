using System;
using WhateverDevs.ExternalCommunication.Runtime;

/// <summary>
///     BitBrainSampleManager sample to test features
/// </summary>
public class BitBrainSampleManager : CommunicationManager
{
    public static long Timestamp;

    public void SetConfigurationData(ExternalCommunicationConfigurationData data) => ConfigurationData = data;

    public override void SendFirstMessage()
    {
        try
        {
            if (ThreadReady)
            {
                CommunicationMessage msgSimulatorMessage = new CommunicationMessage();
                msgSimulatorMessage.Info.Data.Id = "protocol";
                CommunicationThread.Send(msgSimulatorMessage.ToByteArray());
            }
        }
        catch (Exception e)
        {
            GetLogger().Error("SendFirstMessage error :" + e);
        }
    }

    public override void SendLastMessage()
    {
        try
        {
            if (ThreadReady)
            {
                CommunicationMessage msgSimulatorMessage = new CommunicationMessage();
                msgSimulatorMessage.Info.Data.Id = "protocol_end";
                CommunicationThread.Send(msgSimulatorMessage.ToByteArray());
            }
        }
        catch (Exception e)
        {
            GetLogger().Error("Last message error :" + e);
        }
    }
}