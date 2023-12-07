//day 1s
var path = @"C:\Users\ilcra\source\github\AdventCode2023\Day1.txt";

using (StreamReader sr = File.OpenText(path))
{
	char[] digits = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
	string? s;
	int final = 0;
	while ((s = sr.ReadLine()) != null)
	{

		var i = s.IndexOfAny(digits);
		var v1 = i < 0 ? 0 : (int)(s[i] - '0');
		i = s.LastIndexOfAny(digits);
		var v2 = i < 0 ? 0 : (int)(s[i] - '0');
		final += (v1 * 10) + v2;
		Console.WriteLine(final);
	}
}