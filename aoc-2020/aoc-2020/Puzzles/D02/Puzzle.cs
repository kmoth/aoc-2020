using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.D02 {
	public class Puzzle : BasePuzzle {

		private readonly List<CorruptedPassword> _convertedInput;

		public Puzzle() {
			_convertedInput = LoadInputLines().Select(pInput => 
				new CorruptedPassword(pInput)).ToList();
		}
		
		public override string SolvePartOne() {
			return _convertedInput.Count(ValidatePasswordPart1).ToString();
		}
		
		public override string SolvePartTwo() {
			return _convertedInput.Count(ValidatePasswordPart2).ToString();
		}

		private static bool ValidatePasswordPart1(CorruptedPassword pCorruptedPassword) {
			int characterCount = pCorruptedPassword.Password.Count(
				pCharacter => pCharacter == pCorruptedPassword.Character);

			return characterCount >= pCorruptedPassword.Min 
			       && characterCount <= pCorruptedPassword.Max;
		}
		
		private static bool ValidatePasswordPart2(CorruptedPassword pCorruptedPassword) {
			char char1 = SafelyGetChar(pCorruptedPassword.Password, pCorruptedPassword.Min);
			char char2 = SafelyGetChar(pCorruptedPassword.Password, pCorruptedPassword.Max);
			bool isValid = char1 == pCorruptedPassword.Character ^ char2 == pCorruptedPassword.Character;
			Console.WriteLine($"{isValid} - {pCorruptedPassword}, 1={char1}, 2={char2}");
			return isValid;
		}

		private static char SafelyGetChar(string pPassword, int pIndex) {
			// IT'S NOT ZERO-BASED
			int adjustedIndex = pIndex - 1;
			return adjustedIndex < pPassword.Length
				? pPassword[adjustedIndex]
				: default;
		}

	}

	public readonly struct CorruptedPassword {

		private readonly string _raw;

		public string Password { get; }

		public char Character { get; }

		public int Max { get; }

		public int Min { get; }
		
		public CorruptedPassword(string pInput) {
			_raw = pInput;
			string[] bits = _raw.Split(' ');
			Min = Convert.ToInt32(bits[0].Split('-')[0]);
			Max = Convert.ToInt32(bits[0].Split('-')[1]);
			Character = bits[1][0];
			Password = bits[2];
		}

		public override string ToString() {
			return _raw;
		}

	}
}
