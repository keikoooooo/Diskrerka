using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите количество переменных: ");
        int numVars = int.Parse(Console.ReadLine());


        var functionValues = ReadFunctionValues(numVars);
        var truthTable = GenTruthTable(numVars, functionValues);

        var sdnf = GenSDNF(truthTable, numVars);
        var sknf = GenSKNF(truthTable, numVars);

        Console.WriteLine("\nТаблица истинности:");
        foreach (var row in truthTable)
        {
            foreach (var val in row)
                Console.Write(val ? "1 " : "0 ");
            Console.WriteLine();
        }

        Console.WriteLine("\nСДНФ: " + sdnf);
        Console.WriteLine("СКНФ: " + sknf);
    }

    static List<bool> ReadFunctionValues(int numVars) {
        Console.Write("Введите значения: ");
        string input = Console.ReadLine();
        var values = new List<bool>();

        foreach (char val in input)
        {


            if (val == '0')
            {

                values.Add(false);
            }
            else if (val == '1')
            {
                values.Add(true);

            }
            else
            {
                Console.WriteLine("Вы ввели недопустимый символ.Вводите только 0 или 1");
                return ReadFunctionValues(numVars);

            }
        }




        if (values.Count != Math.Pow(2, numVars))
        {
            Console.WriteLine("количество введенных значений не соответствует количеству переменных");
            return ReadFunctionValues(numVars);
        }

        return values;
    }

    static List<List<bool>> GenTruthTable(int numVars, List<bool> functionValues)
    {
        int numRows = 1 << numVars;
        var table = new List<List<bool>>(numRows);      

        for (int i = 0; i < numRows; i++)
        {
            var row = new List<bool>(numVars + 1);
            for (int j = 0; j < numVars; j++)
                row.Add(((i >> j) & 1) == 1);
            row.Add(functionValues[i]);
            table.Add(row);
        }

        return table;
    }

    static string GenSDNF(List<List<bool>> table, int numVars)
    {
        string result = "";
        foreach (var row in table)
        {
            if (row[numVars])
            {
                if (!string.IsNullOrEmpty(result))
                    result += " + ";
                result += "(";
                for (int j = 0; j < numVars; j++)
                {
                    if (j > 0)
                        result += " * ";
                    if (!row[j])
                        result += "¬";
                    result += $"x{j + 1}";
                }
                result += ")";
            }
        }
        return result;
    }

    static string GenSKNF(List<List<bool>> table, int numVars)
    {
        string result = "";
        foreach (var row in table)
        {
            if (!row[numVars])
            {
                if (!string.IsNullOrEmpty(result))
                    result += " * ";
                result += "(";

                for (int j = 0; j < numVars; j++)
                {

                    if (j > 0)
                        result += " + ";
                    if (row[j])
                        result += "¬";
                    result += $"x{j + 1}";
                }
                result += ")";
            }
        }



        return result;
    }
}