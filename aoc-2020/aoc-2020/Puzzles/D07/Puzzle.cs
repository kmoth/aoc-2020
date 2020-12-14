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
			FindAllThatCanFitInTheShinyGold(bag);
			return bag.GetRequiredBags().ToString();
		}
		
		private void FindAllThatCanHoldTheShinyGold(string pBagName, IDictionary<string, Bag> pBags) {
			foreach(Bag bag in _bags.Values.Where(pBag => 
				pBag.CanHold(pBagName) 
				&& !pBags.ContainsKey(pBag.Name))) {
				pBags.Add(bag.Name, bag);
				FindAllThatCanHoldTheShinyGold(bag.Name, pBags);
			}
		}
		
		private void FindAllThatCanFitInTheShinyGold(Bag pBag) {
			foreach(string bagBag in pBag.Bags) {
				FindAllThatCanFitInTheShinyGold(_bags[bagBag]);
			}
		}
		
	}

	public class Bag {

		private readonly string[] _words;
		public string Name { get; }

		public List<string> Bags => _rules.Keys.ToList();

		private readonly Dictionary<string, int> _rules;
		
		public Bag(string pRawData) {
			_rules = new Dictionary<string, int>();
			_words = pRawData.Split(' ');
			Name = $"{_words[0]}_{_words[1]}";
			ParseContentRules(_words);
		}

		private void ParseContentRules(string[] pWords) {
			int bagCountIndex = 4;
			if(int.TryParse(pWords[bagCountIndex], out int bagCount)) {
				string name = $"{pWords[bagCountIndex + 1]}_{pWords[bagCountIndex + 2]}";
				_rules.Add(name, bagCount);
				bagCountIndex = 8;
				for(int index = bagCountIndex; index < pWords.Length; index += 4) {
					name = $"{pWords[index + 1]}_{pWords[index + 2]}";
					_rules.Add(name, int.Parse(pWords[bagCountIndex]));
				}
			}
		}

		public override string ToString() {
			StringBuilder stringBuilder = new StringBuilder();
			foreach((string name, int count) in _rules) {
				stringBuilder.Append($"[{count} {name}]");
			}
			return $"({Name}) {stringBuilder}";
		}


		public bool CanHold(string pBagName) {
			return _rules.ContainsKey(pBagName);
		}

		public int GetRequiredBags() {
			return 0;
		}

	}
}
