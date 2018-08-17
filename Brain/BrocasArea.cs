namespace Lycopersicon_src.Brain
{
    /// <summary>
    /// Represents part of brain responsible for talking.
    /// </summary>
    public class BrocasArea
    {
        public string respondTo(string phrase) {
            return "Tomato";
        }

        /// <summary>
        /// Processes given phrase in order to obtain meaning for better response.
        /// </summary>
        /// <param name="phrase">recorded phrase</param>
        /// <returns>analysis report</returns>
        public string process(string phrase)
        {
            //Split into lines
            var lines = phrase.Split(".!;?");

            var words = phrase.Split(".!;? /t/r/n");

            return $"Phrase contains {lines.Length} lines and {words.Length} words.";
        }
    }
}