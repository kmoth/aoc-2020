using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.D03 {
	public class Puzzle : BasePuzzle {

		private List<string> _lines;
		private int _lineLength;

		private readonly Dictionary<char,uint> _charValue = new Dictionary<char,uint> {
			{'#',1},
			{'.',0}
		};

		public override void Execute() {
			_lines = Input.ToList();
			_lineLength = _lines.First().Length;
			
			PartOne();
			PartTwo();
		}

		private void PartOne() {
			long treesFound = GetTreesAlongSlope(3, 1);
			Console.WriteLine($"tree count: {treesFound}");
		}

		private void PartTwo() {
			long treesFound1 = GetTreesAlongSlope(1, 1);
			long treesFound2 = GetTreesAlongSlope(3, 1);
			long treesFound3 = GetTreesAlongSlope(5, 1);
			long treesFound4 = GetTreesAlongSlope(7, 1);
			long treesFound5 = GetTreesAlongSlope(1, 2);
			
			Console.WriteLine($"tree count 1: {treesFound1}");
			Console.WriteLine($"tree count 2: {treesFound2}");
			Console.WriteLine($"tree count 3: {treesFound3}");
			Console.WriteLine($"tree count 4: {treesFound4}");
			Console.WriteLine($"tree count 5: {treesFound5}");
			
			// ARGHFG!!1! INTS ARE NOT ENOUGH
			long product = treesFound1 * treesFound2 * treesFound3 * treesFound4 * treesFound5;
			
			Console.WriteLine($"product: {product}");
		}

		private uint GetTreesAlongSlope(int pX, int pY) {
			int lastX = pX;
			uint treeCount = 0;
			for(int y = pY; y < _lines.Count; y += pY) {
				treeCount += _charValue[_lines[y][lastX]];
				lastX += pX;
				lastX %= _lineLength;
			}
			return treeCount;
		}


	}

}
