using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace TripCostsManager
{
    public static class Extensions
    {
        private static object GetInstanceField(this Type type, object instance, string fieldName, BindingFlags bindingFlags)
        {
            var fieldInfo = type.GetField(fieldName, bindingFlags);
            return fieldInfo.GetValue(instance);
        }

        public static void ClearContext(this EditContext editContext)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var fieldStates = GetInstanceField(typeof(EditContext), editContext, "_fieldStates", bindingFlags);
            var clearMethodInfo = typeof(HashSet<ValidationMessageStore>).GetMethod("Clear", bindingFlags);

            foreach (DictionaryEntry kv in (IDictionary)fieldStates)
            {
                var messageStores = kv.Value.GetType().GetInstanceField(kv.Value, "_validationMessageStores", bindingFlags);
                clearMethodInfo.Invoke(messageStores, null);
            }
        }

        public static string GetInnerExceptionMessage(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.GetInnerExceptionMessage();

            return ex.Message;
        }
    }
}
