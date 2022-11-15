using DataLayer;
using DataLayer.Enums;
using System.ComponentModel;
using System.Net.Http.Json;

ApiClient.BearerToken = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

var response = await ApiClient.GetPuzzle(EChallengeTrack.B, EChallengeDifficulty.Medium);
var sentenceList = await response.Content!.ReadFromJsonAsync<List<string>>();

int minLength = sentenceList.Min(s => s.Length);
string shorttest = sentenceList.FirstOrDefault(x => x.Length == minLength);

string result = "";
for (int i = 0; i < minLength; i++)
{
    string allChars = "";
    for (int j = 0; j < sentenceList.Count; j++)
    {
        allChars += sentenceList[j][i];
    }

    if (allChars.Distinct().Count() == 1)
    {
        result += allChars[0];
    }
}

await ApiClient.PostPuzzle(EChallengeTrack.B, EChallengeDifficulty.Medium, result.Trim());

