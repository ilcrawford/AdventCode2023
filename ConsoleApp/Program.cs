var path = @"C:\Users\ilcra\source\github\AdventCode2023\Day4.txt";
var test = @"Game 199: 1 red, 10 blue, 5 green; 11 blue, 6 green; 6 green; 1 green, 1 red, 12 blue; 3 blue; 3 blue, 4 green, 1 red";

/*
 * Example Data
 * Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
 * Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
 * Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
 * Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
 * Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
 * Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
 * Card 99: 41 31  1 21 61 | 71 11 36 23 35 67 74 77
 * 
*/

List<string> list =
[
	"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
	"Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
	"Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
	"Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
	"Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
	"Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11",
	"Card 99: 41 31  1 21 61 | 71 11 36 23 35 67 74 77"
];

using (StreamReader sr = File.OpenText(path))
{
	//string s = "";
	int answer = 0;
	var cards = new List<Card>();

	//while ((s = sr.ReadLine()) != null)
	foreach (string s in list)
	{
		var i = s.IndexOf(':');
		var name = s.Substring(0, i);
		var p = s.IndexOf('|');
		var w = s.Substring(i + 1, p - (i + 1)).Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
		var g = s.Substring(p + 1, s.Length - (p + 1)).Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();


		var newCard = new Card(
			name: name,
			winningNbrs: Array.ConvertAll(w, n => int.Parse(n)),
			guesses: Array.ConvertAll(g, n => int.Parse(n)));
		;

		cards.Add(newCard);

		Console.WriteLine(newCard);


	}

	var x = (from a in (
				from c in cards
				select new
				{
					card = c.Name,
					count = (from g in c.Guesses
							 join w in c.WinningNbrs on g equals w
							 select g).Count(),
				})
			 select new
			 {
				 a.card,
				 score = ((Func<int>)(() =>
				 {
					 return a.count switch
					 {
						 0 => 0,
						 1 => 1,
						 _ => (int)Math.Pow(2, (a.count - 1)),
					 };
				 }))(),
			 }).ToArray();
	answer = (from a in x select a.score).Sum();
	Console.WriteLine($"Answer {answer}");
}

Console.WriteLine("\n\r********************************\n\r");

//Console.WriteLine($"Day4.1 Answer {answer}");

class Card
{
	public Card(string name, IEnumerable<int> winningNbrs, IEnumerable<int> guesses)
	{
		this.Name = name;
		this.WinningNbrs.AddRange(winningNbrs);
		this.Guesses.AddRange(guesses);
	}

	public string Name;
	public List<int> WinningNbrs = [];
	public List<int> Guesses = [];

	public override string ToString()
	{
		var w = string.Join(' ', this.WinningNbrs);
		var g = string.Join(' ', this.Guesses);
		return $"{this.Name} : {w} | {g}";
	}
}