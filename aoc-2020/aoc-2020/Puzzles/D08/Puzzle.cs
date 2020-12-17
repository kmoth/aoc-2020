using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC.D08 {
	public class Puzzle : BasePuzzle {

		private readonly List<Instruction> _instructions = new List<Instruction>();
		private readonly List<List<Instruction>> _programCandidates;
		private int _opIndex;
		private int _accumulator;
		private readonly Dictionary<string, Action<int>> _instructionMap;

		public Puzzle() {
			IEnumerable<string> input = LoadInputLines(true);
			foreach(string line in input) {
				_instructions.Add(new Instruction(line));
			}

			_instructionMap = new Dictionary<string, Action<int>> {
				{"acc", InstructionAccumulator},
				{"jmp", InstructionJump},
				{"nop", InstructionNone},
			};
		}

		private void InstructionAccumulator(int pArg) {
			_accumulator += pArg;
			_opIndex++;
		}
		
		private void InstructionJump(int pArg) {
			_opIndex += pArg;
		}
		
		private void InstructionNone(int pArg) {
			_opIndex++;
		}

		public override string SolvePartOne() {
			_opIndex = 0;
			_accumulator = 0;
			
			Instruction instruction = _instructions[_opIndex];

			while(++instruction.RunCount < 2) {
				string op = instruction.Op;
				int arg = instruction.Arg;
				_instructionMap[op].Invoke(arg);
				instruction = _instructions[_opIndex];
			}
			
			return _accumulator.ToString();
		}

		public override string SolvePartTwo() {
			int lastSwapIndex = 0;
			while(lastSwapIndex++ < _instructions.Count) {
				foreach(Instruction instruction in _instructions) {
					if(instruction.Op == "nop") {
						var flipped = instruction.Flip();
					}

					lastSwapIndex++;
				}
			}
			
			foreach(Instruction instruction in _instructions) {
				if(instruction.Op == "nop") {
					var flipped = instruction.Flip();
				}

				lastSwapIndex++;
			}
			return "INCOMPLETE";
		}
		
	}

	internal class Instruction {

		public string Op { get; }
		public int Arg { get; }
		public int RunCount { get; set; }

		public Instruction(string pRaw) {
			string[] inputSlit = pRaw.Split(" ");
			Op = inputSlit[0];
			Arg = int.Parse(inputSlit[1], NumberStyles.AllowLeadingSign);
		}
		
		public Instruction(string pOp, int pArg) {
			Op = pOp;
			Arg = pArg;
		}
		
		public Instruction Flip() {
			return Op switch {
				"jmp" => new Instruction("nop", Arg),
				"nop" when Arg != 0 => new Instruction("jmp", Arg),
				_ => new Instruction(Op, Arg)
			};
		}

	}
}
