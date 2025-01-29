namespace GlobalGameJam
{
    /// <summary>
    /// Represents a score entry with various metrics such as earnings, deductions, potion count, litter count, and overtime.
    /// </summary>
    [System.Serializable]
    public struct ScoreEntry
    {
        /// <summary>
        /// The name of the group associated with this score entry.
        /// </summary>
        public string GroupName;

        /// <summary>
        /// The total earnings for this score entry.
        /// </summary>
        public int Earnings;

        /// <summary>
        /// The total deductions for this score entry.
        /// </summary>
        public int Deductions;

        /// <summary>
        /// The count of potions for this score entry.
        /// </summary>
        public int PotionCount;

        /// <summary>
        /// The count of litter for this score entry.
        /// </summary>
        public int LitterCount;
        
        /// <summary>
        /// The amount of overtime for this score entry.
        /// </summary>
        public float Overtime;
    }
}