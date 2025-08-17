using System.Globalization;
using System.Resources;
using ParkingLot.Common.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
ResourceManager resourceManager = new ResourceManager("ParkingLot.Common.Resources.Messages", typeof(ParkingManager).Assembly);
CultureInfo cultureInfo = CultureInfo.InvariantCulture;

bool showLanguageMenu = true;

while (showLanguageMenu) {
    Console.WriteLine("Choose your language | Escolha o idioma:");
    Console.WriteLine("1 - Português Brasileiro");
    Console.WriteLine("2 - English");
    Console.WriteLine("3 - Exit | Sair");
    switch (Console.ReadLine())
    {
        case "1":
            cultureInfo = new CultureInfo("pt-BR");
            showLanguageMenu = false;
            break;
        case "2":
        case "3":
            showLanguageMenu = false;
            break;

        default:
            Console.WriteLine("Invalid option | Opção inválida");
            break;
    }
}

decimal initialPrice = 0;
decimal pricePerHour = 0;

Console.WriteLine(resourceManager.GetString("WelcomeMessage", cultureInfo));
Console.WriteLine("Digite o preço inicial:");
initialPrice = Convert.ToDecimal(Console.ReadLine());

Console.WriteLine("Agora digite o preço por hora:");
pricePerHour = Convert.ToDecimal(Console.ReadLine());

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
ParkingManager parkingManager = new ParkingManager(initialPrice, pricePerHour);

bool showMenu = true;

// Realiza o loop do menu
while (showMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            parkingManager.AddVehicle();
            break;

        case "2":
            parkingManager.RemoveVehicle();
            break;

        case "3":
            parkingManager.ListVehicles();
            break;

        case "4":
            showMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");
