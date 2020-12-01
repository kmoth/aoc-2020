using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace AoC {
	public abstract class BasePuzzle {

		private const string BASE_PATH = "/Users/kelseymott/work/aoc-2020/aoc-2020/aoc-2020/Puzzles";
		
		protected int Day { get; }
		
		protected IEnumerable<string> Input { get; }

		protected BasePuzzle() {
			Day = ParseDay();
			Input = LoadInput();
		}

		public abstract void Execute();

		private int ParseDay() {
			string fullName = GetType().FullName ?? "";
			Match match = Regex.Match(fullName, @"AoC\.D(\d{2})\.Puzzle");
			Debug.Assert(match.Success, $"Puzzle type name doesn't match expected pattern: {fullName}");
			return int.Parse(match.Groups[1].Value);
		}

		private IEnumerable<string> LoadInput() {
			string inputFilePath = GetFilePath("input.txt");
			
			if(!File.Exists(inputFilePath)) {
				Console.WriteLine($"Input Not Loaded: {inputFilePath}");
				return null;
			}

			Console.WriteLine("Input Loaded Successfully");
			return File.ReadLines(inputFilePath);
		}
		
		private string GetFilePath(string pFileName) {
			return $"{BASE_PATH}/D{Day:00}/{pFileName}";
		}

	}
}
