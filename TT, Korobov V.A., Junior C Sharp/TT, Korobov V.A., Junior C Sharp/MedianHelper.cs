using System;
using System.Collections.Generic;
using System.Text;

namespace TT__Korobov_V.A.__Junior_C_Sharp_
{
	public class MedianHelper
	{
		private static List<long> numbers;
		private static int actionCount;

		public static int ActionCount { get { return actionCount; } }

		public MedianHelper()
		{
			numbers = new List<long>();
		}

		public void FillNumbers(object num)
		{
			ServerConnecter connecter = new ServerConnecter();
			connecter.Connect();

			string response = GetValueFromServer((int)num, connecter);

			string numberInString = string.Empty;

			foreach (char symb in response)
			{
				if (Char.IsDigit(symb))
					numberInString += symb;
			}

			if (!string.IsNullOrEmpty(numberInString))
				numbers.Add(Convert.ToInt64(numberInString));

			actionCount++;
		}

		public static double GetMedian()
		{
			var array = numbers.ToArray();
			var lenght = array.Length;

			Array.Sort(array);

			if ((lenght % 2 == 0) && lenght > 2)
			{
				var middle = lenght / 2;

				return (array[middle] + array[middle + 1]) / 2;
			}
			else if (lenght > 1)
			{
				return array[lenght/2 + 1];
			}


			return 0;
		}

		private string GetValueFromServer(int num, ServerConnecter connecter)
		{
			string response = null;

			while (string.IsNullOrEmpty(response))
			{
				connecter.Write(num.ToString());
				response = connecter.Read();

				if (string.IsNullOrEmpty(response))
					connecter.Connect();
			}

			return response;
		}
	}
}
