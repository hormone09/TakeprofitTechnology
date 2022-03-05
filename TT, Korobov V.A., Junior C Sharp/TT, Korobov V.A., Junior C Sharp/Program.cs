using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TT__Korobov_V.A.__Junior_C_Sharp_
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Thread> threades = new List<Thread>();
			const int threadesCount = 5;
			int endedThreadesCount = 0;


			for (int i = 1; i <= threadesCount; i++)
			{
				var helper = new MedianHelper();

				Thread newThread = new Thread(new ParameterizedThreadStart(helper.FillNumbers));
				newThread.Start(i);
				threades.Add(newThread);
			}

			while(true)
				if (MedianHelper.ActionCount == threadesCount) break;

			Console.WriteLine($"Медиана полученных чисел равна: {MedianHelper.GetMedian()}");
		}
	}
}
