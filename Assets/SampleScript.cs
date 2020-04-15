using System;
using System.Diagnostics;
using UnityEngine;
using WhateverDevs.Core.Runtime.Common;
using Debug = UnityEngine.Debug;
    
public class SampleScript : LoggableMonoBehaviour<SampleScript>
{
    #region Should be configuration
    private string ProcessName = "bbtControllerDevd";
    private string ipAddress = "127.0.0.1";
    private int port = 5555;
    
    #endregion
    
    public static long timestamp = 0;
    
    private CommunicationThread communicationThread;
    private volatile bool dataReceived = false;
    private SimulatorMessage msgSimulatorMessage;
    
    public bool threadReady = false;

    private void Start()
    {
        ///todo should be a corutine
        Process[] processes = Process.GetProcessesByName(ProcessName);
        Process process;
        
        
        if (processes.Length != 0)
        {
            process = processes[0];
            Debug.Log("PROCESO ENCONTRADO " + process.MachineName);
        }
        else
        {
            Debug.Log("NO HAY NADA");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetupThreads();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            SendFirstMessage();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SendLastMessage();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            CloseThread();
        }
    }

    private void SetupThreads()
    {
        try
        {
            if (!threadReady)
            {
                communicationThread = new CommunicationThread(ipAddress,port);
                threadReady = communicationThread.Connected;
                communicationThread.Start();
            }
        }
        catch (Exception e)
        {
            Debug.Log("Socket error:" + e);
            //CloseThread();
        }
    }

    private void SendFirstMessage()
    {
        try
        {
            if (threadReady)
            {
                //GetLogger().Error("prueba");
                msgSimulatorMessage = new SimulatorMessage();
                msgSimulatorMessage._info.DATA.ID = "protocolo";
                //GetLogger().Error(msgSimulatorMessage._info.DATA.ID + "time " + msgSimulatorMessage._info.DATA.BLOCKS[0].TIMESTAMP);
                communicationThread.Send(msgSimulatorMessage.ToByteArray());
            }

        }
        catch (Exception e)
        {
            Debug.Log("SendFirstMessage error :" + e);
        }
    }
            
    private void SendLastMessage()
    {
        try
        {
            if (threadReady)
            {
                msgSimulatorMessage = new SimulatorMessage();
                msgSimulatorMessage._info.DATA.ID = "protocol_end";
                communicationThread.Send(msgSimulatorMessage.ToByteArray());
            }
        }
        catch (Exception e)
        {
            Debug.Log("Last message error :" + e);
        }
    }

    
    private void CloseThread()
    {
        Debug.LogWarning("Closing thread");
        threadReady = false;
        communicationThread.EndThread = true;
    }
}
