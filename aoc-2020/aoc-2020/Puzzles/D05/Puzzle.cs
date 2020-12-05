using System.Collections.Generic;
using System.Linq;

namespace AoC.D05 {
	public class Puzzle : BasePuzzle {

		private readonly List<string> _convertedInput;

		public Puzzle() {
			_convertedInput = LoadInputLines().ToList();
		}
		
		public override string SolvePartOne() {
			string first = _convertedInput.First();
			int seatId = GetSeatID(first);
			// TODO Figure out which boarding pass is mine by process of elimination
			// Maybe by finding the missing element in the seat ID pattern
			return "INCOMPLETE";
		}

		private int GetSeatID(string pBoardingPass) {
			int row = FindRow(pBoardingPass);
			int column = FindColumn(pBoardingPass);
			return row * 8 + column;
		}

		private int FindRow(string pBoardingPass) {
			return -1;
		}
		
		private int FindColumn(string pBoardingPass) {
			return -1;
		}

		public override string SolvePartTwo() {
			return "INCOMPLETE";
		}
	}
}
