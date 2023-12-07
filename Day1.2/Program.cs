//day 1s
var path = @"C:\Users\ilcra\source\github\AdventCode2023\Day1.txt";
var test = "ggdone3nbmsthreefourninefiveoneightpr";

using (StreamReader sr = File.OpenText(path))
{
	string? s = null;
	int final = 0;
	while ((s = sr.ReadLine()) != null)
	{

		var (fi, li) = parse(s);
		final += (fi * 10) + li;
		Console.WriteLine($"{s} : {fi} - {li} - {final}");
	}
	Console.WriteLine(final);

}


static (int fi, int li) parse(string s)
{
	var i = 0;
	var done = false;
	int i2 = 0;
	int fi = -1;
	int li = -1;

	while (!done)
	{
		var s2 = s[i];
		i2 = -1;
		switch (s2)
		{
			case >= '0' and <= '9':
				i2 = s2 - '0';
				//i++;
				break;
			case 'O':
			case 'o':
				if (i + 3 <= s.Length
					&& string.Equals(s.Substring(i, 3), "one", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 1;
					//i += 3;
				}
				break;
			case 'T':
			case 't':
				if (i + 3 <= s.Length
					&& string.Equals(s.Substring(i, 3), "two", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 2;
					//i += 3;
				}
				else if (i + 5 <= s.Length
						&& string.Equals(s.Substring(i, 5), "three", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 3;
					//i += 5;
				}
				break;
			case 'F':
			case 'f':
				if (i + 4 <= s.Length
					&& string.Equals(s.Substring(i, 4), "four", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 4;
					//i += 4;
				}
				else if (i + 4 <= s.Length
						&& string.Equals(s.Substring(i, 4), "five", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 5;
					//i += 4;
				}
				break;
			case 'S':
			case 's':
				if (i + 3 <= s.Length
					&& string.Equals(s.Substring(i, 3), "six", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 6;
					//i += 3;
				}
				else if (i + 5 <= s.Length
						&& string.Equals(s.Substring(i, 5), "seven", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 7;
					//i += 5;
				}
				break;
			case 'E':
			case 'e':
				if (i + 5 <= s.Length
					&& string.Equals(s.Substring(i, 5), "eight", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 8;
					//i += 5;
				}
				break;
			case 'N':
			case 'n':
				if (i + 4 <= s.Length
					&& string.Equals(s.Substring(i, 4), "nine", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 9;
					//i += 4;
				}
				break;
			case 'Z':
			case 'z':
				if (i + 4 <= s.Length
					&& string.Equals(s.Substring(i, 4), "zero", StringComparison.OrdinalIgnoreCase))
				{
					i2 = 0;
					//i += 4;
				}
				break;
			default:
				i2 = -1;
				break;
		}

		fi = fi < 0 ? i2 : fi;
		li = i2 > 0 ? i2 : li;
		i++;
		done = i >= s.Length;

		//Console.WriteLine($"{s} : {i}");
	}
	return (fi, li);
}