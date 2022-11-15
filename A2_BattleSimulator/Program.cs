using System.Net.Http.Json;
using A2_BattleSimulator;
using DataLayer;
using DataLayer.Enums;

const string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

ApiClient.BearerToken = token;

var result = await ApiClient.GetSample(EChallengeTrack.A, EChallengeDifficulty.Medium);
var battle = await result.Content!.ReadFromJsonAsync<Battle>();

