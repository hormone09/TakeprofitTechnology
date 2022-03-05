using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SearchTT
{
	class Program
	{
		static void Main(string[] args)
		{
			int port = 2013;
			string hostname = "88.212.241.115";
			string message = "Greetings\n";

			TcpClient client = new TcpClient();
			client.Connect(hostname, port);
			Console.WriteLine("Connected");
			NetworkStream stream = client.GetStream();

			byte[] messageInArray = System.Text.Encoding.ASCII.GetBytes(message);
			stream.Write(messageInArray);

			if (client.Connected && stream != null)
			{
				int i = 0;
				byte[] buffer = new byte[256];

				try
				{
					string result = string.Empty;
					while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
					{
						Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
						result += Encoding.GetEncoding(20866).GetString(buffer);

					}

					string writePath = @"C:\Users\" + Environment.UserName + @"\Desktop\hacked.txt";
					using (StreamWriter writer = new StreamWriter(writePath, false))
					{
						writer.Write(result);
					}
					Console.WriteLine(result);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Ошибка: {ex.Message}");
				}
			}
		}
	}
}
