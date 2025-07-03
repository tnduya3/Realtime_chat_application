using System;
using System.Collections.Generic;
using System.Linq;

namespace project_cuoi_ky.Utils
{
    public static class FriendSearchHelper
    {
        /// <summary>
        /// Search friends by userName using multiple algorithms
        /// </summary>
        /// <param name="friends">List of friends to search</param>
        /// <param name="query">Search query</param>
        /// <returns>Filtered and sorted list of friends</returns>
        public static List<Models.FriendInfo> SearchFriends(List<Models.FriendInfo> friends, string query)
        {
            if (string.IsNullOrWhiteSpace(query) || friends == null || friends.Count == 0)
            {
                return friends ?? new List<Models.FriendInfo>();
            }

            var searchTerm = query.Trim().ToLower();
            var results = new List<FriendSearchResult>();

            foreach (var friend in friends)
            {
                var score = CalculateSearchScore(friend, searchTerm);
                if (score > 0)
                {
                    results.Add(new FriendSearchResult
                    {
                        Friend = friend,
                        Score = score
                    });
                }
            }

            // Sort by score (higher first), then by userName
            return results
                .OrderByDescending(r => r.Score)
                .ThenBy(r => r.Friend.userName)
                .Select(r => r.Friend)
                .ToList();
        }

        /// <summary>
        /// Calculate search score for a friend based on different matching criteria
        /// </summary>
        private static int CalculateSearchScore(Models.FriendInfo friend, string searchTerm)
        {
            if (friend?.userName == null)
                return 0;

            var userName = friend.userName.ToLower();
            var email = friend.email?.ToLower() ?? "";

            // Exact match (highest priority)
            if (userName == searchTerm)
                return 1000;

            // Starts with (high priority)
            if (userName.StartsWith(searchTerm))
                return 500;

            // Contains (medium priority)
            if (userName.Contains(searchTerm))
                return 250;

            // Email matches (lower priority)
            if (!string.IsNullOrEmpty(email))
            {
                if (email == searchTerm)
                    return 200;
                if (email.StartsWith(searchTerm))
                    return 100;
                if (email.Contains(searchTerm))
                    return 50;
            }

            // Fuzzy matching for common typos (lowest priority)
            if (CalculateLevenshteinDistance(userName, searchTerm) <= 2 && searchTerm.Length > 2)
                return 25;

            return 0;
        }

        /// <summary>
        /// Calculate Levenshtein distance for fuzzy matching
        /// </summary>
        private static int CalculateLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;

            if (string.IsNullOrEmpty(target))
                return source.Length;

            var sourceLength = source.Length;
            var targetLength = target.Length;
            var matrix = new int[sourceLength + 1, targetLength + 1];

            // Initialize first column and row
            for (int i = 0; i <= sourceLength; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= targetLength; j++)
                matrix[0, j] = j;

            // Calculate distances
            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    var cost = source[i - 1] == target[j - 1] ? 0 : 1;
                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[sourceLength, targetLength];
        }

        /// <summary>
        /// Helper class for search results
        /// </summary>
        private class FriendSearchResult
        {
            public Models.FriendInfo Friend { get; set; }
            public int Score { get; set; }
        }
    }
}
