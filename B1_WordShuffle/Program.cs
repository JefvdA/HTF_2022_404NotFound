using System.Net.Http.Json;
using DataLayer;
using DataLayer.Enums;

ApiClient.BearerToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

var response = await ApiClient.GetPuzzle(EChallengeTrack.B, EChallengeDifficulty.Easy);
var wordList = await response.Content!.ReadFromJsonAsync<List<string>>();

string answer = "";

for (int w = 0; w < wordList!.Count; w++)
{
    char[] unique = new char[wordList[w].Length];
    SortedDictionary<int, string> result = new SortedDictionary<int, string>();
    
    int counted = 0;
    
    for (int i = 0; i < wordList[w].Length; i++)
    {
        bool alreadyCounted = false;
        for (int j = 0; j < counted; j++)
        {
            if (wordList[w][i] == unique[j])
                alreadyCounted = true;
        }

        if (alreadyCounted)
            continue;

        int count = 0;
        for (int j = 0; j < wordList[w].Length; j++)
        {
            if (wordList[w][i] == wordList[w][j])
                count++;
        }

        result.Add(count, wordList[w][i].ToString());

        unique[counted] = wordList[w][i];
        counted++;
    }

    foreach (var item in result)
    {
        answer += item.Value;
    }
    answer += " ";
}

Console.WriteLine(await ApiClient.PostPuzzle(EChallengeTrack.B, EChallengeDifficulty.Easy, answer.Trim()));