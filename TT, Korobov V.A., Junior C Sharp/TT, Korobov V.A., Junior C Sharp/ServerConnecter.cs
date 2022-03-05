using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TT__Korobov_V.A.__Junior_C_Sharp_
{
	public class ServerConnecter
	{
		private const string IP = "88.212.241.115";
		private const int Port = 2013;

		private TcpClient Client { get; set; }
		private NetworkStream Stream { get; set; }

		public void Connect()
		{
			Client = new TcpClient();

			try
			{
				Client.Connect(IP, Port);
				Console.WriteLine("+++ Connected +++");
				Stream = Client.GetStream();
			}
			catch (Exception)
			{
				Console.WriteLine("!!! Failed to connect !!!");
				Connect();
			}
		}

		public void Write(string message)
		{
			message += "\n";
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);

			try
			{
				Stream.WriteAsync(bytes);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"!!! Failed! Message: {ex.Message} !!!");
				Connect();
				Write(message);
			}
		}

		public string Read()
		{
			if (Client.Connected && Stream != null)
			{
				int i = 0;
				byte[] buffer = new byte[256];

				try
				{
					string response = string.Empty;

					while ((i = Stream.Read(buffer, 0, buffer.Length)) != 0)
					{
						Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
						response += Encoding.GetEncoding(20866).GetString(buffer);

						CheckResponse(response);
					}

					return response;
				}
				catch (Exception ex)
				{
					Console.WriteLine($"!!! Failed! Message: {ex.Message} !!!");

					return null;
				}
			}

			return null;
		}

		public bool CheckResponse(string response)
		{
			Write($"Check {response}\n");
			Console.WriteLine(Read());


			return true;
		}
	}
}
