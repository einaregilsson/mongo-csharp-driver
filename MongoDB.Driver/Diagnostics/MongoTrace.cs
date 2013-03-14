using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MongoDB.Bson.IO;
using MongoDB.Bson;

namespace MongoDB.Driver.Diagnostics
{
    public static class MongoTrace
    {
        static readonly TraceSource _traceSource = new TraceSource("MongoDB.Driver");
        static readonly BooleanSwitch _traceSwitch = new BooleanSwitch("MongoDB.Driver", "MongoDB Driver switch");
        static readonly JsonWriterSettings _writerSettings = new JsonWriterSettings();

        static MongoTrace()
        {
            _writerSettings.Indent = true;
            _writerSettings.IndentChars = "    ";
        }

        public static bool Enabled 
        { 
            get
            {
                return _traceSwitch.Enabled;
            }
            set 
            {
                _traceSwitch.Enabled = value;
            }
        }

        internal static void Trace(IMongoTraceable traceableObject) 
        {
            if (!_traceSwitch.Enabled)
            {
                return;
            }

            var traceDoc = traceableObject.GetTraceDocument();
            _traceSource.TraceInformation(traceDoc.ToJson(_writerSettings));
        }
    }
}
