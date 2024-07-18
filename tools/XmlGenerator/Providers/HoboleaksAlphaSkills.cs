using EVEMon.XmlGenerator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace EVEMon.XmlGenerator.Providers
{
    // Fetches the max alpha skills from a new location to make it not hard coded!
    internal static class HoboleaksAlphaSkills
    {
        public static async Task<IDictionary<int, int>> GetAlphaSkillLimits()
        {
            //var request = WebRequest.CreateHttp("http://sde.hoboleaks.space/tq/clonestates.json");
            var request = await new HttpClient().GetAsync("http://sde.hoboleaks.space/tq/clonestates.json");
            var result = new Dictionary<int, int>(256);
            IDictionary<int, cloneStates> raw;
            using (var response = request.Content)
            {
                var stream = await response.ReadAsStreamAsync();
                var serializer = new JsonSerializer() { MaxDepth = 4 };
                using (var reader = new JsonTextReader(new StreamReader(stream)))
                {
                    raw = serializer.Deserialize<Dictionary<int, cloneStates>>(reader);
                }
            }
            foreach (var pair in raw)
            {
                foreach (var skillLevel in pair.Value.skills)
                {
                    int id = skillLevel.Key;
                    if (result.TryGetValue(id, out int existing))
                        result[id] = Math.Max(existing, skillLevel.Value);
                    else
                        result.Add(id, skillLevel.Value);
                }
            }
            return result;
        }
    }
}
