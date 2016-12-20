using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IsUniqueChars : MonoBehaviour {

	public Text gt;

	void Start(){
		//gt = GetComponent<Text>();
	}

	void Update () {
		/*foreach (char c in Input.inputString) {
			if (c == "\b" [0]) {
				if (gt.text.Length != 0) {
					gt.text = gt.text.Substring (0, gt.text.Length - 1);
				}
			} else {
				if (c == "\n" [0] || c == "\r" [0]) {
					Debug.Log ("User enter their name: " + gt.text);
				} else {
					gt.text += c;
				}
			}
		}*/

		if (Input.GetKeyDown ("space")) {
			Debug.Log(isUniqueChars("abccc"));
		}
	}

	/*
		Assume ASCII char_set, otherwise the array size needs to change
		O(n) where n is length of the string, and space complexity is O(n)
	*/
	static bool isUniqueChars2(string str){
		// Create array of booleans for ASCII char set
		bool[] char_set = new bool[256];

		for (int i = 0; i < str.Length; i++) {
			// Grab the integer value of each character in the string
			int val = str [i];

			// If that integer in the char_set array has been activated, non-unique character found. 
			if (char_set [val])
				return false;

			// If not returned false yet, set the space in the char_set array to active
			char_set [val] = true;
		}

		// No non-unique character found.
		return true;
	}

	/*
		Reduce space usage by using a Bit Vector
		Assume string is restriced to lowercase a - z
		Allows me to use a single integer
	*/
	static bool isUniqueChars(string str){
		int checker = 0;

		for (int i = 0; i < str.Length; i++) {
			// Grab the integer value of each character in the string minus 'a'(97)
			// f(a) -> a - 'a' = 97 - 97 = 0
			// f(b) -> b - 'a' = 98 - 97 = 1
			// f(c) -> c - 'a' = 99 - 97 = 2
			int val = str[i] - 'a';

			// f(a) -> (0 & (1 << 0)) -> (0 & 1) = 0 > 0 
			// f(b) -> (1 & (1 << 1)) -> (1 & 2) = 0 > 0
			// f(c) -> (3 & (1 << 2)) -> (3 & 4) = 0 > 0
			// f(c) -> (7 & (1 << 2)) -> (7 & 4) = 4 > 0 !Duplicate!
			if((checker & (1 << val)) > 0)
				return false;

			// f(a) -> 0000 OR 0001 = 0001
			// f(b) -> 0001 OR 0010 = 0011
			// f(c) -> 0011 OR 0100 = 0111
			// f(c) -> 0111 OR 0011 = 0011
			checker |= (1 << val);
		}

		return true;
	}

	/* Other Options to solve problem 
	 
		1. Check every char of the string with every other char of the 
			string for duplicate occurences. Takes O(n^2) time and no space.
			
		2. If we are allowed to destroy the input string, we could sort the string
			in O(n log n) time and then linearly check the string for neighboring 
			characters that are identical. Careful though - many sorting algorithms
			tak up extra space.
	*/
}