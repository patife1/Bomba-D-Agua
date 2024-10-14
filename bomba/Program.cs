using System;

class Program
{
	static void Main(string[] args)
	{
		Reservatorio reservatorio = new Reservatorio();
		Bomba bomba = new Bomba(reservatorio);
		Eletrovalvula eletrovalvula = new Eletrovalvula(reservatorio);
		while (true)
		{
			Console.WriteLine("Estado do reservatório: " + reservatorio.Nivel);
			bomba.Controle();
			eletrovalvula.Controle();
			Console.WriteLine("Adicionar água? (s/n)");
			var input = Console.ReadLine();
			if (input?.ToLower() == "s")
			{
				reservatorio.AdicionarAgua(10);
			}
			else if (input?.ToLower() == "n")
			{
				Console.WriteLine("Encerrado.");
				break;
			}

			System.Threading.Thread.Sleep(1000);
		}
	}
}

class Reservatorio
{
	public int Nivel { get; private set; }

	public void AdicionarAgua(int quantidade)
	{
		Nivel += quantidade;
		Console.WriteLine("Água adicionada. Nível atual: " + Nivel);
	}

	public void RetirarAgua(int quantidade)
	{
		if (Nivel >= quantidade)
		{
			Nivel -= quantidade;
			Console.WriteLine("Água retirada. Nível atual: " + Nivel);
		}
		else
		{
			Console.WriteLine("Erro: Reservatório vazio!");
		}
	}

	public bool EstaVazio() => Nivel <= 0;
}

class Bomba
{
	private readonly Reservatorio _reservatorio;
	public Bomba(Reservatorio reservatorio)
	{
		_reservatorio = reservatorio;
	}

	public void Controle()
	{
		if (_reservatorio.EstaVazio())
		{
			Console.WriteLine("Bomba desligada: reservatório vazio.");
		}
		else if (_reservatorio.Nivel < 30)
		{
			Console.WriteLine("Bomba ligada: nível abaixo de 30.");
			_reservatorio.RetirarAgua(5);
		}
		else
		{
			Console.WriteLine("Bomba desligada: nível suficiente.");
		}
	}
}

class Eletrovalvula
{
	private readonly Reservatorio _reservatorio;
	public Eletrovalvula(Reservatorio reservatorio)
	{
		_reservatorio = reservatorio;
	}

	public void Controle()
	{
		if (_reservatorio.Nivel < 50)
		{
			Console.WriteLine("Eletrovalvula aberta: nível abaixo de 50.");
			_reservatorio.AdicionarAgua(5);
		}
		else
		{
			Console.WriteLine("Eletrovalvula fechada: nível suficiente.");
		}
	}
}