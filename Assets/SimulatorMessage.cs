using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using WhateverDevs.Core.Runtime.Serialization;

[Serializable]
public class BLOCK
{
    public long TIMESTAMP { get; set; }
    public List<int> VALUES { get; set; }
}
[Serializable]
public class DATA
{
    public List<BLOCK> BLOCKS { get; set; }
    public string ID { get; set; }
}
[Serializable]
public class RootObject
{
    public DATA DATA { get; set; }

    public RootObject()
    {
        DATA = new DATA();
        DATA.BLOCKS = new List<BLOCK>();
        BLOCK temp = new BLOCK();
        temp.TIMESTAMP = Stopwatch.GetTimestamp();
        temp.VALUES = new List<int>();
        DATA.BLOCKS.Add(temp);
    }
}

[System.Serializable]
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
            _info = serializer.From<RootObject>(allJsons[i]);//todo change
        }
    }
    
    public byte[] ToByteArray()
    {
        string resultString = serializer.To(_info);
        //protocol_end
        byte[] bArray = Encoding.UTF8.GetBytes(resultString);
        return bArray;
    }
}