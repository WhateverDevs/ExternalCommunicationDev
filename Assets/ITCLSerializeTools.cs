using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct FloatInt
{
    [FieldOffset(0)] public float float_value;
    [FieldOffset(0)] public Int32 int_value;
}

[StructLayout(LayoutKind.Explicit)]
public struct DoubleLong
{
    [FieldOffset(0)] public double double_value;
    [FieldOffset(0)] public long long_value;
}

public static class ITCLFlagsHelper
{
    public static bool IsSet(short flags, short flag)
    {
        int flagsValue = (int)flags;
        int flagValue = (int)flag;

        return (flagsValue & flagValue) != 0;
    }

    public static void Set(ref short flags, short flag)
    {
        int flagsValue = (int)flags;
        int flagValue = (int)flag;

        flags = (short)(flagsValue | flagValue);
    }

    public static void Unset(ref short flags, short flag)
    {
        int flagsValue = (int)flags;
        int flagValue = (int)flag;

        flags = (short)(flagsValue & (~flagValue));
    }
}

public static class ITCLSerializeTools
{
    static byte[] tempArrayInt = new byte[4];

    static FloatInt nValue = new FloatInt();
    static byte[] intdata = new byte[4];
    static byte[] int16data = new byte[2];
    static Vector3 vectorData = new Vector3();
    static Quaternion quaternionData = new Quaternion();

    public static bool serializeByte(byte value, ref MemoryStream stream)
    {        
        stream.WriteByte(value);
        return true;
    }

    public static bool deserializeByte(out byte value, ref MemoryStream stream)
    {        
        value = (byte)stream.ReadByte();
        return true;
    }
    public static bool serializeBool(bool value, ref MemoryStream stream)
    {
        byte boolValue = (value) ? (byte)1 : (byte)0;
        stream.WriteByte(boolValue);
        return true;
    }

    public static bool deserializeBool(out bool value, ref MemoryStream stream)
    {
        byte boolData = (byte)stream.ReadByte();
        value = (boolData == 0) ? false : true;        
        return true;
    }
    public static bool serializeInt(int value, ref MemoryStream stream)
    {
        tempArrayInt = BitConverter.GetBytes(value);
        stream.Write(tempArrayInt, 0, 4);
        return true;
    }

    public static bool deserializeInt(out int value, ref MemoryStream stream)
    {
        stream.Read(intdata, 0, 4);
        value = BitConverter.ToInt32(intdata, 0);
        return true;
    }

    public static bool serializeUInt(uint value, ref MemoryStream stream)
    {
        stream.Write(BitConverter.GetBytes(value), 0, 4);
        return true;
    }

    public static bool deserializeUInt(out uint value, ref MemoryStream stream)
    {
        stream.Read(intdata, 0, 4);
        value = BitConverter.ToUInt32(intdata, 0);
        return true;
    }

    public static bool serializeInt16(Int16 value, ref MemoryStream stream)
    {        
        stream.Write(BitConverter.GetBytes(value), 0, 2);
        return true;
    }

    public static bool deserializeInt16(out Int16 value, ref MemoryStream stream)
    {        
        stream.Read(int16data, 0, 2);
        value = BitConverter.ToInt16(int16data, 0);
        return true;
    }

    public static bool serializeUInt16(UInt16 value, ref MemoryStream stream)
    {
        stream.Write(BitConverter.GetBytes(value), 0, 2);
        return true;
    }

    public static bool deserializeUInt16(out UInt16 value, ref MemoryStream stream)
    {
        stream.Read(int16data, 0, 2);
        value = BitConverter.ToUInt16(int16data, 0);
        return true;
    }
    public static bool serializeFloat(float value,ref MemoryStream stream)
    {
        nValue.float_value = value;
        stream.Write(BitConverter.GetBytes(nValue.int_value), 0, 4);
        return true;
    }

    public static bool deserializeFloat(out float value, ref MemoryStream stream)
    {
        stream.Read(intdata, 0, 4);
        nValue.int_value = BitConverter.ToInt32(intdata,0);
        value = nValue.float_value;
        return true;
    }

    public static bool serializeVector(Vector3 vector,ref MemoryStream stream)
    {
        serializeFloat(vector.x,ref stream);
        serializeFloat(vector.y,ref stream);
        serializeFloat(vector.z,ref stream);
        return true;
    }

    public static bool deserializeVector(out Vector3 vector, ref MemoryStream stream)
    {
        
        deserializeFloat(out vector.x, ref stream);
        deserializeFloat(out vector.y, ref stream);
        deserializeFloat(out vector.z, ref stream);
        return true;
    }

    public static bool serializeQuaternion(Quaternion quat, ref MemoryStream stream)
    {
        serializeVector(quat.eulerAngles, ref stream);
        return true;
    }

    public static bool deserializeQuaternion(out Quaternion quat, ref MemoryStream stream)
    {
        deserializeVector(out vectorData, ref stream);
        quaternionData.eulerAngles = vectorData;
        quat = quaternionData;
        return true;
    }

    public static bool serializeRigidBody(Rigidbody rb, ref MemoryStream stream)
    {
        serializeVector(rb.position, ref stream);
        serializeQuaternion(rb.rotation, ref stream);
        serializeVector(rb.velocity, ref stream);
        serializeVector(rb.angularVelocity, ref stream);
        return true;
    }

    public static bool deserializeRigidBody(ref Rigidbody rb, ref MemoryStream stream)
    {  
        deserializeVector(out vectorData, ref stream);
        rb.position = vectorData;
        deserializeQuaternion(out quaternionData, ref stream);
        rb.rotation = quaternionData;
        deserializeVector(out vectorData, ref stream);
        rb.velocity = vectorData;
        deserializeVector(out vectorData, ref stream);
        rb.angularVelocity = vectorData;
        return true;
    }

    public static bool serializeTransform(Transform t, ref MemoryStream stream)
    {
        serializeVector(t.position, ref stream);
        serializeQuaternion(t.rotation, ref stream);
        return true;
    }

    public static bool deserializeTransform(ref Transform t, ref MemoryStream stream)
    {
        deserializeVector(out vectorData, ref stream);
        t.position = vectorData;
        deserializeQuaternion(out quaternionData, ref stream);
        t.rotation = quaternionData;
        return true;
    }

    public static bool serializeTransformList(List<Transform> lt, ref MemoryStream stream)
    {
        stream.Write(BitConverter.GetBytes((Int16)lt.Count),0, 2);
        for(int i =0; i< lt.Count; i++)
        {
            serializeTransform(lt[i], ref stream);
        }
        return true;
    }

    public static bool deserializeTransformList(ref List<Transform> lt, ref MemoryStream stream)
    {
        Int16 size = 0;
        deserializeInt16(out size, ref stream);
        if(size != lt.Count)
        {
            return false;
        }
        for (int i = 0; i < size; i++)
        {
            Transform t = lt[i];
            deserializeTransform(ref t, ref stream);
            lt[i] = t;
        }
        return true;
    }

    public static bool serializeString(string str, ref MemoryStream stream)
    {
        byte[] bytes = new byte[str.Length * sizeof(char)];
        Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        stream.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
        stream.Write(bytes, 0, bytes.Length);
        return true;
    }

    public static bool deserializeString(out string str,out int size, ref MemoryStream stream)
    {
        stream.Read(intdata, 0, 4);
        size = BitConverter.ToInt32(intdata, 0);
        byte[] strdata = new byte[size];
        stream.Read(strdata, 0, size);
        char[] chars = new char[size / sizeof(char)];
        Buffer.BlockCopy(strdata, 0, chars, 0, size);
        str = new string(chars);
        return true;
    }
}
