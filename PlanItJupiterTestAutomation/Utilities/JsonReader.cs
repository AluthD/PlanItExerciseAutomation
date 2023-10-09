using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanItJupiterTestAutomation.Utilities
{
    public class JsonReader
    {
        public JsonReader() { }

        public string extractData(String tokenName)
        {
            String jsonFileString = File.ReadAllText("Settings/appSettings.json");
            var jsonObject = JToken.Parse(jsonFileString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

    }
}
