using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Services;
using ECommerce.Presentation.AdminPageUI;
using ECommerce.Presentation.UserPage;
using Raaya.Domain.Enums;

namespace ECommerce.Presentation.RegistrationUI;

public class Login
{
    UserService userService = new UserService();
    UserCreationDto user = new UserCreationDto();
    public string log = "";

 
    public async Task LoginPageAsync()
    {
        while (true)
        {
            Console.WriteLine("1.Registration");
            Console.WriteLine("2.Login");
            Console.WriteLine("3.Exit");
            Console.Write("\nChoose your option: ");
            string N = Console.ReadLine();
            switch (N)
            {

                case "1":
                    Console.Clear();
                    Console.WriteLine("\t---Please fill your info for Registration---");
                    Console.Write("Input your firstname: ");
                    user.FirstName = Console.ReadLine();
                    Console.Write("Input your lastname: ");
                    user.LastName = Console.ReadLine();
                    Console.Write("Input your email: ");
                    user.Email = Console.ReadLine();
                    Console.Write("Enter your mobile number: ");
                    user.Phone = Console.ReadLine();
                    Console.Write("Create username: ");
                    user.Login = Console.ReadLine();
                    log = user.Login;
                    Console.Write("Input your height: ");
                    user.Height = int.Parse(Console.ReadLine());
                    Console.Write("Input your weight: ");
                    user.Weight = int.Parse(Console.ReadLine());
                    Console.WriteLine("Choose your gender:" +
                        "1 = Man; 2 = Woman; 3 = Unknown;");
                    int Gender = int.Parse(Console.ReadLine());


                    Console.Write("Create your password: ");

                    user.Password = Console.ReadLine();
                    var response = await userService.CreateAsync(user);

                    if (response.Value is null)
                    {
                        Console.WriteLine("This user already exists");
                        goto case "2";
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Successfully created\n");
                        //var user = new UserPageUI();
                        //await user.UserPage();

                    }
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Enter login informations: ");
                    Console.Write("Login: ");
                    user.Login = Console.ReadLine();
                    log = user.Login;
                    Console.Write("Password: ");
                    user.Password = Console.ReadLine();
                    var response1 = await userService.ChechkForExists(user.Login, user.Password);
                    if (user.Login == "admin" && user.Password == "admin")
                    {
                        Console.Clear();
                        var admin = new AdminPage();
                        await admin.AdminPageRun();
                        return;

                    }
                    else if (response1.Value is null)
                    {
                        Console.Clear();

                        Console.WriteLine("User is not found");
                    }
                    else
                    {
                        Console.Clear();
                        var user = new UserPageUI();
                        await user.UserPage(log);
                        return;
                    }
                    break;

                case "3":
                    Console.Write("Are you sure:(Y) or (N) ");

                    string javob = Console.ReadLine();
                    if (javob.ToLower() == "y")
                    {
                        return;
                    }
                    else if (javob.ToLower() == "n")
                    {
                        Console.Clear();
                        goto case "1";
                    }
                    else
                    {
                        goto case "1";
                    }
                    
                default:
                    Console.Clear();
                    Console.WriteLine("You chose invalid number!");
                    goto case "1";
                    
            }




        }

    }
}
