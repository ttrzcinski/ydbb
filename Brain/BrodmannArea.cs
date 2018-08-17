using System.Collections.Generic;

namespace Lycopersicon_src.Brain
{
    /// <summary>
    /// Represents part of brain responsible for memory.
    /// </summary>
    public class BrodmannArea
    {
        //public int TurnCount { get; set; } = 0;

        //public bool greeting { get; set; }

        //public bool Lycopersicon { get; set; }

        private Dictionary<string, string> facts;

        public BrodmannArea()
        {
            initFactsIfNeeded();
        }

        /// <summary>
        /// Initialize facts, if didn't before and adds base with basic facts.
        /// </summary>
        private void initFactsIfNeeded()
        {
            if (facts == null)
            {
                facts = new Dictionary<string, string>();
                facts.Add("name", "Lycopersicon");
                facts.Add("DOB", System.DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                facts.Add("POB", System.Environment.MachineName);
                facts.Add("turncount", "0");
                facts.Add("greeting", "0");
                facts.Add("Lycopersicon", "0");
            }
        }

        /// <summary>
        /// Adds (or updates) fact in memory.
        /// </summary>
        /// <param name="fact">fact's name</param>
        /// <param name="value">fact's value</param>
        /// <returns>true, if is in memory after, false otherwise</returns>
        public bool Put(string fact, string value)
        {
            // check entered params
            if (string.IsNullOrWhiteSpace(fact) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            fact = fact.ToLower();
            value = value.ToLower();
            // Initialize facts base with basic facts
            initFactsIfNeeded();
            // Add fact to memory
            facts.Add(fact, value);
            // Check, if fact is remembered
            return facts.ContainsKey(fact);
        }

        /// <summary>
        /// Reads fact from memory.
        /// </summary>
        /// <param name="fact">fact's name</param>
        /// <returns>value, if exists, null otherwise</returns>
        public string Read(string fact)
        {
            // check entered params
            if (string.IsNullOrWhiteSpace(fact))
            {
                return null;
            }
            fact = fact.ToLower();
            return facts.ContainsKey(fact) ? facts[fact] : null;
        }

        public bool Exists(string fact, string value)
        {
            // check entered params
            if (string.IsNullOrWhiteSpace(fact) || string.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            fact = fact.ToLower();
            value = value.ToLower();

            return facts.ContainsKey(fact) ? facts[fact].Equals(value) : false;
        }

        public bool Change(string fact, string operation)
        {
            // check entered params
            if (string.IsNullOrWhiteSpace(fact) || string.IsNullOrWhiteSpace(operation))
            {
                return false;
            }
            fact = fact.ToLower();
            operation = operation.ToLower();

            switch(operation) {
                case "++":
                    var value = Read(fact);
                    if (value != null) {
                        if (int.TryParse(value, out int n)) {
                            n++;
                            Put(fact, n.ToString());
                            return true;
                        }
                    }
                    break;
                case "--":
                    var value2 = Read(fact);
                    if (value2 != null) {
                        if (int.TryParse(value2, out int n)) {
                            n--;
                            Put(fact, n.ToString());
                            return true;
                        }
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Removes pointed fact, if exists.
        /// </summary>
        /// <param name="fact">pointed fact's name</param>
        /// <returns>true, if removed, false otherwise</returns>
        public bool Remove(string fact)
        {
            // check entered params
            if (string.IsNullOrWhiteSpace(fact))
            {
                return false;
            }
            fact = fact.ToLower();
            return facts.ContainsKey(fact) ? facts.Remove(fact) : false;
        }
    }
}