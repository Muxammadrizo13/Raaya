
using ECommerce.Domain.Enums;
using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Services;
using Raaya.Domain.Enums;

namespace ECommerce.Presentation.AdminPageUI
{
    public class AdminPage
    {
        ClothesService clothesService = new ClothesService();
        OrderService orderService = new OrderService();

        public async Task AdminPageRun()
        {
            while (true)
            {
                Console.WriteLine("                      ");
                Console.WriteLine("                        Welcome to Admin Page                    ");
                Console.WriteLine("                                                                  ");
                Console.WriteLine("                     1 ----- Create new Clothes                    ");
                Console.WriteLine("                     2 ----- GetAll Clothes                       ");
                Console.WriteLine("                     3 ----- GetById Clothes                        ");
                Console.WriteLine("                     4 ----- Update Clothes                        ");
                Console.WriteLine("                     5 ----- Delete Clothes                          ");
                Console.WriteLine("                     6 -----  GetAllOrders                            ");
                Console.WriteLine("                     7 ----- Change Statues                            ");
                Console.WriteLine("                     10 ----- To exit                                 ");

                int num = int.Parse(Console.ReadLine());

                if (num == 1)
                {
                    Console.Clear();
                    Console.Write("Enter Clothes Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Clothes Price: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Description: ");
                    string description = Console.ReadLine();
                    Console.Write("Enter Clothes Count: ");
                    int count = int.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Size: ");
                    ClothesSize size = (ClothesSize)int.Parse(Console.ReadLine()); 
                    Console.Write("Enter Clothes Type: ");
                    ClothesType type = (ClothesType)int.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Brand: ");
                    ClothesBrand brand = (ClothesBrand)int.Parse(Console.ReadLine());

                    ClothesCreationDto clothesCreationDto = new ClothesCreationDto
                    {
                        Name = name,
                        Price = price,
  
                        Description = description,
                        Count = count,
                        Type = type,
                        Size = size,
                        Brand = brand
                        
                        
                    };
                    var repo = await clothesService.CreateAsync(clothesCreationDto);
                    Console.Clear();
                    Console.WriteLine(repo.Message);
                    //Console.Clear();
                }
                else if (num == 2)
                {
                    Console.Clear();

                    var clotheses = await clothesService.GetAllAsync(x => x.Id > 0);
                    foreach (var clothes in clotheses.Value)
                    {
                        Console.WriteLine($"Id: {clothes.Id}");
                        Console.WriteLine($"Name: {clothes.Name}");
                        Console.WriteLine($"Price: {clothes.Price}");
                        Console.WriteLine($"Description: {clothes.Describtion}");
                        Console.WriteLine($"Count: {clothes.Count}");
                        Console.WriteLine($"Type: {clothes.Type}");
                        Console.WriteLine($"Size: {clothes.Size}");
                        Console.WriteLine($"Size: {clothes.Brand}");


                    }

                }
                else if (num == 3)
                {
                    Console.Clear();
                    Console.Write("Enter Product Id: ");
                    long id = long.Parse(Console.ReadLine());
                    
                    var product = await clothesService.GetByIdAsync(x => x.Id == id);
                    Console.WriteLine($"Id: {product.Value.Id}");
                    Console.WriteLine($"Name: {product.Value.Name}");
                    Console.WriteLine($"Price: {product.Value.Price}");
                    Console.WriteLine($"Description: {product.Value.Describtion}");
                    Console.WriteLine($"Count: {product.Value.Count}");
                    Console.WriteLine($"Type: {product.Value.Type}");
                    Console.WriteLine($"Size: {product.Value.Size}");


                }
                else if (num == 4)
                {
                    Console.Clear();

                    Console.Write("Enter Clothes Id:  ");
                    long id = long.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Name:  ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Clothes Price:  ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Description:  ");
                    string description = Console.ReadLine();
                    Console.Write("Enter Clothes Count:  ");
                    int count = int.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Type:  ");
                    ClothesType type = (ClothesType)int.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Size:  ");
                    ClothesSize size = (ClothesSize)int.Parse(Console.ReadLine());
                    Console.Write("Enter Clothes Brand:  ");
                    ClothesBrand brand = (ClothesBrand)int.Parse(Console.ReadLine());


                    ClothesCreationDto ClothesCreationDto = new ClothesCreationDto
                    {
                        Name = name,
                        Price = price,
                        Description = description,
                        Count = count,
                        Type = type,
                        Size = size,
                        Brand = brand
                    };
                   
                    await clothesService.UpdateAsync(x => x.Id == id, ClothesCreationDto);
                    Console.Clear();
                }
                else if (num == 5)
                {
                    Console.Clear();

                    Console.Write("Enter Clothes Id:  ");
                    long id = long.Parse(Console.ReadLine());
                    
                    await clothesService.DeleteAsync(x => x.Id == id);

                }
                else if(num == 6)
                {
                    Console.Clear();

                    var res = await orderService.GetAllAsync(x => x.Id > 0);
                    foreach(var item in res.Value)
                    {
                        Console.WriteLine($"Adress {item.Adress}");
                        Console.WriteLine($"Paymnet {item.Payment.IsPaid}");
                        Console.WriteLine($"Status {item.Status}");
                        var les = item.Products;
                        foreach(var utem in les)
                        {
                            Console.WriteLine(utem.Name);
                            Console.WriteLine(utem.Price);
                            Console.WriteLine(utem.Type);
                            Console.WriteLine(utem.Size);
                            Console.WriteLine(utem.Count);
                            Console.WriteLine(utem.Describtion);
                            Console.WriteLine(utem.UpdatedAt);
                            Console.WriteLine(utem.Brand);
                        }

                    }
                }
                else if(num == 7)
                {
                    Console.Clear();

                    Console.Write("Enter Order Id:  ");
                    long id = long.Parse(Console.ReadLine());
                    Console.Write("Enter Status:  ");
                    OrderStatus status = (OrderStatus)int.Parse(Console.ReadLine());
                    // await orderService.ChangeStatusAsync(id, status); shu funksiyani yozib quyish kerak
                }
                else if(num == 10)
                {
                    return;
                }
                else
                {
                    Console.WriteLine($"There is no service numbered {num}");
                }

            }
        }
    }
}
