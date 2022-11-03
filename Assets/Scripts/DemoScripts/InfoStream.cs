using System.Collections;
using System.Collections.Generic;

public class InfoStream {

    private List<byte> _stream;

    public InfoStream() {
        _stream = new List<byte>();
    }

    public InfoStream(byte[] source) {

        _stream = new List<byte>();
        _stream.AddRange(source);
    }

    public void addString(string s) {
        byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
        addByteArray(b);
    }

    public string getString() {

        byte[] b = getByteArray();
        string s = System.Text.Encoding.UTF8.GetString(b);
        return s;
    }

    public void addByteArray(byte[] b) {
        int len = b.Length;
        addInteger(len);
        for(int i = 0; i < len; i++) {
            addByte(b[i]);
        }
    }

    public byte[] getByteArray() {
        int len = getInteger();
        byte[] b = new byte[len];
        for(int i = 0; i < len; i++) {
            b[i] = getByte();
        }
        return b;
    }

    public void addInteger(int n) {
        byte[] b = System.BitConverter.GetBytes(n);
        for(int i = 0; i < 4; i++) {
            addByte(b[i]);
        }
    }

    public int getInteger() {
        byte[] b = new byte[4];
        for(int i = 0; i < 4; i++) {
            b[i] = getByte();
        }
        int n = System.BitConverter.ToInt32(b);
        return n;
    }

    public void addByte(byte b) {
        _stream.Add(b);
    }

    public byte getByte() {
        byte b = _stream[0];
        _stream.RemoveAt(0);
        return b;
    }

    public byte[] getStream() {
        return _stream.ToArray();
    }

}
