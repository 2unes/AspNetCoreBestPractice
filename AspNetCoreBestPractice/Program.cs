using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace AspNetCoreBestPractice
{
	public class Program
	{
		public static void Main(string[] args)
		{
            //try{
				BuildWebHost(args).Run();
            //}catch(Exception ex){
            //    Console.WriteLine(ex.Message);
            //}
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
