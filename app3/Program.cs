using Consultant;
using Microsoft.EntityFrameworkCore;

class Program
{
	static async Task Main(string[] args)
	{
		while (true)
		{
			PrintHelp();
			string choice = Console.ReadLine();

			switch (choice)
			{
				case "1":
					Console.WriteLine("Введите число");
					int.TryParse(Console.ReadLine(), out var number);
					Console.WriteLine(await CountOccurrences(number));
					break;
				case "2":
					Console.WriteLine("Введите число");
					int.TryParse(Console.ReadLine(), out var occurrences);
					var list = await ListNumbers(occurrences);
					Console.WriteLine(string.Join(",", list));
					break;
				case "3":
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
					break;
			}
		}
	}

	private static void PrintHelp()
	{
		Console.WriteLine("Выберите действие:");
		Console.WriteLine("1. Ввести число и получить количество его повторений");
		Console.WriteLine("2. Ввести количество повторений и получить перечень чисел");
		Console.WriteLine("3. Выйти");
	}

	private static async Task<int> CountOccurrences(int number)
	{
		using (var context = new DatabaseContext())
		{
			return await context.NumberStatistics
				.Where(q => q.Value == number)
				.Select(q => q.Count)
				.FirstOrDefaultAsync();
		}
	}

	private static async Task<List<int>> ListNumbers(int occurrences)
	{ 
		using (var context = new DatabaseContext())
		{
			return await context.NumberStatistics
				.Where(q => q.Count == occurrences)
				.Select(q => q.Value)
				.ToListAsync();
		}
	}
}



