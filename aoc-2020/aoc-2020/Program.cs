using System;
using System.Collections.Generic;

namespace aoc_2020 {
	class Program {

		private static readonly Dictionary<string, int> ARG_ID_MAP = new Dictionary<string, int> {
			{"puzzleId", 0},
		};
		
		private static readonly Dictionary<string, Type> PUZZLE_MAP = new Dictionary<string, Type> {
			{"p1a", typeof(Puzzle_1a)},
		};
		
		private static void Main(string[] pArgs) {
			if(!GetArg("puzzleId", pArgs, out string puzzleId)) {
				Console.WriteLine($"Invalid argument value for 'puzzleId'");
				return;
			}

			Puzzle puzzleInstance = InstantiatePuzzle(puzzleId);

			if(puzzleInstance == null) {
				Console.WriteLine($"Invalid puzzleId: {puzzleId}");
				return;
			}
			
			Console.WriteLine($"Execute Puzzle: {puzzleId}");
			puzzleInstance.Execute();
		}

		private static Puzzle InstantiatePuzzle(string pPuzzleId) {
			if(!PUZZLE_MAP.ContainsKey(pPuzzleId)) {
				return null;
			}
			return (Puzzle)Activator.CreateInstance(PUZZLE_MAP[pPuzzleId]);
		}

		private static bool GetArg<T>(string pArgId, IReadOnlyList<string> pArgs, out T pOut) {
			if(!ARG_ID_MAP.ContainsKey(pArgId)) {
				pOut = default;
				return false;
			}

			int argIndex = ARG_ID_MAP[pArgId];

			if(pArgs.Count < argIndex) {
				pOut = default;
				return false;
			}

			pOut = (T)Convert.ChangeType(pArgs[argIndex], typeof(T));
			return true;
		}

	}

}
