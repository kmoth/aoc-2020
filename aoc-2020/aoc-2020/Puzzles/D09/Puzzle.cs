using System.Collections.Generic;
using System.Linq;

namespace AoC.D09 {
	public class Puzzle : BasePuzzle {

		private readonly List<int> _convertedInput;

		public Puzzle() {
			_convertedInput = LoadInputLines(true)
				.Select(int.Parse)
				.ToList();
		}
		
		public override string SolvePartOne() {
			XmasCracker xmasCracker = new XmasCracker(5, _convertedInput);
			return xmasCracker.FirstInvalid();
		}
		
		public override string SolvePartTwo() {
			return "INCOMPLETE";
		}
		
	}

	public class XmasCracker {

		private int _preambleSize;
		private List<int> _input;

		public XmasCracker(int pPreambleSize, List<int> pInput) {
			_preambleSize = pPreambleSize;
			_input = pInput;
		}

		public string FirstInvalid() {
			return "INCOMPLETE";
		}

	}
}
