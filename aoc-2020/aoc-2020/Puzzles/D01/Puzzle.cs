using System.Collections.Generic;
using System.Linq;

namespace AoC.D01 {
	public class Puzzle : BasePuzzle {

		private readonly List<int> _convertedInput;

		public Puzzle() {
			_convertedInput = LoadInputLines().Select(int.Parse).ToList();
		}

		public override string SolvePartOne() {
			return FindPair(_convertedInput, 2020).ToString();
		}
		
		public override string SolvePartTwo() {
			return FindTrio(_convertedInput, 2020).ToString();
		}
		
		private static int FindPair(IReadOnlyCollection<int> pInputs, int pMatchSum) {
			foreach(int inputA in pInputs) {
				foreach(int inputB in pInputs) {
					if(inputA + inputB == pMatchSum) {
						return inputA * inputB;
					}
				}
			}

			return -1;
		}
		
		private static int FindTrio(IReadOnlyCollection<int> pInputs, int pMatchSum) {
			foreach(int inputA in pInputs) {
				foreach(int inputB in pInputs) {
					foreach(int inputC in pInputs) {
						if(inputA + inputB + inputC == pMatchSum) {
							return inputA * inputB * inputC;
						}
					}
				}
			}

			return -1;
		}
	}
}
