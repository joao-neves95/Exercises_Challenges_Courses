using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebServer.Services
{
    public class Utils
    {
        public static List<string> GetErrorsFromModelState(ModelStateDictionary modelState)
        {
            List<string> errors = new List<string>();

            foreach (ModelStateEntry value in modelState.Values)
            {
                for (int i = 0; i < value.Errors.Count; i++) 
                {
                    errors.Add(value.Errors[i].ErrorMessage);
                }
            }
            return errors;
        }

        public static T GetObject<T>(Dictionary<string, object> dictionary)
        {
            Type type = typeof(T);
            var obj = Activator.CreateInstance(type);

            foreach (var kv in dictionary)
            {
                object val = null;
                Type valueType = type.GetProperty(kv.Key).PropertyType;
                if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (kv.Value == null || kv.Value is DBNull || kv.Value == DBNull.Value)
                        val = null;
                    else
                        Convert.ChangeType(kv.Value, Nullable.GetUnderlyingType(valueType));
                }
                else
                    val = Convert.ChangeType(kv.Value, valueType);
                type.GetProperty(kv.Key).SetValue(obj, val);
            }
            return (T)obj;
        }
    }
}
