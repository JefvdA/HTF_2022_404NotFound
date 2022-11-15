using System.Net.Http.Json;
using DataLayer;
using DataLayer.Enums;

const string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

ApiClient.BearerToken = token;

var result = await ApiClient.GetPuzzle(EChallengeTrack.A, EChallengeDifficulty.Easy);
var numeralsList = await result.Content!.ReadFromJsonAsync<List<string>>();

var romanNumerals = new Dictionary<char, int>()
{
    { 'I', 1 },
    { 'V', 5 },
    { 'X', 10 },
    { 'L', 50 },
    { 'C', 100 },
    { 'D', 500 },
    { 'M', 1000 }
};

var sum = numeralsList!.Sum(RomanNumeralToInt);

var solution = ToRoman(sum);

await ApiClient.PostPuzzle(EChallengeTrack.A, EChallengeDifficulty.Easy, solution);

string ToRoman(int number)
{
    return number switch
    {
        >= 1000 => "M" + ToRoman(number - 1000),
        >= 900 => "CM" + ToRoman(number - 900),
        >= 500 => "D" + ToRoman(number - 500),
        >= 400 => "CD" + ToRoman(number - 400),
        >= 100 => "C" + ToRoman(number - 100),
        >= 90 => "XC" + ToRoman(number - 90),
        >= 50 => "L" + ToRoman(number - 50),
        >= 40 => "XL" + ToRoman(number - 40),
        >= 10 => "X" + ToRoman(number - 10),
        >= 9 => "IX" + ToRoman(number - 9),
        >= 5 => "V" + ToRoman(number - 5),
        >= 4 => "IV" + ToRoman(number - 4),
        >= 1 => "I" + ToRoman(number - 1),
        _ => throw new ArgumentOutOfRangeException(nameof(number), number, null)
    };
}

int RomanNumeralToInt(string numeral)
{
    Console.WriteLine(numeral);
    var result = 0;

    for (var i = 0; i < numeral.Length; i++)
    {
        var value = romanNumerals![numeral[i]];

        if (i + 1 < numeral.Length && romanNumerals![numeral[i + 1]] > value)
            result -= value;
        else
            result += value;
    }
    
    return result;
}