using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DiscordRPC.IO
{
    public struct PipeFrame : IEquatable<PipeFrame>
    {
        public Opcode Opcode { get; set; }

        public uint Length
        {
            get
            {
                return (uint)this.Data.Length;
            }
        }

        public byte[] Data { get; set; }

        public string Message
        {
            get
            {
                return this.GetMessage();
            }
            set
            {
                this.SetMessage(value);
            }
        }

        public PipeFrame(Opcode opcode, object data)
        {
            this.Opcode = opcode;
            this.Data = null;
            this.SetObject(data);
        }

        public Encoding MessageEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        private void SetMessage(string str)
        {
            this.Data = this.MessageEncoding.GetBytes(str);
        }

        private string GetMessage()
        {
            return this.MessageEncoding.GetString(this.Data);
        }

        public void SetObject(object obj)
        {
            string message = JsonConvert.SerializeObject(obj);
            this.SetMessage(message);
        }

        public void SetObject(Opcode opcode, object obj)
        {
            this.Opcode = opcode;
            this.SetObject(obj);
        }

        public T GetObject<T>()
        {
            string message = this.GetMessage();
            return JsonConvert.DeserializeObject<T>(message);
        }

        public bool ReadStream(Stream stream)
        {
            uint opcode;
            bool flag = !this.TryReadUInt32(stream, out opcode);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                uint num;
                bool flag2 = !this.TryReadUInt32(stream, out num);
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    uint num2 = num;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        uint num3 = (uint)this.Min(2048, num);
                        byte[] array = new byte[num3];
                        int count;
                        while ((count = stream.Read(array, 0, this.Min(array.Length, num2))) > 0)
                        {
                            num2 -= num3;
                            memoryStream.Write(array, 0, count);
                        }
                        byte[] array2 = memoryStream.ToArray();
                        bool flag3 = (long)array2.Length != (long)((ulong)num);
                        if (flag3)
                        {
                            result = false;
                        }
                        else
                        {
                            this.Opcode = (Opcode)opcode;
                            this.Data = array2;
                            result = true;
                        }
                    }
                }
            }
            return result;
        }

        private int Min(int a, uint b)
        {
            bool flag = (ulong)b >= (ulong)((long)a);
            int result;
            if (flag)
            {
                result = a;
            }
            else
            {
                result = (int)b;
            }
            return result;
        }

        private bool TryReadUInt32(Stream stream, out uint value)
        {
            byte[] array = new byte[4];
            int num = stream.Read(array, 0, array.Length);
            bool flag = num != 4;
            bool result;
            if (flag)
            {
                value = 0U;
                result = false;
            }
            else
            {
                value = BitConverter.ToUInt32(array, 0);
                result = true;
            }
            return result;
        }

        public void WriteStream(Stream stream)
        {
            byte[] bytes = BitConverter.GetBytes((uint)this.Opcode);
            byte[] bytes2 = BitConverter.GetBytes(this.Length);
            byte[] array = new byte[bytes.Length + bytes2.Length + this.Data.Length];
            bytes.CopyTo(array, 0);
            bytes2.CopyTo(array, bytes.Length);
            this.Data.CopyTo(array, bytes.Length + bytes2.Length);
            stream.Write(array, 0, array.Length);
        }

        public bool Equals(PipeFrame other)
        {
            return this.Opcode == other.Opcode && this.Length == other.Length && this.Data == other.Data;
        }

        public static readonly int MAX_SIZE = 16384;
    }
}
