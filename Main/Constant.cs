using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMod
{
    public class BsonEventArgs : EventArgs
    {
        public Il2CppKernys.Bson.BSONObject BSON { get; set; }

        public BsonEventArgs(Il2CppKernys.Bson.BSONObject bSONObject)
        {
            this.BSON = bSONObject;
        }
    }
}