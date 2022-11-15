using System.Net.Http.Json;
using A3_Maze;
using DataLayer;
using DataLayer.Enums;

const string token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMiIsIm5iZiI6MTY2ODUwMzE3NywiZXhwIjoxNjY4NTg5NTc3LCJpYXQiOjE2Njg1MDMxNzd9.k2hY9b8QAfoPkqwhut0O3wIR1TZPNM89ryPW53n6lHq68pdnNBSLBLWu6i-IT5Wjs9NAbsq7L5tWXlvnS663LA";

ApiClient.BearerToken = token;

var result = await ApiClient.GetPuzzle(EChallengeTrack.A, EChallengeDifficulty.Hard);
var currentRoom = await result.Content!.ReadFromJsonAsync<Room>();

var wrongDoors = new Dictionary<int, List<int>>();
var correctDoors = new Dictionary<int, int>();

while (!currentRoom!.finished)
{
    var currentRoomNr = currentRoom.roomNr;
    var door = 0;
    while (wrongDoors.ContainsKey(currentRoomNr) && wrongDoors[currentRoomNr].Contains(door))
    {
        door++;
    }
    
    if (correctDoors.ContainsKey(currentRoomNr))
        door = correctDoors[currentRoomNr];

    Console.WriteLine($"{currentRoomNr} - {door}");

    var response = await ApiClient.PostPuzzle(EChallengeTrack.A, EChallengeDifficulty.Hard, currentRoom.doors[door]);
    currentRoom = await response.Content!.ReadFromJsonAsync<Room>();

    if (currentRoom!.roomNr == 1)
    {
        if (wrongDoors.ContainsKey(currentRoomNr))
        {
            wrongDoors[currentRoomNr].Add(door);
        }
        else
        {
            wrongDoors.Add(currentRoomNr, new List<int> { door });
        }
    } else if (!correctDoors.ContainsKey(currentRoomNr)) {
        correctDoors.Add(currentRoomNr, door);
    }

    if (currentRoom!.finished)
        break;
}

Console.WriteLine("You have finished the maze, and found the artifact!");