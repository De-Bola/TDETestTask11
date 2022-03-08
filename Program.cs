using System;
using System.Collections.Generic;
using System.Linq;

namespace TDETestTask11
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<int, SortedSet<char>> output = new Dictionary<int, SortedSet<char>>();

			output = get_dict_by_occurences("adebolaolubunmigbiri");
			Console.WriteLine();
			Console.WriteLine("Processing output...");
			Console.WriteLine();
			printFinalOutput(output);
		}

		private static void printFinalOutput(Dictionary<int, SortedSet<char>> output)
		{
			int length = output.Keys.Count;
			List<int> keysList = output.Keys.ToList();
			keysList.Sort();
			length = keysList.Count;

			Console.Write("{");

			for (int i = 0; i < length; i++)
			{
				Console.Write(keysList[i] + ": ");
				Console.Write("['" + string.Join("', '", output[keysList[i]]) + "']");
				if (i != length - 1)
				{
					Console.Write(", ");
				}
			}

			Console.Write("}");
		}

		public static Dictionary<int, SortedSet<char>> get_dict_by_occurences(string input)
		{
			Dictionary<int, SortedSet<char>> output = new Dictionary<int, SortedSet<char>>();
			SortedSet<char> unique = new SortedSet<char>();
			int length = input.Length;

			List<int> counts;
			List<char> counted;
			countOccurrences(input, length, out counts, out counted);

			printTempOutput(counts, counted);

			length = counts.Count;

			int max = getMaxCount(counts, length);

			// add counted items to dictionary
			for (int i = 1; i <= length; i++)
			{
				unique = new SortedSet<char>();
				if (!unique.Contains(counted[i - 1]))
				{
					for (int j = 0; j < length; j++)
					{
						if ((counts[j] == i) && (!unique.Contains(counted[j])))
						{
							unique.Add(counted[j]);
						}
					}
				}

				if (!output.ContainsKey(i))
				{
					output.Add(i, unique);
				}
				if (i == max) { break; }
			}

			return output;
		}

		private static void countOccurrences(string input, int length, out List<int> counts, out List<char> counted)
		{
			counts = new List<int>();
			counted = new List<char>();
			int counter = 0;

			for (int i = 0; i < length; i++)
			{
				counter = 0;

				if (!counted.Contains((char)input[i]))
				{
					for (int j = 0; j < length; j++)
					{
						if (input[i] == input[j])
						{
							counter++;
							if (!counted.Contains((char)input[i]))
							{
								counted.Add(input[i]);
							}
						}
					}
				}
				if (counter != 0) { counts.Add(counter); }
			}
		}

		private static int getMaxCount(List<int> counts, int length)
		{
			List<int> temp = new List<int>();
			temp.AddRange(counts);
			temp.Sort();
			int max = temp[length - 1];
			return max;
		}

		private static void printTempOutput(List<int> counts, List<char> counted)
		{
			foreach (int count in counts)
			{
				Console.Write(count + " ");
			}

			Console.WriteLine();

			foreach (var letter in counted)
			{
				Console.Write(letter + " ");
			}

			Console.WriteLine();
		}
	}

}
