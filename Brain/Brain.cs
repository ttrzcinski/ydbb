

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lycopersicon_src.Brain
{
    /// <summary>
    /// Works as a hub for all brain's functions and entry points.
    /// </summary>
    public class Brain // : Microsoft.Bot.Builder.Core.Extensions.IStorage
    {
        private BrocasArea talking;

        public BrodmannArea memory;

        public string respond(string phrase)
        {
            // Check entered phrase
            if (string.IsNullOrEmpty(phrase)) {
                return "So.. silent..";
            }
            // Make it lowercase
            var question = phrase.ToLower();
            //Process given question
            var analysis = talking.process(question);
            //Go through known responses
            var response = "...";
            switch (phrase)
            {
                case "hello":
                    if (memory.Exists("greeting","1"))
                    {
                        response = "We've already greeted Today.";
                    }
                    else
                    {
                        response = "Hello!";
                        memory.Put("greeting", "1");
                    }

                    break;

                case "bye":
                    if (memory.Exists("greeting", "0"))
                    {
                        response = "Bye!";
                        memory.Put("greeting", "0");
                    }
                    else
                    {
                        response = "Bye without Hello first?!";
                    }

                    break;

                default:
                    var resp = memory.Read("TurnCount") ?? "undefined count";
                    response = $"Turn {resp}: You sent '{phrase}'";
                    break;
            }
            return response;
        }

        /*public Task<IEnumerable<KeyValuePair<string, object>>> Read(params string[] keys)
        {
            var successCount = 0;
            var result = new Dictionary<string, object>();
            foreach (var key in keys) {
                result.Add(key, memory.Read(key));
                if (successCount +1 == result.Count) {
                    successCount++;
                }
            }
            return successCount == keys.Length 
                ? Task.FromResult((IEnumerable<KeyValuePair<string, object>>)result)
                : Task.FromResult((IEnumerable<KeyValuePair<string, object>>)null);
        }

        public Task Write(IEnumerable<KeyValuePair<string, object>> changes)
        {
            var successCount = 0;
            foreach (var change in changes) {
                if (memory.Put(change.Key, change.Value.ToString()))
                {
                    successCount++;
                }
            }
            return successCount == changes.Count() 
                ? Task.CompletedTask
                : Task.FromException(new KeyNotFoundException("Not all keys were found."));
        }

        public Task Delete(params string[] keys)
        {
            var successCount = 0;
            foreach (var key in keys) {
                if (memory.Remove(key))
                {
                    successCount++;
                }
            }
            return successCount == keys.Length 
                ? Task.CompletedTask
                : Task.FromException(new KeyNotFoundException("Not all keys were found."));
        }*/
    }
}