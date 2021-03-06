using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.D05 {
	public class Puzzle : BasePuzzle {

		private readonly List<string> _convertedInput;
		
		private delegate (int min, int max) Op((int min, int max) pIn);
		
		private static readonly Dictionary<char, Op> ROW_OPS = new Dictionary<char, Op> {
			{'B', TakeUpper},
			{'F', TakeLower}
		};
		
		private static readonly Dictionary<char, Op> COLUMN_OPS = new Dictionary<char, Op> {
			{'R', TakeUpper},
			{'L', TakeLower}
		};

		private static (int min, int max) TakeUpper((int min, int max) pRange) {
			(int min, int max) = pRange;
			(int, int max) upper = (min + (max - min) / 2 + 1, max);
			// Console.WriteLine($"{nameof(upper)}: {upper}");
			return upper;
		}
		
		private static (int min, int max) TakeLower((int min, int max) pRange) {
			(int min, int max) = pRange;
			(int min, int) lower = (min, max - (max - min) / 2 - 1);
			// Console.WriteLine($"{nameof(lower)}: {lower}");
			return lower;
		}

		public Puzzle() {
			_convertedInput = LoadInputLines().ToList();
		}

		private (int min, int max) _seatRange;
		private readonly Dictionary<int, string> _seatIds = new Dictionary<int, string>();

		public override string SolvePartOne() {
			_seatIds.Clear();
			List<int> seats = new List<int>();
			_seatRange = (int.MaxValue, int.MinValue);
			foreach(string boardingPass in _convertedInput) {
				int seatId = GetSeatID(boardingPass);
				_seatIds.Add(seatId, boardingPass);
				seats.Add(seatId);
				_seatRange.min = Math.Min(_seatRange.min, seatId);
				_seatRange.max = Math.Max(_seatRange.max, seatId);
			}
			seats.Sort();

			
			return seats.Last().ToString();
		}
		
		public override string SolvePartTwo() {
			int currentIndex = _seatRange.max;
			List<int> seatIdsOfInterest = new List<int>();
			while(currentIndex >= _seatRange.min) {
				if(!_seatIds.ContainsKey(currentIndex)) {
					seatIdsOfInterest.Add(currentIndex);
				}
				--currentIndex;
			}
			
			return seatIdsOfInterest.First().ToString();
		}

		private int GetSeatID(string pBoardingPass) {
			char[] rowChars = new char[7];
			char[] columnChars = new char[3];
			const int columnOffset = 7;
			for(int i = 0; i < 7; i++) {
				rowChars[i] = pBoardingPass[i];
			}
			for(int i = 0; i < 3; i++) {
				columnChars[i] = pBoardingPass[columnOffset + i];
			}
			int row = FindRow(rowChars);
			int column = FindColumn(columnChars);
			// Console.WriteLine($"row: {row}, column: {column}");
			return row * 8 + column;
		}

		private static int FindRow(IEnumerable<char> pInstructions) {
			(int min, int max) r = (0, 127);
			r = pInstructions.Aggregate(r, 
				(pCurrent, pInstruction) => 
					ROW_OPS[pInstruction].Invoke(pCurrent));
			return r.min;
		}

		private static int FindColumn(IEnumerable<char> pInstructions) {
			(int min, int max) c = (0, 7);
			c = pInstructions.Aggregate(c, 
				(pCurrent, pInstruction) => 
					COLUMN_OPS[pInstruction].Invoke(pCurrent));
			return c.max;
		}
		
	}
}
