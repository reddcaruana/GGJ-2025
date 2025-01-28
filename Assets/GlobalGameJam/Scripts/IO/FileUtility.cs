using System;
using System.IO;
using UnityEngine;

namespace GlobalGameJam
{
    public static class FileUtility
    {
        private const string ScoreFileName = "scores.json";
        
        [System.Serializable]
        private class ScoreEntryList
        {
            public ScoreEntry[] Entries;
        }
        
        /// <summary>
        /// Saves an array of ScoreEntry to a JSON file in the application's persistentDataPath folder.
        /// </summary>
        /// <param name="scoreEntries">The array of ScoreEntry to save.</param>
        public static void SaveScores(ScoreEntry[] scoreEntries)
        {
            var json = JsonUtility.ToJson(new ScoreEntryList { Entries = scoreEntries });
            var path = Path.Combine(Application.persistentDataPath, ScoreFileName);
            
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Loads an array of ScoreEntry from a JSON file in the application's persistentDataPath folder.
        /// </summary>
        /// <returns>The array of ScoreEntry loaded from the file.</returns>
        public static ScoreEntry[] LoadScores()
        {
            var path = Path.Combine(Application.persistentDataPath, ScoreFileName);

            if (File.Exists(path) == false)
            {
                return new ScoreEntry[] { };
            }

            var json = File.ReadAllText(path);
            var scoreEntryList = JsonUtility.FromJson<ScoreEntryList>(json);

            return scoreEntryList.Entries;
        }
    }
}