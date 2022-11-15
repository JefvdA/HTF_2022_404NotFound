using System.Net.Http.Json;
using DataLayer;
using DataLayer.Enums;

ApiClient.BearerToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

var response = await ApiClient.GetPuzzle(EChallengeTrack.B, EChallengeDifficulty.Easy);
var wordList = await response.Content!.ReadFromJsonAsync<List<string>>();

string answer = "";

for (int w = 0; w < wordList!.Count; w++)
{
    // Create unique character array
    char[] unique = new char[wordList[w].Length];

    // SortedDictionary to store the count of each character
    SortedDictionary<int, string> result = new SortedDictionary<int, string>();
    
    int counted = 0;
    // Loop through each character in the word
    for (int i = 0; i < wordList[w].Length; i++)
    {
        bool alreadyCounted = false;
        
        // Loop through each character in counted word
        for (int j = 0; j < counted; j++)
        {
            // If the character is already in the unique array, set alreadyCounted to true
            if (wordList[w][i] == unique[j])
                alreadyCounted = true;
        }

        // If character is counted, skip
        if (alreadyCounted)
            continue;

        int count = 0;
        // Loop through each character in the word, and count the number of times it appears
        for (int j = 0; j < wordList[w].Length; j++)
        {
            if (wordList[w][i] == wordList[w][j])
                count++;
        }

        // Add the character and its count to the result dictionary
        result.Add(count, wordList[w][i].ToString());

        // Add the character to the unique array
        unique[counted] = wordList[w][i];

        // Increment the number of counted characters
        counted++;
    }

    // Add each character to the answer string
    foreach (var item in result)
    {
        answer += item.Value;
    }
    answer += " ";
}

await ApiClient.PostPuzzle(EChallengeTrack.B, EChallengeDifficulty.Easy, answer.Trim());