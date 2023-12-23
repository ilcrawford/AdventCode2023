using System.Drawing;

var path = @"C:\Users\ilcra\source\github\AdventCode2023\Day3.txt";
var test = @"Game 199: 1 red, 10 blue, 5 green; 11 blue, 6 green; 6 green; 1 green, 1 red, 12 blue; 3 blue; 3 blue, 4 green, 1 red";

/*
 * Example Data
 * "467..114..",
 * "...*......",
 * "..35..633.",
 * "......#...",
 * "617*......",
 * ".....+.58.",
 * "..592.....",
 * "......755.",
 * "...$.*....",
 * ".664.598..",
 * 
 * inc y
 * readline
 *   read over line 1 char at a time. 
 *     If digit capture x, read until no digit into number,  capture next x
 *       store number and x1,x2,y
 *     if symbol caputre y, x, symbol
 *   repeat until end of line
 * repeat;
 * 
 * find valid by looking a anything abs(number y - other y) <= 1, other x between x1-1 and x2 + 1
*/

List<string> list =
[
	"1.12*123..",
	".*........",
	"#?.@.....#",
];

List<Numby> numbyLis = [];

using (StreamReader sr = File.OpenText(path))
{
	string? s = "";
	int answer = 0;
	int x = 1, y = 1;
	while ((s = sr.ReadLine()) != null)
	//foreach (string s in list)
	{
		Console.WriteLine(s);
		var innumb = false;
		var ns = "";
		x = 1;
		int lx = 1, rx = 1;
		foreach (char c in s)
		{
			switch (c)
			{
				case >= '0' and <= '9':
					lx = innumb ? lx : x;
					rx = x;
					innumb = true;
					ns = innumb ? ns + c : c.ToString();
					break;
				default:
					if (innumb)
					{
						numbyLis.Add(new Numby(new Point(lx, y), new Point(rx, y), int.Parse(ns)));
						ns = "";
					}
					innumb = false;
					if (c != '.')
					{
						numbyLis.Add(new Numby(new Point(x, y), new Point(x, y), (int)c * -1));
					}
					break;
			}
			x++;
		}
		if (innumb)
		{
			numbyLis.Add(new Numby(new Point(lx, y), new Point(rx, y), int.Parse(ns)));
		}

		y++;
	}

	Console.WriteLine("\n\r********************************\n\r");

	foreach (Numby nb in numbyLis)
	{
		Console.WriteLine(nb);
	}

	Console.WriteLine("\n\r********************************\n\r");

	var sl = (from n in numbyLis where n.isSymbol select n).ToList();
	var nl = (from n in numbyLis where !n.isSymbol select n).ToList();
	var gl = (from n in numbyLis where n.number == (int)'*' * -1 select n).ToList();

	answer = (from n in nl
			  from ss in sl
			  where n.NextTo(ss)
			  select n.number).Sum();
	Console.WriteLine($"Answer Linq {answer}");

	answer = 0;
	foreach (var n in nl)
	{
		if ((from ss in sl where n.NextTo(ss) select x).Any())
		{
			answer += n.number;
		}
	}

	Console.WriteLine($"Answer loop {answer}");

	answer = (
		from g in gl
		from n in nl
		where g.NextTo(n)
		group n by g into gs
		where gs.Count() == 2
		//select gs).ToList();
		select new
		{
			key = gs.Key,
			nbr1 = gs.ElementAt(0).number,
			nbr2 = gs.ElementAt(1).number,
			value = gs.ElementAt(0).number * gs.ElementAt(1).number
		}).Sum(g => g.value);

	//foreach (var n in t)
	//{
	//	//		Console.WriteLine(n);
	//	Console.WriteLine($"Gear {n.Key}  -- Nbr1 {n.ElementAt(0).number}, Nbr2 {n.ElementAt(1).number}");
	//}
	Console.WriteLine($"Day3.2 Answer {answer}");
	// first guess 24908233
	// second guess 81997870
}

class Numby(Point p1, Point p2, int number)
{
	public Point P1 = p1;
	public Point P2 = p2;
	public int number = number;
	public bool isSymbol = number < 0;

	public bool NextTo(Numby nb)
	{
		return (Math.Abs(this.P1.Y - nb.P1.Y) <= 1 && (nb.P2.X >= this.P1.X - 1) && (nb.P1.X <= this.P2.X + 1));
	}

	public override string ToString()
	{
		return $"Number: {this.number} - P1:{this.P1}, P2:{this.P2}";
	}
}