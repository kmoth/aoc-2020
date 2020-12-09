using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC.D04 {
	public class Puzzle : BasePuzzle {

		private readonly List<Passport> _passports;

		public Puzzle() {
			_passports = new List<Passport>();
			Passport passport = new Passport();
			StringBuilder passportData = new StringBuilder();
			foreach(string line in LoadInputLines()) {
				if(string.IsNullOrWhiteSpace(line)) {
					passport.Parse(passportData.ToString());
					_passports.Add(passport);
					passportData.Clear();
					passport = new Passport();
					continue;
				}
				passportData.Append($"{line} ");
			}
			
			passport.Parse(passportData.ToString());
			_passports.Add(passport);
		}
		
		public override string SolvePartOne() {
			return ValidatePassportsPartOne(_passports).ToString();
		}

		public override string SolvePartTwo() {
			return ValidatePassportsPartTwo(_passports).ToString();
		}

		private int ValidatePassportsPartOne(IEnumerable<Passport> pPassports) {
			return pPassports.Count(pPassport => pPassport.HasRequiredCreds);
		}
		
		private int ValidatePassportsPartTwo(IEnumerable<Passport> pPassports) {
			return pPassports.Count(pPassport => pPassport.AreRequiredCredsValid);
		}

	}

	public struct Passport {

		private int _validCreds;
		private int _foundCreds;
		private int _requiredCreds;

		public bool HasRequiredCreds { get; private set; }
		
		public bool AreRequiredCredsValid { get; private set; }

		public void Parse(string pRawData) {
			string[] creds = pRawData.Split(' ');
			_validCreds = 0;
			_foundCreds = 0;
			_requiredCreds = 7;
			foreach(string cred in creds) {
				ParseCred(cred);
			}

			HasRequiredCreds = _foundCreds == _requiredCreds;
			AreRequiredCredsValid = _validCreds == _requiredCreds;
		}

		private void ParseCred(string pCred) {
			string[] credPair = pCred.Split(':');
			switch(credPair[0]) {
				case "byr":
					ValidateByr(credPair[1]);
					_foundCreds++;
					break;
				case "iyr":
					ValidateIyr(credPair[1]);
					_foundCreds++;
					break;
				case "eyr":
					ValidateEyr(credPair[1]);
					_foundCreds++;
					break;
				case "hgt":
					ValidateHgt(credPair[1]);
					_foundCreds++;
					break;
				case "hcl":
					ValidateHcl(credPair[1]);
					_foundCreds++;
					break;
				case "ecl":
					ValidateEcl(credPair[1]);
					_foundCreds++;
					break;
				case "pid":
					ValidatePid(credPair[1]);
					_foundCreds++;
					break;
			}
		}

		private void ValidateByr(string pValue) {
			// (Birth Year) - four digits; at least 1920 and at most 2002.
			if(!int.TryParse(pValue, out int convertedValue)
			|| !InRange(convertedValue, 1920, 2002)) {
				return;
			}
			_validCreds++;
		}

		private void ValidateIyr(string pValue) {
			// (Issue Year) - four digits; at least 2010 and at most 2020.
			if(!int.TryParse(pValue, out int convertedValue)
			   || !InRange(convertedValue, 2010, 2020)) {
				return;
			}
			_validCreds++;
		}
		
		private void ValidateEyr(string pValue) {
			// (Expiration Year) - four digits; at least 2020 and at most 2030.
			if(!int.TryParse(pValue, out int convertedValue)
			   || !InRange(convertedValue, 2020, 2030)) {
				return;
			}
			_validCreds++;
		}
		
		private void ValidateHgt(string pValue) {
			// hgt (Height) - a number followed by either cm or in:
			// If cm, the number must be at least 150 and at most 193.
			// If in, the number must be at least 59 and at most 76.
			string unit = pValue.Substring(pValue.Length - 2);
			
			if(!ParseHeightValue(pValue, unit, out int height)) {
				return;
			}
			
			switch(unit) {
				case "cm":
					if(InRange(height, 150, 193)) {
						_validCreds++;
					}
					break;
				case "in":
					if(InRange(height, 59, 76)) {
						_validCreds++;
					}
					break;
			}
		}

		private bool ParseHeightValue(string pValue, string pUnit, out int pHeight) {
			string heightString = pValue.Replace(pUnit, "");
			return int.TryParse(heightString, out pHeight);
		}

		private void ValidateHcl(string pValue) {
			// (Hair Color) - a # followed by exactly six characters 0-9 or a-f.
			Match match = Regex.Match(pValue, @"^#[a-f0-9]{6}$");
			if(!match.Success) {
				return;
			}
			_validCreds++;
		}
		
		private void ValidateEcl(string pValue) {
			// (Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
			switch(pValue) {
				case "amb":
				case "blu":
				case "brn":
				case "gry":
				case "grn":
				case "hzl":
				case "oth":
					_validCreds++;
					break;
			}
		}
		
		private void ValidatePid(string pValue) {
			// (Passport ID) - a nine-digit number, including leading zeroes.
			if(pValue.Length != 9 || !int.TryParse(pValue, out int id)) {
				return;
			}
			_validCreds++;
		}
		
		private bool InRange(in int pValue, int pMin, int pMax) {
			return pValue >= pMin && pValue <= pMax;
		}

	}
}
