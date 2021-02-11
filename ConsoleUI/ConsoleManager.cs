using Business.Concrete;
using Business.Constants;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    public class ConsoleManager
    {
        public void MainScreen()
        {
            CarManager carManager = new CarManager(new EFCarDal());
            BrandManager brandManager = new BrandManager(new EFBrandDal());
            ColorManager colorManager = new ColorManager(new EFColorDal());

            bool IsMainMenuOpen = true;
            while (IsMainMenuOpen)
            {
                Console.WriteLine("\n-------- MAIN MENU --------");
                Console.WriteLine(" 1 - Car");
                Console.WriteLine(" 2 - Brand");
                Console.WriteLine(" 3 - Color");
                Console.WriteLine(" 4 - Exit");
                Console.WriteLine("-----------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            CarMenuScreen(carManager, brandManager, colorManager);
                            IsMainMenuOpen = true;
                            break;
                        case 2:
                            BrandMenuScreen(brandManager, carManager);
                            IsMainMenuOpen = true;
                            break;
                        case 3:
                            ColorMenuScreen(colorManager, carManager);
                            IsMainMenuOpen = true;
                            break;
                        case 4:
                            IsMainMenuOpen = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            Console.WriteLine("\nGoodbye..");
        }

        private void CarMenuScreen(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Car");
                Console.WriteLine(" 2 - Update a Car");
                Console.WriteLine(" 3 - Delete a Car");
                Console.WriteLine(" 4 - View Cars by Brand Id");
                Console.WriteLine(" 5 - View Cars by Color Id");
                Console.WriteLine(" 6 - View the List of Cars");
                Console.WriteLine(" 7 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            CarMenu_Save(carManager);
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAllCars());
                            break;
                        case 2:
                            CarMenu_Update(carManager);
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAllCars());
                            break;
                        case 3:
                            CarMenu_Delete(carManager);
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAllCars());
                            break;
                        case 4:
                            CarMenu_GetCarsByBrandId(carManager, brandManager);
                            break;
                        case 5:
                            CarMenu_GetCarsByColorId(carManager, colorManager);
                            break;
                        case 6:
                            if (carManager.GetCountOfAllCars() != 0)
                            {
                                Console.WriteLine("\nList of All Cars");
                                carManager.WriteAllCarDetails(carManager.GetAllCarDetails().Data);
                            }
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAllCars());
                            break;
                        case 7:
                            IsMenuOpen = false;
                            break;
                        default:
                            Console.WriteLine("Wrong! Try again.");
                            break;
                    }
                }
            }
        }

        private void CarMenu_Save(CarManager carManager)
        {
            Console.WriteLine("Please enter the information of the new car.");
            Console.Write("BrandId:     ");
            int BrandId = Convert.ToInt32(Console.ReadLine());
            Console.Write("ColorId:     ");
            int ColorId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Model Year:  ");
            int ModelYear = Convert.ToInt32(Console.ReadLine());
            Console.Write("Price:       ");
            decimal DailyPrice = Convert.ToDecimal(Console.ReadLine().Replace(".", ","));
            Console.Write("Description: ");
            string Description = Console.ReadLine();

            carManager.Add(new Car { BrandId = BrandId, ColorId = ColorId, ModelYear = ModelYear, DailyPrice = DailyPrice, Description = Description });
        }

        private void CarMenu_Update(CarManager carManager)
        {
            Console.WriteLine("\nList of All Cars");
            carManager.WriteAll(carManager.GetAllCars().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the car you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.CarNotExist + " Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(searchID).Success;
                }
            }

            Console.WriteLine("Please update the information below.");
            Console.Write("BrandId:     ");
            int BrandId = Convert.ToInt32(Console.ReadLine());

            Console.Write("ColorId:     ");
            int ColorId = Convert.ToInt32(Console.ReadLine());

            Console.Write("Model Year:  ");
            int ModelYear = Convert.ToInt32(Console.ReadLine());

            Console.Write("Price:       ");
            decimal DailyPrice = Convert.ToDecimal(Console.ReadLine().Replace(".", ","));

            Console.Write("Description: ");
            string Description = Console.ReadLine();

            carManager.Update(new Car { BrandId = BrandId, ColorId = ColorId, ModelYear = ModelYear, DailyPrice = DailyPrice, Description = Description });
        }

        private void CarMenu_Delete(CarManager carManager)
        {
            Console.WriteLine("\nList of All Cars");
            carManager.WriteAll(carManager.GetAllCars().Data);

            int searchID = 0;
            bool IsExist = false;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the car you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.CarNotExist + " Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(searchID).Success;
                }
            }

            carManager.Delete(carManager.GetCarById(searchID).Data);
        }

        private void CarMenu_GetCarsByBrandId(CarManager carManager, BrandManager brandManager)
        {
            Console.WriteLine("\nList of All Brands");
            brandManager.WriteAll(brandManager.GetAllBrands().Data);

            bool IsExist = false;
            int id = 0;

            while (!IsExist)
            {
                Console.Write("Choose a Brand ID: ");
                id = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(id).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.BrandNotExist + " Try again. Brand ID: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(id).Success;
                }
            }
            var brandsOfCars = carManager.GetAllCarDetails(x => x.ColorId == id).Data;
            if (brandsOfCars.Count != 0)
            {
                Console.WriteLine("List of all cars with {0} brand : ", brandManager.GetBrandById(id).Data.Name);
                carManager.WriteAllCarDetails(brandsOfCars);
            }
            else Console.WriteLine("(-) There is no car with this brand.");
        }

        private void CarMenu_GetCarsByColorId(CarManager carManager, ColorManager colorManager)
        {
            Console.WriteLine("\nList of All Colors");
            colorManager.WriteAll(colorManager.GetAllColors().Data);

            bool IsExist = false;
            int id = 0;

            while (!IsExist)
            {
                Console.Write("Choose a Color ID: ");
                id = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(id).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Color ID: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(id).Success;
                }
            }
            var colorsOfCars = carManager.GetAllCarDetails(x => x.ColorId == id).Data;
            if (colorsOfCars.Count != 0)
            {
                Console.WriteLine("List of all cars with {0} color : ", colorManager.GetColorById(id).Data.Name);
                carManager.WriteAllCarDetails(colorsOfCars);
            }
            else Console.WriteLine("(-) There is no car with this color.");
        }

        private void BrandMenuScreen(BrandManager brandManager, CarManager carManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Brand");
                Console.WriteLine(" 2 - Update a Brand");
                Console.WriteLine(" 3 - Delete a Brand");
                Console.WriteLine(" 4 - View the List of Brands");
                Console.WriteLine(" 5 - View Cars by Brand Id");
                Console.WriteLine(" 6 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            BrandMenu_Save(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAllBrands());
                            break;
                        case 2:
                            BrandMenu_Update(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAllBrands());
                            break;
                        case 3:
                            BrandMenu_Delete(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAllBrands());
                            break;
                        case 4:
                            if (brandManager.GetCountOfAllBrands() != 0)
                            {
                                Console.WriteLine("\nList of All Brands");
                                brandManager.WriteAll(brandManager.GetAllBrands().Data);
                            }
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAllBrands());
                            break;
                        case 5:
                            CarMenu_GetCarsByBrandId(carManager, brandManager);
                            break;
                        case 6:
                            IsMenuOpen = false;
                            break;
                        default:
                            Console.WriteLine("Wrong! Try again.");
                            break;
                    }
                }
            }
        }

        private void BrandMenu_Save(BrandManager brandManager)
        {
            Console.WriteLine("Please enter the information of the new brand.");
            Console.Write("Name:     ");
            string Name = Console.ReadLine();
            brandManager.Add(new Brand { Name = Name });
        }

        private void BrandMenu_Update(BrandManager brandManager)
        {
            Console.WriteLine("\nList of All Brands");
            brandManager.WriteAll(brandManager.GetAllBrands().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the brand you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = brandManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.BrandNotExist + " Try again. Brand ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = brandManager.IsExistById(searchID).Success;
                }
            }

            Console.WriteLine("Please update the information below.");
            Console.Write("Name:     ");
            string Name = Console.ReadLine();
            brandManager.Update(new Brand { Name = Name });
        }

        private void BrandMenu_Delete(BrandManager brandManager)
        {
            Console.WriteLine("\nList of All Brands");
            brandManager.WriteAll(brandManager.GetAllBrands().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the brand you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = brandManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.BrandNotExist + " Try again. Brand ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = brandManager.IsExistById(searchID).Success;
                }
            }

            brandManager.Delete(brandManager.GetBrandById(searchID).Data);
        }

        private void ColorMenuScreen(ColorManager colorManager, CarManager carManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Color");
                Console.WriteLine(" 2 - Update a Color");
                Console.WriteLine(" 3 - Delete a Color");
                Console.WriteLine(" 4 - View the List of Color");
                Console.WriteLine(" 5 - View Cars by Color Id");
                Console.WriteLine(" 6 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            ColorMenu_Save(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAllColors());
                            break;
                        case 2:
                            ColorMenu_Update(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAllColors());
                            break;
                        case 3:
                            ColorMenu_Delete(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAllColors());
                            break;
                        case 4:
                            if (colorManager.GetCountOfAllColors() != 0)
                            {
                                Console.WriteLine("\nList of All Colors");
                                colorManager.WriteAll(colorManager.GetAllColors().Data);
                            }
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAllColors());
                            break;
                        case 5:
                            CarMenu_GetCarsByColorId(carManager, colorManager);
                            break;
                        case 6:
                            IsMenuOpen = false;
                            break;
                        default:
                            Console.WriteLine("Wrong! Try again.");
                            break;
                    }
                }
            }
        }

        private void ColorMenu_Save(ColorManager colorManager)
        {
            Console.WriteLine("Please enter the information of the new color.");
            Console.Write("Name:     ");
            string Name = Console.ReadLine();
            colorManager.Add(new Color { Name = Name });
        }

        private void ColorMenu_Update(ColorManager colorManager)
        {
            Console.WriteLine("\nList of All Colors");
            colorManager.WriteAll(colorManager.GetAllColors().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the color you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = colorManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Color ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = colorManager.IsExistById(searchID).Success;
                }
            }

            Console.WriteLine("Please update the information below.");
            Console.Write("Name:     ");
            string Name = Console.ReadLine();
            colorManager.Update(new Color { Name = Name });
        }

        private void ColorMenu_Delete(ColorManager colorManager)
        {
            Console.WriteLine("\nList of All Colors");
            colorManager.WriteAll(colorManager.GetAllColors().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the color you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = colorManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Color ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = colorManager.IsExistById(searchID).Success;
                }
            }

            colorManager.Delete(colorManager.GetColorById(searchID).Data);
        }
    }
}