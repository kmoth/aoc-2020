using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AoC.D08 {
	public class Puzzle : BasePuzzle {

		private readonly List<Instruction> _instructions = new List<Instruction>();

		public Puzzle() {
			IEnumerable<string> input = LoadInputLines();
			foreach(string line in input) {
				_instructions.Add(new Instruction(line));
			}
		}

		public override string SolvePartOne() {
			GameProgram gameProgram = new GameProgram(_instructions);
			gameProgram.Run();
			return gameProgram.Accumulator.ToString();
		}

		public override string SolvePartTwo() {
			int swapIndex = 0;
			List<int> swapIndices = new List<int>();
			while(swapIndex < _instructions.Count) {
				swapIndex = _instructions.FindIndex(swapIndex, pInstruction => 
					pInstruction.Op == "nop" && pInstruction.Arg != 0 
					|| pInstruction.Op == "jmp");

				if(swapIndex == -1) {
					break;
				}

				swapIndices.Add(swapIndex);
				swapIndex++;
			}

			// PrintInstructions(_instructions);

			foreach(int index in swapIndices) {
				List<Instruction> instructions = CopyInstructions();
				instructions[index] = instructions[index].Flip();
				// PrintInstructions(instructions);
				GameProgram gameProgram = new GameProgram(instructions);
				gameProgram.Run();
				if(!gameProgram.DidTerminate()) {
					continue;
				}

				return gameProgram.Accumulator.ToString();
			}

			return "FAILED";
		}

		private void PrintInstructions(List<Instruction> pInstructions) {
			StringBuilder stringBuilder = new StringBuilder();
			foreach(Instruction instruction in pInstructions) {
				stringBuilder.Append($"{instruction}, ");
			}
			Console.WriteLine(stringBuilder);
		}

		private List<Instruction> CopyInstructions() {
			List<Instruction> instructions = _instructions.Select(pInstruction => 
				new Instruction(pInstruction)).ToList();
			return instructions;
		}

	}

	internal class GameProgram {

		public int Accumulator { get; private set; }
		
		private int _opIndex;

		private readonly Dictionary<string, Action<int>> _instructionMap;
		private List<Instruction> _instrucitons;

		public GameProgram(List<Instruction> pInstructions) {
			_instrucitons = pInstructions;
			_instructionMap = new Dictionary<string, Action<int>> {
				{"acc", InstructionAccumulator},
				{"jmp", InstructionJump},
				{"nop", InstructionNone},
			};
		}

		public void Run() {
			_opIndex = 0;
			Accumulator = 0;
			
			if(_opIndex >= _instrucitons.Count || _opIndex <= -1) {
				return;
			}
			
			Instruction instruction = _instrucitons[_opIndex];

			while(_opIndex < _instrucitons.Count && _opIndex > -1 && ++instruction.RunCount < 2) {
				string op = instruction.Op;
				int arg = instruction.Arg;
				_instructionMap[op].Invoke(arg);
				if(_opIndex >= _instrucitons.Count || _opIndex <= -1) {
					break;
				}
				
				instruction = _instrucitons[_opIndex];
				if(instruction.RunCount >= 2) {
					break;
				}
			}
		}

		// private bool GetInstruction(out Instruction pInstruction) {
		// 	if(_opIndex < _instrucitons.Count && _opIndex > -1) {
		// 		pInstruction = _instrucitons[_opIndex];
		// 		return true;
		// 	}
		//
		// 	pInstruction = null;
		// 	return false;
		// }

		private void InstructionAccumulator(int pArg) {
			Accumulator += pArg;
			_opIndex++;
		}
		
		private void InstructionJump(int pArg) {
			_opIndex += pArg;
		}
		
		private void InstructionNone(int pArg) {
			_opIndex++;
		}

		public bool DidTerminate() {
			return _instrucitons.All(pInstruction => pInstruction.RunCount < 2);
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
			RunCount = 0;
		}
		
		public Instruction(string pOp, int pArg) {
			Op = pOp;
			Arg = pArg;
			RunCount = 0;
		}

		public Instruction(Instruction pInstruction) {
			Op = pInstruction.Op;
			Arg = pInstruction.Arg;
			RunCount = 0;
		}

		public Instruction Flip() {
			return Op switch {
				"jmp" => new Instruction("nop", Arg),
				"nop" when Arg != 0 => new Instruction("jmp", Arg),
				_ => new Instruction(Op, Arg)
			};
		}

		public override string ToString() {
			return Op;//$"[Instruction] op={Op}, arg={Arg}";
		}

	}
}
