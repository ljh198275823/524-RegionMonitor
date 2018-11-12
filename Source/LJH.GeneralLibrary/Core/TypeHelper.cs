using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace LJH.GeneralLibrary.Core
{
    public static class TypeHelper
    {
        public static Type GetTypeOfName(string assembly, string name)
        {
            Type ret = null;
            try
            {
                Assembly asm = Assembly.Load(assembly);
                if (asm != null)
                {
                    foreach (Type t in asm.GetTypes())
                    {
                        if (string.Compare(t.Name, name, true) == 0)
                        {
                            return t;
                        }
                    }
                }
            }
            catch
            {
            }
            return ret;
        }
    }
}
