using System;

namespace Seagull.Interior_04E.Inspector {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class IgnoreInInspectorAttribute : Attribute {
        
    }
}