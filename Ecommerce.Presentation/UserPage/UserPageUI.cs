
using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Services;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Presentation.RegistrationUI;
using Raaya.Domain.Enums;

namespace ECommerce.Presentation.UserPage;

public class UserPageUI
{
    UserService userService = new UserService();
    ClothesService productService = new ClothesService();
    CartService cartService = new CartService();
    Login login = new Login();
    PaymentService payment = new PaymentService();

    

    public async Task UserPage(string log)
    {
        
        
        var userId = (await userService.GetByIdAsync(x => x.Login == log)).Value.Id;
        
        start:
        Console.WriteLine("\t\t-- User Page --");
        Console.WriteLine("1. Search clothes ");
        Console.WriteLine("2. Show categories ");
        Console.WriteLine("3. My Cart ");
        Console.WriteLine("4. Exit ");
        Console.WriteLine("5. Orders ");

        Console.Write("\nChoose one option: ");
        int press = int.Parse(Console.ReadLine());

        switch (press)
        {
            case 1:
                Console.Clear();
                Console.Write("Enter clothes' name: ");
                string name = Console.ReadLine();
                var response = await productService.GetAllAsync(x => x.Name == name);
                if (response.Value is null)
                    Console.WriteLine("Not Found!");
                else
                {
                    var items = new List<long>();
                    foreach (var item in response.Value)
                    {
                        items.Add(item.Id);
                    }
                    choose:
                    foreach (var item in response.Value)
                    {
                        
                        Console.WriteLine($"{item.Id} | {item.Name} | {item.Price} | {item.Type} | {item.Size}");
                    }

                    Console.Write("\nEnter product's id to add to cart: ");
                    long id = long.Parse(Console.ReadLine());
                    foreach (var item in response.Value)
                    {
                        if (id == item.Id)
                        {

                            var a = await cartService.AddProductAsync(userId, item);
          

                            Console.WriteLine("\nAdded to cart");
                            Console.Write("\nEnter 4 to return: ");
                            Console.Write("\nEnter 0 to return to Main Menu: ");


                            int pres = int.Parse(Console.ReadLine());
                            if (pres == 4)
                            {
                                Console.Clear();
                                goto choose;
                            }
                            else if (pres == 0)
                            {
                                Console.Clear();
                                goto start;
                            }

                        }
                        
                        
                    }
                    
                    
                }
                break;

            case 2:
                Console.Clear();
                Console.WriteLine("1.Sport\n2.Kids\n3.Men\n4.Women\n5.Uniform\n6.Sale\n");

                Console.Write("Choose one option: ");
                int choose = int.Parse(Console.ReadLine());

                var response2 = await productService.GetAllAsync(x => x.Type == (ClothesType)choose);

                if (response2.Value is null)
                    Console.WriteLine(response2.Message);
                else
                {
                    
                    var items = new List<long>();
                    foreach (var item in response2.Value)
                    {
                        items.Add(item.Id);
                    }
                choose:
                    foreach (var item in response2.Value)
                    {

                        Console.WriteLine($"{item.Id} | {item.Name} | {item.Price} | {item.Type}");
                    }
                    if (items.Count != 0)
                    {
                        Console.Write("\nEnter product's id to add to cart: ");
                        long id = long.Parse(Console.ReadLine());
                        foreach (var item in response2.Value)
                        {
                            if (id == item.Id)
                            {

                                await cartService.AddProductAsync(userId, item);

                                Console.WriteLine("\nAdded to cart");
                                Console.Write("\nEnter 4 to return: ");
                                Console.Write("\nEnter 0 to return to Main Menu: ");


                                int pres = int.Parse(Console.ReadLine());
                                if (pres == 4)
                                {
                                    Console.Clear();
                                    goto choose;
                                }
                                else if (pres == 0)
                                {
                                    Console.Clear();
                                    goto start;
                                }

                            }
                        }
                    }
                    else
                    {
                        Console.Write("\nEnter 0 to return to Main Menu: ");


                        int pres = int.Parse(Console.ReadLine());
                        if (pres == 4)
                        {
                            Console.Clear();
                            goto choose;
                        }
                        else if (pres == 0)
                        {
                            Console.Clear();
                            goto start;
                        }
                    }

                }
                break;

            case 3:
                Console.Clear();
                var response3 = await cartService.GetCartAsync(x => x.UserId == userId);
                if (response3.Value is null)
                    Console.WriteLine(response3.Message);
                else
                {
                    decimal summ = 0;   
                    var products = response3.Value.Products;

                    if (products is null)
                    {
                        Console.WriteLine("Your cart is empty");
                        Console.Write("\nEnter 0 to return to Main Menu: ");
                        int retur = int.Parse(Console.ReadLine());
                        if (retur == 0)
                        {
                            Console.Clear();
                            goto start;
                        }
                    }


                    foreach (var product in products)
                    {
                        summ += product.Price;
                        Console.WriteLine($"{product.Id} | {product.Name} | {product.Price} | {1} | " +
                            $"{product.Type}");
                    }

                    Console.WriteLine($"\nSumm: {summ}");


                    Console.Write($"Enter 1 to buy: ");
                    Console.Write("\nEnter 0 to return to Main Menu: ");
                    int buy = int.Parse(Console.ReadLine());
                    if (buy == 0)
                    {
                        Console.Clear();
                        goto start;
                    }
                    else if (buy == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("1.Click\n2.Payme\n");

                        Console.Write("Choose one option: ");
                        int choose2 = int.Parse(Console.ReadLine());
                        var paymentDto = new PaymentCreationDto()
                        {
                            IsPaid = true,
                            Type = (PaymentType)choose2,
                            UserId= userId,

                        };


                        var response4 = await payment.CreateAsync(paymentDto);
                        Console.WriteLine($"\n{response4.Message}");

                        Console.Write("\nEnter 0 to return to Main Menu: ");
                        int retur = int.Parse(Console.ReadLine());
                        if (retur == 0)
                        {
                            Console.Clear();
                            goto start;
                        }


                    }
                }
                break;
            
            
        }



    }
}
