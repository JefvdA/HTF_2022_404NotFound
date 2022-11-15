using System.Net.Http.Json;
using A2_BattleSimulator;
using DataLayer;
using DataLayer.Enums;

const string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

ApiClient.BearerToken = token;

var result = await ApiClient.GetPuzzle(EChallengeTrack.A, EChallengeDifficulty.Medium);
var battle = await result.Content!.ReadFromJsonAsync<Battle>();

while(battle!.teamA.Count > 0 && battle!.teamB.Count > 0)
{
    var player1 = battle!.teamA[0];
    var player2 = battle!.teamB[0];

    var currentPlayer = player1;
    if (player2.speed > player1.speed)
        currentPlayer = player2;
    
    while (player1.health > 0 && player2.health > 0)
    {
        var otherPlayer = player2;
        if (currentPlayer == player2)
            otherPlayer = player1;

        otherPlayer.health -= currentPlayer.strength;
        
        if (otherPlayer.health <= 0)
            break;
        
        currentPlayer = otherPlayer;
    }
    
    if (player1.health <= 0)
        battle!.teamA.Remove(player1);
    if (player2.health <= 0)
        battle!.teamB.Remove(player2);
}

var winner = battle!.teamA.Count > 0 ? "TeamA" : "TeamB";
Console.WriteLine($"The winner is: {winner}");

await ApiClient.PostPuzzle(EChallengeTrack.A, EChallengeDifficulty.Medium, winner);