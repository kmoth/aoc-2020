using System.Collections.Generic;
using System.Linq;

namespace AoC.D03 {
	public class Puzzle : BasePuzzle {

		private readonly List<string> _convertedInput;
		private readonly int _inputLength;

		private readonly Dictionary<char,uint> _charValue = new Dictionary<char,uint> {
			{'#',1},
			{'.',0}
		};

		public Puzzle() {
			_convertedInput = LoadInputLines().ToList();
			_inputLength = _convertedInput.First().Length;
		}

		public override string SolvePartOne() {
			return GetTreesAlongSlope(3, 1).ToString();
		}
		
		public override string SolvePartTwo() {
			long treesFound1 = GetTreesAlongSlope(1, 1);
			long treesFound2 = GetTreesAlongSlope(3, 1);
			long treesFound3 = GetTreesAlongSlope(5, 1);
			long treesFound4 = GetTreesAlongSlope(7, 1);
			long treesFound5 = GetTreesAlongSlope(1, 2);
			
			// ARGHFG!!1! INTS ARE NOT ENOUGH
			return (treesFound1 * treesFound2 * treesFound3 * treesFound4 * treesFound5).ToString();
		}

		private uint GetTreesAlongSlope(int pX, int pY) {
			int lastX = pX;
			uint treeCount = 0;
			for(int y = pY; y < _convertedInput.Count; y += pY) {
				treeCount += _charValue[_convertedInput[y][lastX]];
				lastX += pX;
				lastX %= _inputLength;
			}
			return treeCount;
		}


	}

}
