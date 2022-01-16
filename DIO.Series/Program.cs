using DIO.Series;
SerieRepositorio repositorio = new SerieRepositorio();
string opcaoUsuario = ObterOpcaoUsuario();

while (opcaoUsuario.ToUpper() != "X")
{
	switch (opcaoUsuario)
	{
		case "1":
			ListarSeries();
			break;
		case "2":
			InserirSerie();
			break;
		case "3":
			AtualizarSerie();
			break;
		case "4":
			ExcluirSerie();
			break;
		case "5":
			VisualizarSerie();
			break;
		case "C":
			Console.Clear();
			break;

		default:
            throw new ArgumentOutOfRangeException();
	}

	opcaoUsuario = ObterOpcaoUsuario();
}

Console.WriteLine("Obrigado por utilizar nossos serviços.");
Console.ReadLine();



void ListarSeries()
{
	Console.WriteLine("Listar séries");

	var lista = repositorio.Lista();

	if (lista.Count == 0)
	{
		Console.WriteLine("Nenhuma série cadastrada.");
		return;
	}

	foreach (var serie in lista)
	{
		var excluido = serie.RetornaExcluido();

		Console.WriteLine("#ID {0}: - {1} {2}", serie.RetornaId(), serie.RetornaTitulo(), (excluido ? "*Excluído*" : ""));
	}
}

void InserirSerie()
{
	Console.WriteLine("Inserir nova série");

	MostrarGeneros();

	Console.Write("Digite o gênero entre as opções acima: ");
	int entradaGenero = int.Parse(Console.ReadLine());

	Console.Write("Digite o Título da Série: ");
	string entradaTitulo = Console.ReadLine();

	Console.Write("Digite o Ano de Início da Série: ");
	int entradaAno = int.Parse(Console.ReadLine());

	Console.Write("Digite a Descrição da Série: ");
	string entradaDescricao = Console.ReadLine();

	Serie novaSerie = new Serie(id: repositorio.ProximoId(),
								genero: (Genero)entradaGenero,
								titulo: entradaTitulo,
								ano: entradaAno,
								descricao: entradaDescricao);

	repositorio.Insere(novaSerie);
}

void AtualizarSerie()
{
	Console.Write("Digite o id da série: ");
	int indiceSerie = int.Parse(Console.ReadLine());

	MostrarGeneros();

	Console.Write("Digite o gênero entre as opções acima: ");
	int entradaGenero = int.Parse(Console.ReadLine());
	var serie_antiga = repositorio.RetornaPorId(indiceSerie);
	if (serie_antiga != null)
	{
		Console.Write("Digite o Título da Série: ");
		string entradaTitulo = Console.ReadLine();

		Console.Write("Digite o Ano de Início da Série: ");
		int entradaAno = int.Parse(Console.ReadLine());

		Console.Write("Digite a Descrição da Série: ");
		string entradaDescricao = Console.ReadLine();

		Serie atualizaSerie = new Serie(id: indiceSerie,
									genero: (Genero)entradaGenero,
									titulo: entradaTitulo,
									ano: entradaAno,
									descricao: entradaDescricao);

		repositorio.Atualiza(indiceSerie, atualizaSerie);

	}
	else
		throw new ArgumentOutOfRangeException($"Id {indiceSerie} não encontrado");
}

void ExcluirSerie()
{
	Console.Write("Digite o id da série: ");
	int indiceSerie = int.Parse(Console.ReadLine());
	Console.WriteLine("Deseja mesmo excluir a série: ");
	var serie = repositorio.RetornaPorId(indiceSerie);
	if (serie != null)
	{
		Console.WriteLine(serie);
		Console.WriteLine("Digite sim para confirmar: ");
		string resposta = Console.ReadLine();
		if (resposta == "sim")
		{
			repositorio.Excluir(indiceSerie);
			Console.WriteLine("Série excluída com sucesso!");
		}
		else
		{
			Console.WriteLine("Série não excluida");
		}
	}
	else
    {
		throw new ArgumentOutOfRangeException($"Id {indiceSerie} não encontrado");
    }
	
}

void VisualizarSerie()
{
	Console.Write("Digite o id da série: ");
	int indiceSerie = int.Parse(Console.ReadLine());
	
	var serie = repositorio.RetornaPorId(indiceSerie);
	if (serie != null)
		Console.WriteLine(serie);
	else
		throw new ArgumentOutOfRangeException($"Id {indiceSerie} não encontrado");
}

string ObterOpcaoUsuario()
{
	Console.WriteLine();
	Console.WriteLine("DIO Séries a seu dispor!!!");
	Console.WriteLine("Informe a opção desejada:");

	Console.WriteLine("1- Listar séries");
	Console.WriteLine("2- Inserir nova série");
	Console.WriteLine("3- Atualizar série");
	Console.WriteLine("4- Excluir série");
	Console.WriteLine("5- Visualizar série");
	Console.WriteLine("C- Limpar Tela");
	Console.WriteLine("X- Sair");
	Console.WriteLine();

	string opcaoUsuario = Console.ReadLine().ToUpper();
	Console.WriteLine();
	return opcaoUsuario;
}

void MostrarGeneros()
{
	// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
	// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
	//Percorre os generos declarados no Enum e mostra no console
	foreach (int i in Enum.GetValues(typeof(Genero)))
	{
		Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
	}
}
