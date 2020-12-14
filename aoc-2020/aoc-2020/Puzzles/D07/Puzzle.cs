using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.D07 {
	public class Puzzle : BasePuzzle {

		private readonly Dictionary<string, Bag> _bags;

		public Puzzle() {
			_bags = new Dictionary<string, Bag>();
			IEnumerable<string> input = LoadInputLines();
			foreach(string line in input) {
				Bag bag = new Bag(line);
				_bags.Add(bag.Name, bag);
			}
		}
		
		public override string SolvePartOne() {
			Dictionary<string,Bag> bags = new Dictionary<string,Bag>();
			FindAllThatCanHoldTheShinyGold("shiny_gold", bags);
			return bags.Count.ToString();
		}

		public override string SolvePartTwo() {
			Bag bag = _bags["shiny_gold"];
			return bag.GetTotalBags(_bags).ToString();
		}
		
		private void FindAllThatCanHoldTheShinyGold(string pBagName, IDictionary<string, Bag> pBags) {
			foreach(Bag bag in _bags.Values.Where(pBag => 
				pBag.CanHold(pBagName) 
				&& !pBags.ContainsKey(pBag.Name))) {
				pBags.Add(bag.Name, bag);
				FindAllThatCanHoldTheShinyGold(bag.Name, pBags);
			}
		}

	}

	public class Bag {

		private readonly string[] _words;
		public string Name { get; }

		private readonly Dictionary<string, int> _bags;
		
		public Bag(string pRawData) {
			_bags = new Dictionary<string, int>();
			_words = pRawData.Split(' ');
			Name = $"{_words[0]}_{_words[1]}";
			ParseContentRules(_words);
		}

		private void ParseContentRules(IReadOnlyList<string> pWords) {
			int bagCountIndex = 4;
			
			if(!int.TryParse(pWords[bagCountIndex], out int bagCount)) {
				return;
			}

			string name = $"{pWords[bagCountIndex + 1]}_{pWords[bagCountIndex + 2]}";
			_bags.Add(name, bagCount);
			
			for(int index = 8; index < pWords.Count; index += 4) {
				name = $"{pWords[index + 1]}_{pWords[index + 2]}";
				_bags.Add(name, int.Parse(pWords[index]));
			}
		}

		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			foreach((string name, int count) in _bags) {
				stringBuilder.Append($"[{count} {name}]");
			}
			return $"({Name}) {stringBuilder}";
		}


		public bool CanHold(string pBagName) {
			return _bags.ContainsKey(pBagName);
		}

		public int GetTotalBags(Dictionary<string,Bag> pBags) {
			int bagTotal = 0;

			foreach((string bagName, int bagCount) in _bags) {
				if(bagCount == 0) {
					Console.WriteLine(bagName);
				}
				bagTotal += bagCount + pBags[bagName].GetTotalBags(pBags) * bagCount;
			}
			
			return bagTotal;
		}

	}
}
