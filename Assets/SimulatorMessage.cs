using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using WhateverDevs.Core.Runtime.Serialization;
using Debug = UnityEngine.Debug;

[Serializable]
public class BLOCK
{
    public long TIMESTAMP;
    public int[] VALUES;
}
[Serializable]
public class DATA
{
    public BLOCK[] BLOCKS;
    public string ID;
}
[Serializable]
public class RootObject
{
    public DATA DATA;

    public RootObject()
    {
        DATA = new DATA();
        DATA.BLOCKS = new BLOCK[1];
        BLOCK temp = new BLOCK();
        temp.TIMESTAMP = Stopwatch.GetTimestamp();
        temp.VALUES = new int[1];
        temp.VALUES[0] = 1;
        DATA.BLOCKS[0] = temp;
    }
}

[Serializable]
public class SimulatorMessage
{
    public RootObject _info;
    private JsonSerializer serializer;

    public SimulatorMessage()
    {
        serializer = new JsonSerializer();
        _info = new RootObject();
    }

    
    public SimulatorMessage(byte[] data)
    {
        FromByteArray(data);
    }

    private int amount = 0;
    StringBuilder bufferString = new StringBuilder();
    
    public void FromByteArray(byte[] data)
    { 
        List<string> allJsons = new List<string>();
        string allDataString = System.Text.Encoding.UTF8.GetString(data);

        int index = 0;
        while (index < allDataString.Length - 1)
        {
            for (int i = index; i < allDataString.Length; ++i)
            {
                bufferString.Append(allDataString[i]);
                if (allDataString[i] == '{')
                {
                    ++amount;
                }
                else if (allDataString[i] == '}')
                {
                    --amount;
                    if (amount == 0)
                    {
                        allJsons.Add(bufferString.ToString());
                        bufferString.Clear();
                        index = i;
                        ++index;
                        break;
                    }
                }
                index = i;
            }
        }

        for (int i = 0; i < allJsons.Count; ++i)
        {
            RootObject a = serializer.From<RootObject>(allJsons[i]);//todo change
            _info = a;
            SampleScript.timestamp = a.DATA.BLOCKS[0].TIMESTAMP;
        }
    }

    public byte[] ToByteArray()
    {
        string a = JsonUtility.ToJson(_info);
        string resultString = serializer.To(_info);
        //protocol_end
        Debug.Log("TIMESTAMP" + SampleScript.timestamp);
        byte[] bArray = //Encoding.UTF7.GetBytes("{\"BLOCKS\":[{\"TIMESTAMP\":" + Stopwatch.GetTimestamp().ToString() + "\"VALUES\":[1]}],\"ID\":\"protocolo\"}");//(resultString);
            Encoding.UTF8.GetBytes("{\"DATA\":{\"BLOCKS\":[{\"TIMESTAMP\":" + SampleScript.timestamp + ",\"VALUES\":[1]}],\"ID\":\"protocol\"}}");//(resultString);
        return bArray;
    }
}