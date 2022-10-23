using System;
using System.Text;

class Solution
{
    static void Main(string[] args)
    {
        string MESSAGE = Console.ReadLine();

        byte[] bytes = Encoding.ASCII.GetBytes(MESSAGE);
        string binaryString = "";
        foreach (byte b in bytes) binaryString += Convert.ToString(b, 2).PadLeft(7 , '0');

        string answer = "";
        while (binaryString != "")
        {
            if (answer != "") answer += " ";
            char firstChar = binaryString[0];
            int length = 1;
            while (binaryString.Length > length && binaryString[length] == firstChar) length++;
            answer += (firstChar == '1')? "0 " : "00 ";
            answer += new string('0', length);
            binaryString = binaryString.Remove(0, length);
        }

        Console.WriteLine(answer);
    }
}

// https://www.codingame.com/ide/puzzle/unary