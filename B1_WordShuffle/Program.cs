﻿List<string> wordList = new List<string>
{
    "cosuuscsshocsuu",
    "ucsssscucuoopsu",
    "assssapttt"
};

string answer = "";

for (int w = 0; w < wordList.Count; w++)
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

Console.WriteLine(answer.Trim());