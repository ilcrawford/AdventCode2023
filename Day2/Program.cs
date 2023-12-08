﻿using System.Text;

var path = @"C:\Users\ilcra\source\github\AdventCode2023\Day2.txt";
var test = @"Game 199: 1 red, 10 blue, 5 green; 11 blue, 6 green; 6 green; 1 green, 1 red, 12 blue; 3 blue; 3 blue, 4 green, 1 red";

using (StreamReader sr = File.OpenText(path))
{
	string? s = test;
	int final = 0;
	var games = new List<Game>();
	while ((s = sr.ReadLine()) != null)
	{
		var g = new Game(s);
		games.Add(g);
		Console.WriteLine(g);
	}

	Console.WriteLine("\n\rBAD\n\r");

	var bad = (from g in games
			   where g.Turns.Count(t => t.Red > 12 || t.Blue > 14 || t.Green > 13) > 0
			   select g).ToList();

	var good = games.ExceptBy(bad.Select(b => b.ID), z => z.ID).ToList();


	Console.WriteLine();
	Console.WriteLine("GOOD");

	foreach (var g in good)
	{
		Console.WriteLine(g);
	}

	var answer = good.Sum(g => g.ID);

	Console.WriteLine($"Answer {answer}");

}

public class Game
{
	public Game(string raw)
	{
		this.Raw = raw;
		var i = this.Raw.IndexOf(':');
		this.ID = int.Parse(this.Raw[5..i]);
		i++;
		var t = this.Raw[i..].Split(';');
		foreach (var t2 in t)
		{
			this.Turns.Add(new Turn(t2));
		}

	}

	public string Raw { get; }
	public int ID { get; set; }

	public List<Turn> Turns { get; set; } = new List<Turn>();

	public override string ToString()
	{
		var result = new StringBuilder();
		result.Append($"Game {this.ID}: ");
		foreach (var t in this.Turns)
		{
			result.Append(t.ToString());
		}

		return result.ToString();
	}
};

public class Turn
{
	public Turn(string raw)
	{
		this.Raw = raw;
		var colors = this.Raw.Split(',');
		int i;
		foreach (var t in colors)
		{
			if (t.Contains("green", StringComparison.OrdinalIgnoreCase))
			{
				i = t.Length - 5;
				this.Green = int.Parse(t[1..i]);
				continue;
			}

			if (t.Contains("blue", StringComparison.OrdinalIgnoreCase))
			{
				i = t.Length - 4;
				this.Blue = int.Parse(t[1..i]);
				continue;
			}

			if (t.Contains("red", StringComparison.OrdinalIgnoreCase))
			{
				i = t.Length - 3;
				this.Red = int.Parse(t[1..i]);
				continue;
			}
		}
	}
	public string Raw { get; }
	public int Red { get; set; } = 0;
	public int Blue { get; set; } = 0;
	public int Green { get; set; } = 0;

	public override string ToString()
	{
		return $"Red {this.Red}, Blue {this.Blue}, Green {this.Green};";
	}

}


