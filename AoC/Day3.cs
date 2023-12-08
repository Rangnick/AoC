using System.Reflection;

public static class Day3
{
    static int hits = 0;

    public static string Main()
    {
        string input = File.ReadAllText(@"inputs\3.txt");
        List<string> tmp = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
        List<int> numbers = new List<int>();
        List<int> numbers2 = new List<int>();

        for (int i = 0; i < tmp.Count; i++)
        {
            for (int j = 0; j < tmp[i].Length; j++)
            {
                hits = 0;
                numbers = new List<int>();
                if(tmp[i][j] == '*')
                {
                    if(Int32.TryParse(tmp[i-1][j-1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i-1, j-1, ref tmp));
                    }
                    if(Int32.TryParse(tmp[i-1][j].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i-1, j, ref tmp));
                    }
                    if(Int32.TryParse(tmp[i-1][j+1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i-1, j+1, ref tmp));
                    }

                    if(Int32.TryParse(tmp[i][j-1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i, j-1, ref tmp));
                    }
                    if(Int32.TryParse(tmp[i][j+1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i, j+1, ref tmp));
                    }

                    if(Int32.TryParse(tmp[i+1][j-1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i+1, j-1, ref tmp));
                    }
                    if(Int32.TryParse(tmp[i+1][j].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i+1, j, ref tmp));
                    }
                    if(Int32.TryParse(tmp[i+1][j+1].ToString(), out _))
                    {
                        numbers.Add(GetNumber(i+1, j+1, ref tmp));
                    }
                    if (numbers.Count == 2)
                    {
                        numbers2.Add(numbers[1] * numbers[0]);
                    }
                }
            }
        }

        return numbers2.Sum().ToString();
    } 

    public static int GetNumber(int ind, int pos, ref List<string> tmp)
    {
        string line = tmp[ind];
        string result = "" + line[pos];
        var a = line.ToCharArray();
        a[pos] = '.';
        tmp[ind] = line = new string(a);

        int counter = 1;
        while(true)
        {
            if(pos + counter <= line.Length-1 && Char.IsNumber(line[pos + counter]))
            {
                result += line[pos + counter];
                var b = line.ToCharArray();
                b[pos + counter] = '.';
                tmp[ind] = line = new string(b);
                counter += 1;
                continue;
            }
            break;
        }

        counter = 1;
        while(true)
        {
            if(pos - counter >= 0 && Char.IsNumber(line[pos - counter]))
            {
                result = line[pos - counter] + result;
                var b = line.ToCharArray();
                b[pos - counter] = '.';
                tmp[ind] = line = new string(b);
                counter += 1;
                continue;
            }
            break;
        }
        
        hits = hits + 1;
        return Int32.Parse(result);
    }
}