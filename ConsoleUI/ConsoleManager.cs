using CarRent.Business.Concrete;
using CarRent.DataAccess.Concrete.EntityFramework;
using CarRent.DataAccess.Concrete.InMemory;
using CarRent.Entities;
using CarRent.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent.ConsoleUI
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
                if (choice == "")
                {
                    Console.WriteLine("Wrong! Try again.");
                }
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            CarMenuScreen(carManager, brandManager, colorManager);
                            IsMainMenuOpen = true;
                            break;
                        case 2:
                            BrandMenuScreen(brandManager);
                            IsMainMenuOpen = true;
                            break;
                        case 3:
                            ColorMenuScreen(colorManager);
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
                if (choice == "")
                {
                    Console.WriteLine("Wrong! Try again.");
                }
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
                                carManager.WriteAll(carManager.GetAllCars());
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
            carManager.WriteAll(carManager.GetAllCars());

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the car you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no car registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(searchID);
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
            carManager.WriteAll(carManager.GetAllCars());

            int searchID = 0;
            bool IsExist = false;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the car you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = carManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no car registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = carManager.IsExistById(searchID);
                }
            }

            carManager.Delete(carManager.GetCarById(searchID));
        }

        private void CarMenu_GetCarsByBrandId(CarManager carManager, BrandManager brandManager)
        {
            var brandIdList = carManager.GetAllCars().Select(x => x.BrandId).Distinct();

            foreach (int id in brandIdList)
            {
                Console.WriteLine("Brand #{0} : {1}", id, brandManager.GetBrandById(id).Name);
                var carList = carManager.GetAllCarsByBrandId(id);
                carManager.WriteAll(carList);
            }
        }

        private void CarMenu_GetCarsByColorId(CarManager carManager, ColorManager colorManager)
        {
            var colorIdList = carManager.GetAllCars().Select(x => x.ColorId).Distinct();

            foreach (int id in colorIdList)
            {
                Console.WriteLine("Color #{0} : {1}", id, colorManager.GetColorById(id).Name);
                var carList = carManager.GetAllCarsByColorId(id);
                carManager.WriteAll(carList);
            }
        }

        private void BrandMenuScreen(BrandManager brandManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Brand");
                Console.WriteLine(" 2 - Update a Brand");
                Console.WriteLine(" 3 - Delete a Brand");
                Console.WriteLine(" 4 - View the List of Brands");
                Console.WriteLine(" 5 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "")
                {
                    Console.WriteLine("Wrong! Try again.");
                }
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
                            Console.WriteLine("Count of All Brands: " + brandManager.GetAllBrands().Count);
                            break;
                        case 3:
                            BrandMenu_Delete(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetAllBrands().Count);
                            break;
                        case 4:
                            if (brandManager.GetAllBrands().Count != 0)
                            {
                                Console.WriteLine("\nList of All Brands");
                                brandManager.WriteAll(brandManager.GetAllBrands());
                            }
                            Console.WriteLine("Count of All Brands: " + brandManager.GetAllBrands().Count);
                            break;
                        case 5:
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
            brandManager.WriteAll(brandManager.GetAllBrands());

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the brand you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = brandManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no brand registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = brandManager.IsExistById(searchID);
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
            brandManager.WriteAll(brandManager.GetAllBrands());

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the brand you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = brandManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no brand registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = brandManager.IsExistById(searchID);
                }
            }

            brandManager.Delete(brandManager.GetBrandById(searchID));
        }

        private void ColorMenuScreen(ColorManager colorManager)
        {
            //int id = colorManager.GetAllColors().Count + 1; // Next ID to insert

            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Color");
                Console.WriteLine(" 2 - Update a Color");
                Console.WriteLine(" 3 - Delete a Color");
                Console.WriteLine(" 4 - View the List of Color");
                Console.WriteLine(" 5 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "")
                {
                    Console.WriteLine("Wrong! Try again.");
                }
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            ColorMenu_Save(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetAllColors().Count);
                            break;
                        case 2:
                            ColorMenu_Update(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetAllColors().Count);
                            break;
                        case 3:
                            ColorMenu_Delete(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetAllColors().Count);
                            break;
                        case 4:
                            if (colorManager.GetAllColors().Count != 0)
                            {
                                Console.WriteLine("\nList of All Colors");
                                colorManager.WriteAll(colorManager.GetAllColors());
                            }
                            Console.WriteLine("Count of All Colors: " + colorManager.GetAllColors().Count);
                            break;
                        case 5:
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
            colorManager.WriteAll(colorManager.GetAllColors());

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the color you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = colorManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no color registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = colorManager.IsExistById(searchID);
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
            colorManager.WriteAll(colorManager.GetAllColors());

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the color you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = colorManager.IsExistById(searchID);
                if (!IsExist)
                {
                    Console.Write("(*) There is no color registered with this ID. Try again. ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = colorManager.IsExistById(searchID);
                }
            }

            colorManager.Delete(colorManager.GetColorById(searchID));
        }
    }
}