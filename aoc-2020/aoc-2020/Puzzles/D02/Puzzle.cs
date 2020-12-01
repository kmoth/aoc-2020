using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.D02 {
	public class Puzzle : BasePuzzle {
		
		public override void Execute() {
			List<int> convertedInput = Input.Select(int.Parse).ToList();
			int result = FindPair(convertedInput, 2020);
			Console.WriteLine($"result: {result}");

			result = FindTrio(convertedInput, 2020);
			Console.WriteLine($"result: {result}");
		}
		
		private int FindPair(List<int> pInputs, int pMatchSum) {
			foreach(int inputA in pInputs) {
				foreach(int inputB in pInputs) {
					if(inputA + inputB == pMatchSum) {
						return inputA * inputB;
					}
				}
			}

			return -1;
		}
		
		private int FindTrio(List<int> pInputs, int pMatchSum) {
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
