using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMVC.Extension
{
    public static class SessionHelper
    {
        public static async Task SetObjectAsJsonAsync(this ISession session, string key, object value)
        {
            if (!session.IsAvailable)
                await session.LoadAsync();
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static async Task<T> GetObjectFromJsonAsync<T>(this ISession session, string key)
        {
            if (!session.IsAvailable)
            {
                await session.LoadAsync();
            }
            var value = session.GetString(key);

            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
