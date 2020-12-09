using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoC.D06 {
	public class Puzzle : BasePuzzle {

		private readonly List<GroupResponse> _groupResponses;

		public Puzzle() {
			_groupResponses = new List<GroupResponse>();
			GroupResponse groupResponse = new GroupResponse();
			StringBuilder groupResponseData = new StringBuilder();
			foreach(string line in LoadInputLines()) {
				if(string.IsNullOrWhiteSpace(line)) {
					groupResponse.Parse(groupResponseData.ToString());
					_groupResponses.Add(groupResponse);
					groupResponseData.Clear();
					groupResponse = new GroupResponse();
					continue;
				}
				groupResponseData.Append($"{line} ");
			}
			
			groupResponse.Parse(groupResponseData.ToString());
			_groupResponses.Add(groupResponse);
		}
		
		public override string SolvePartOne() {
			return _groupResponses.Sum(pGroupResponse => 
				pGroupResponse.UniqueAnswers).ToString();
		}
		
		public override string SolvePartTwo() {
			return _groupResponses.Sum(pGroupResponse => 
				pGroupResponse.EveryoneAnswered).ToString();
		}
	}

	public class GroupResponse {

		private Dictionary<char, int> _charMap;
		private int _groupSize;
		public int UniqueAnswers => _charMap.Count;
		public int EveryoneAnswered => CountEveryone();

		private int CountEveryone() {
			int everyone = 0;
			foreach((char c, int count) in _charMap) {
				everyone += count == _groupSize ? 1 : 0;
			}

			return everyone;
		}

		public void Parse(string pData) {
			string[] personResponses = pData.Split(' ');
			// ignore the last line, its blank
			_groupSize = personResponses.Length - 1;
			_charMap = new Dictionary<char, int>();
			foreach(string personResponse in personResponses) {
				foreach(char c in personResponse) {
					if(_charMap.ContainsKey(c)) {
						_charMap[c]++;
					}
					else {
						_charMap.Add(c, 1);
					}
				}
			}
		}

	}
}
