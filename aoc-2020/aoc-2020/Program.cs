using System;
using System.Collections.Generic;

namespace AoC {
	class Program {

		private static readonly Dictionary<string, int> ARG_ID_MAP = new Dictionary<string, int> {
			{"puzzleId", 0},
		};
		
		private static void Main(string[] pArgs) {
			if(!GetArg("puzzleId", pArgs, out string puzzleId)) {
				Console.WriteLine($"Invalid argument value for 'puzzleId'");
				return;
			}

			BasePuzzle puzzleInstance = InstantiatePuzzle(puzzleId);

			if(puzzleInstance == null) {
				Console.WriteLine($"Invalid puzzleId: {puzzleId}");
				return;
			}
			
			Console.WriteLine($"Execute Puzzle: {puzzleId}");
			
			string partOneResult = puzzleInstance.SolvePartOne();
			Console.WriteLine($"Part One: {partOneResult}");
			
			string partTwoResult = puzzleInstance.SolvePartTwo();
			Console.WriteLine($"Part Two: {partTwoResult}");
		}

		private static BasePuzzle InstantiatePuzzle(string pPuzzleId) {
			Type type = Type.GetType($"AoC.{pPuzzleId}.Puzzle");
			
			if(type == null) {
				return null;
			}
			
			BasePuzzle puzzle = (BasePuzzle)Activator.CreateInstance(type);

			return puzzle;
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
