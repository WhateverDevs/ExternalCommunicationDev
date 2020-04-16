using System;
using Debug = UnityEngine.Debug;
    
public class BitBrainSampleManager : CommunicationManager
{
    public BitBrainSampleManager()
    {
        configurationData = new ExternalCommunicationConfigurationData();
    }
    public override void SendFirstMessage()
    {
        try
        {
            if (threadReady)
            {
                CommunicationMessage msgSimulatorMessage = new CommunicationMessage();
                msgSimulatorMessage._info.DATA.ID = "protocol";
                communicationThread.Send(msgSimulatorMessage.ToByteArray());
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
            if (threadReady)
            {
                CommunicationMessage msgSimulatorMessage = new CommunicationMessage();
                msgSimulatorMessage._info.DATA.ID = "protocol_end";
                communicationThread.Send(msgSimulatorMessage.ToByteArray());
            }
        }
        catch (Exception e)
        {
            GetLogger().Error("Last message error :" + e);
        }
    }
}
