using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

public class RobotOnMoon
{
    bool executeMove(string[] board, ref int[] newPos, int[] prevPos)
    {
        if (newPos[0] < 0 || newPos[0] >= board.Length)
        {
            return false;
        }

        if (newPos[1] < 0 || newPos[1] >= board[newPos[0]].Length)
        {
            return false;
        }

        if (board[newPos[0]][newPos[1]] == '.')
        {
            return true;
        }

        if (board[newPos[0]][newPos[1]] == '#')
        {
            newPos[0] = prevPos[0];
            newPos[1] = prevPos[1];
            return true;
        }
        return true;
    }

    public string isSafeCommand(string[] board, string S)
    {
        int[] position = new int[2];
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == 'S')
                {
                    position[0] = i;
                    position[1] = j;
                    break;
                }
            }
        }
        foreach (char ch in S)
        {
            int[] newPosition = new int[2];
            switch (ch)
            {
                case 'U':
                    newPosition[0] = position[0] - 1;
                    newPosition[1] = position[1];
                    if (!executeMove(board, ref newPosition, position)) return "Dead";
                    position[0] = newPosition[0];
                    position[1] = newPosition[1];
                    break;
                case 'D':
                    newPosition[0] = position[0] + 1;
                    newPosition[1] = position[1];
                    if (!executeMove(board, ref newPosition, position)) return "Dead";
                    position[0] = newPosition[0];
                    position[1] = newPosition[1];
                    break;
                case 'L':
                    newPosition[0] = position[0];
                    newPosition[1] = position[1] - 1;
                    if (!executeMove(board, ref newPosition, position)) return "Dead";
                    position[0] = newPosition[0];
                    position[1] = newPosition[1];
                    break;
                case 'R':
                    newPosition[0] = position[0];
                    newPosition[1] = position[1] + 1;
                    if (!executeMove(board, ref newPosition, position)) return "Dead";
                    position[0] = newPosition[0];
                    position[1] = newPosition[1];
                    break;
            }
        }
        return "Alive";

    }

    #region Testing code

    [STAThread]
    private static Boolean KawigiEdit_RunTest(int testNum, string[] p0, string p1, Boolean hasAnswer, string p2)
    {
        Console.Write("Test " + testNum + ": [" + "{");
        for (int i = 0; p0.Length > i; ++i)
        {
            if (i > 0)
            {
                Console.Write(",");
            }
            Console.Write("\"" + p0[i] + "\"");
        }
        Console.Write("}" + "," + "\"" + p1 + "\"");
        Console.WriteLine("]");
        RobotOnMoon obj;
        string answer;
        obj = new RobotOnMoon();
        DateTime startTime = DateTime.Now;
        answer = obj.isSafeCommand(p0, p1);
        DateTime endTime = DateTime.Now;
        Boolean res;
        res = true;
        Console.WriteLine("Time: " + (endTime - startTime).TotalSeconds + " seconds");
        if (hasAnswer)
        {
            Console.WriteLine("Desired answer:");
            Console.WriteLine("\t" + "\"" + p2 + "\"");
        }
        Console.WriteLine("Your answer:");
        Console.WriteLine("\t" + "\"" + answer + "\"");
        if (hasAnswer)
        {
            res = answer == p2;
        }
        if (!res)
        {
            Console.WriteLine("DOESN'T MATCH!!!!");
        }
        else if ((endTime - startTime).TotalSeconds >= 2)
        {
            Console.WriteLine("FAIL the timeout");
            res = false;
        }
        else if (hasAnswer)
        {
            Console.WriteLine("Match :-)");
        }
        else
        {
            Console.WriteLine("OK, but is it right?");
        }
        Console.WriteLine("");
        return res;
    }

    public static void Main(string[] args)
    {
        Boolean all_right;
        all_right = true;

        string[] p0;
        string p1;
        string p2;

        // ----- test 0 -----
        p0 = new string[] {".....", ".###.", "..S#.", "...#."};
        p1 = "URURURURUR";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(0, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 1 -----
        p0 = new string[] {".....", ".###.", "..S..", "...#."};
        p1 = "URURURURUR";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(1, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 2 -----
        p0 = new string[] {".....", ".###.", "..S..", "...#."};
        p1 = "URURU";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(2, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 3 -----
        p0 = new string[] {"#####", "#...#", "#.S.#", "#...#", "#####"};
        p1 = "DRULURLDRULRUDLRULDLRULDRLURLUUUURRRRDDLLDD";
        p2 = "Alive";
        all_right = KawigiEdit_RunTest(3, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 4 -----
        p0 = new string[] {"#####", "#...#", "#.S.#", "#...#", "#.###"};
        p1 = "DRULURLDRULRUDLRULDLRULDRLURLUUUURRRRDDLLDD";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(4, p0, p1, true, p2) && all_right;
        // ------------------

        // ----- test 5 -----
        p0 = new string[] {"S"};
        p1 = "R";
        p2 = "Dead";
        all_right = KawigiEdit_RunTest(5, p0, p1, true, p2) && all_right;
        // ------------------

        if (all_right)
        {
            Console.WriteLine("You're a stud (at least on the example cases)!");
        }
        else
        {
            Console.WriteLine("Some of the test cases had errors.");
        }
    }

    #endregion
}