﻿using Business.Concrete;
using Business.Constants;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    /* TO DO: 
     * Email-Password Control
     */
    public class ConsoleManager
    {
        public void MainScreen()
        {
            BrandManager brandManager = new BrandManager(new EFBrandDal());
            ColorManager colorManager = new ColorManager(new EFColorDal());
            CarManager carManager = new CarManager(new EFCarDal(), brandManager);
            CustomerManager customerManager = new CustomerManager(new EFCustomerDal());
            UserManager userManager = new UserManager(new EFUserDal());
            RentalManager rentalManager = new RentalManager(new EFRentalDal());

            bool IsMainMenuOpen = true;
            while (IsMainMenuOpen)
            {
                Console.WriteLine("\n-------- MAIN MENU --------");
                Console.WriteLine(" 1 - Car");
                Console.WriteLine(" 2 - Brand");
                Console.WriteLine(" 3 - Color");
                Console.WriteLine(" 4 - Customer");
                Console.WriteLine(" 5 - User");
                Console.WriteLine(" 6 - Rental");
                Console.WriteLine(" 7 - Exit");
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
                            CustomerMenuScreen(customerManager, userManager);
                            IsMainMenuOpen = true;
                            break;
                        case 5:
                            UserMenuScreen(userManager, customerManager);
                            IsMainMenuOpen = true;
                            break;
                        case 6:
                            RentalMenuScreen(rentalManager, customerManager, carManager);
                            IsMainMenuOpen = true;
                            break;
                        case 7:
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
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAll());
                            break;
                        case 2:
                            CarMenu_Update(carManager);
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAll());
                            break;
                        case 3:
                            CarMenu_Delete(carManager);
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAll());
                            break;
                        case 4:
                            CarMenu_GetCarsByBrandId(carManager, brandManager);
                            break;
                        case 5:
                            CarMenu_GetCarsByColorId(carManager, colorManager);
                            break;
                        case 6:
                            if (carManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Cars");
                                carManager.WriteAllCarDetails(carManager.GetAllCarDetails().Data);
                            }
                            Console.WriteLine("Count of All Cars: " + carManager.GetCountOfAll());
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

            var result = carManager.Add(new Car { BrandId = BrandId, ColorId = ColorId, ModelYear = ModelYear, DailyPrice = DailyPrice, Description = Description });
            Console.WriteLine(result.Message);
        }

        private void CarMenu_Update(CarManager carManager)
        {
            Console.WriteLine("\nList of All Cars");
            carManager.WriteAll(carManager.GetAll().Data);

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

            var result = carManager.Update(new Car { Id = searchID, BrandId = BrandId, ColorId = ColorId, ModelYear = ModelYear, DailyPrice = DailyPrice, Description = Description });
            Console.WriteLine(result.Message);
        }

        private void CarMenu_Delete(CarManager carManager)
        {
            Console.WriteLine("\nList of All Cars");
            carManager.WriteAll(carManager.GetAll().Data);

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

            var result = carManager.Delete(carManager.GetById(searchID).Data);
            Console.WriteLine(result.Message);
        }

        private void CarMenu_GetCarsByBrandId(CarManager carManager, BrandManager brandManager)
        {
            Console.WriteLine("\nList of All Brands");
            brandManager.WriteAll(brandManager.GetAll().Data);

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
                Console.WriteLine("List of all cars with {0} brand : ", brandManager.GetById(id).Data.Name);
                carManager.WriteAllCarDetails(brandsOfCars);
            }
            else Console.WriteLine("(-) There is no car with this brand.");
        }

        private void CarMenu_GetCarsByColorId(CarManager carManager, ColorManager colorManager)
        {
            Console.WriteLine("\nList of All Colors");
            colorManager.WriteAll(colorManager.GetAll().Data);

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
                Console.WriteLine("List of all cars with {0} color : ", colorManager.GetById(id).Data.Name);
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
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAll());
                            break;
                        case 2:
                            BrandMenu_Update(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAll());
                            break;
                        case 3:
                            BrandMenu_Delete(brandManager);
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAll());
                            break;
                        case 4:
                            if (brandManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Brands");
                                brandManager.WriteAll(brandManager.GetAll().Data);
                            }
                            Console.WriteLine("Count of All Brands: " + brandManager.GetCountOfAll());
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
            brandManager.WriteAll(brandManager.GetAll().Data);

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
            brandManager.Update(new Brand { Id = searchID, Name = Name });
        }

        private void BrandMenu_Delete(BrandManager brandManager)
        {
            Console.WriteLine("\nList of All Brands");
            brandManager.WriteAll(brandManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the brand you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = brandManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.BrandNotExist + " Try again. Brand ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = brandManager.IsExistById(searchID).Success;
                }
            }

            brandManager.Delete(brandManager.GetById(searchID).Data);
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
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAll());
                            break;
                        case 2:
                            ColorMenu_Update(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAll());
                            break;
                        case 3:
                            ColorMenu_Delete(colorManager);
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAll());
                            break;
                        case 4:
                            if (colorManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Colors");
                                colorManager.WriteAll(colorManager.GetAll().Data);
                            }
                            Console.WriteLine("Count of All Colors: " + colorManager.GetCountOfAll());
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
            colorManager.WriteAll(colorManager.GetAll().Data);

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
            colorManager.Update(new Color { Id = searchID, Name = Name });
        }

        private void ColorMenu_Delete(ColorManager colorManager)
        {
            Console.WriteLine("\nList of All Colors");
            colorManager.WriteAll(colorManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the color you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = colorManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Color ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = colorManager.IsExistById(searchID).Success;
                }
            }

            colorManager.Delete(colorManager.GetById(searchID).Data);
        }

        private void CustomerMenuScreen(CustomerManager customerManager, UserManager userManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Customer");
                Console.WriteLine(" 2 - Update a Customer");
                Console.WriteLine(" 3 - Delete a Customer");
                Console.WriteLine(" 4 - View Customer List");
                Console.WriteLine(" 5 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            CustomerMenu_Save(customerManager, userManager);
                            Console.WriteLine("Count of All Customers: " + customerManager.GetCountOfAll());
                            break;
                        case 2:
                            CustomerMenu_Update(customerManager, userManager);
                            Console.WriteLine("Count of All Customers: " + customerManager.GetCountOfAll());
                            break;
                        case 3:
                            CustomerMenu_Delete(customerManager, userManager);
                            Console.WriteLine("Count of All Customers: " + customerManager.GetCountOfAll());
                            break;
                        case 4:
                            if (customerManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Customers");
                                customerManager.WriteAll(customerManager.GetAll().Data);
                            }
                            Console.WriteLine("Count of All Customers: " + customerManager.GetCountOfAll());
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

        private void CustomerMenu_Save(CustomerManager customerManager, UserManager userManager)
        {
            Console.WriteLine("Please enter the information of the new customer.");
            Console.Write("First Name:     ");
            string FirstName = Console.ReadLine();
            Console.Write("Last Name:      ");
            string LastName = Console.ReadLine();
            Console.Write("Email Address:  ");
            string Email = Console.ReadLine();
            Console.Write("Company Name:   ");
            string CompanyName = Console.ReadLine();
            Console.Write("Password:       ");
            string Password = Console.ReadLine();   //? 

            // TO DO: Email&Password Control - if it does exists in user list, then add customer to that user.
            var user = userManager.Add(new User { FirstName = FirstName, LastName = LastName, Email = Email }).Data;
            customerManager.Add(new Customer { UserId = user.Id, CompanyName = CompanyName });
        }

        private void CustomerMenu_Update(CustomerManager customerManager, UserManager userManager)
        {
            Console.WriteLine("\nList of All Customers");
            customerManager.WriteAll(customerManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the customer you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = customerManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Customer ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = customerManager.IsExistById(searchID).Success;
                }
            }

            Console.WriteLine("Please update the information below.");
            Console.Write("First Name:     ");
            string FirstName = Console.ReadLine();
            Console.Write("Last Name:      ");
            string LastName = Console.ReadLine();
            Console.Write("Email Address:  ");
            string Email = Console.ReadLine();
            Console.Write("Company Name:   ");
            string CompanyName = Console.ReadLine();
            Console.Write("Password:       ");
            string Password = Console.ReadLine();   // Password salt and Hash

            var user = userManager.Update(new User { Id = searchID, FirstName = FirstName, LastName = LastName, Email = Email}).Data;
            customerManager.Update(new Customer { UserId = user.Id, CompanyName = CompanyName });
        }

        private void CustomerMenu_Delete(CustomerManager customerManager, UserManager userManager)
        {
            Console.WriteLine("\nList of All Customers");
            customerManager.WriteAll(customerManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the customer you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = customerManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Customer ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = customerManager.IsExistById(searchID).Success;
                }
            }

            customerManager.Delete(customerManager.GetById(searchID).Data);
        }

        private void UserMenuScreen(UserManager userManager, CustomerManager customerManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New User");
                Console.WriteLine(" 2 - Update a User");
                Console.WriteLine(" 3 - Delete a User");
                Console.WriteLine(" 4 - View User List");
                Console.WriteLine(" 5 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            UserMenu_Save(userManager);
                            Console.WriteLine("Count of All Users: " + userManager.GetCountOfAll());
                            break;
                        case 2:
                            UserMenu_Update(userManager);
                            Console.WriteLine("Count of All Users: " + userManager.GetCountOfAll());
                            break;
                        case 3:
                            UserMenu_Delete(userManager, customerManager);
                            Console.WriteLine("Count of All Users: " + userManager.GetCountOfAll());
                            break;
                        case 4:
                            if (userManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Users");
                                userManager.WriteAll(userManager.GetAll().Data);
                            }
                            Console.WriteLine("Count of All Users: " + userManager.GetCountOfAll());
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

        private void UserMenu_Save(UserManager userManager)
        {
            Console.WriteLine("Please enter the information of the new user.");
            Console.Write("First Name:     ");
            string FirstName = Console.ReadLine();
            Console.Write("Last Name:      ");
            string LastName = Console.ReadLine();
            Console.Write("Email Address:  ");
            string Email = Console.ReadLine();
            Console.Write("Password:       ");
            string Password = Console.ReadLine();   // ?

            var user = userManager.Add(new User { FirstName = FirstName, LastName = LastName, Email = Email}).Data;
        }

        private void UserMenu_Update(UserManager userManager)
        {
            Console.WriteLine("\nList of All Users");
            userManager.WriteAll(userManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the user you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = userManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. User ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = userManager.IsExistById(searchID).Success;
                }
            }

            Console.WriteLine("Please update the information below.");
            Console.Write("First Name:     ");
            string FirstName = Console.ReadLine();
            Console.Write("Last Name:      ");
            string LastName = Console.ReadLine();
            Console.Write("Email Address:  ");
            string Email = Console.ReadLine();
            Console.Write("Password:       ");
            string Password = Console.ReadLine();   //?

            var user = userManager.Update(new User { Id = searchID, FirstName = FirstName, LastName = LastName, Email = Email }).Data;
        }

        private void UserMenu_Delete(UserManager userManager, CustomerManager customerManager)
        {
            Console.WriteLine("\nList of All Users");
            userManager.WriteAll(userManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the user you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = userManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. User ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = userManager.IsExistById(searchID).Success;
                }
            }

            var user = userManager.Delete(userManager.GetById(searchID).Data).Data;
            customerManager.Delete(customerManager.GetById(user.Id).Data);
        }

        private void RentalMenuScreen(RentalManager rentalManager, CustomerManager customerManager, CarManager carManager)
        {
            bool IsMenuOpen = true;
            while (IsMenuOpen)
            {
                Console.WriteLine("\n--------- ADMIN MENU ---------");
                Console.WriteLine(" 1 - Add New Rental ");
                Console.WriteLine(" 2 - Update a Rental");
                Console.WriteLine(" 3 - Delete a Rental");
                Console.WriteLine(" 4 - View Rental List");
                Console.WriteLine(" 5 - Go back to Main Menu");
                Console.WriteLine("------------------------------");

                string choice = Console.ReadLine();
                if (choice == "") Console.WriteLine("Wrong! Try again.");
                else
                {
                    switch (Int32.Parse(choice))
                    {
                        case 1:
                            RentalMenu_Save(rentalManager, customerManager, carManager);
                            Console.WriteLine("Count of All Rentals: " + rentalManager.GetCountOfAll());
                            break;
                        case 2:
                            RentalMenu_Update(rentalManager, customerManager, carManager);
                            Console.WriteLine("Count of All Rentals: " + rentalManager.GetCountOfAll());
                            break;
                        case 3:
                            RentalMenu_Delete(rentalManager);
                            Console.WriteLine("Count of All Rentals: " + rentalManager.GetCountOfAll());
                            break;
                        case 4:
                            if (rentalManager.GetCountOfAll() != 0)
                            {
                                Console.WriteLine("\nList of All Rentals");
                                rentalManager.WriteAll(rentalManager.GetAll().Data);
                            }
                            Console.WriteLine("Count of All Rentals: " + rentalManager.GetCountOfAll());
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

        private void RentalMenu_Save(RentalManager rentalManager, CustomerManager customerManager, CarManager carManager)
        {
            Console.WriteLine("Please enter the information of the new rental.");

            Console.WriteLine("\nList of All Cars");
            carManager.WriteAll(carManager.GetAll().Data);
            Console.Write("Car ID:         ");
            int CarId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nList of All Customers");
            customerManager.WriteAll(customerManager.GetAll().Data);
            Console.Write("Customer ID:     ");
            int CustomerId = Convert.ToInt32(Console.ReadLine());

            rentalManager.Add(new Rental { CarId = CarId, CustomerId = CustomerId, RentDate = DateTime.Now, ReturnDate = null });
        }

        private void RentalMenu_Update(RentalManager rentalManager, CustomerManager customerManager, CarManager carManager)
        {
            Console.WriteLine("\nList of All Rentals");
            rentalManager.WriteAll(rentalManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the rental you want to update: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = rentalManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Rental ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = rentalManager.IsExistById(searchID).Success;
                }
            }

            if (!rentalManager.IsReturn(searchID).Success)
            {
                Console.WriteLine(Messages.CarNotReturn);

                bool isUpdated = false;
                int choice = 0;

                while (!isUpdated)
                {
                    Console.Write("Is it delivered right now? (1) Yes, (2) No  : ");
                    choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        var rental = rentalManager.GetById(searchID).Data;
                        rentalManager.Update(new Rental
                        {
                            Id = searchID,
                            CarId = rental.CarId,
                            CustomerId = rental.CustomerId,
                            RentDate = rental.RentDate,
                            ReturnDate = DateTime.Now
                        });
                        isUpdated = true;
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("No update.");
                        isUpdated = true;
                    }
                    else Console.WriteLine("Wrong! Try again.");
                }
            }
            else
                Console.WriteLine("Update is not available. The rental process is already completed.");
        }

        private void RentalMenu_Delete(RentalManager rentalManager)
        {
            Console.WriteLine("\nList of All Rentals");
            rentalManager.WriteAll(rentalManager.GetAll().Data);

            bool IsExist = false;
            int searchID = 0;

            while (!IsExist)
            {
                Console.Write("-> Enter the ID of the rental you want to delete: ");
                searchID = Convert.ToInt32(Console.ReadLine());
                IsExist = rentalManager.IsExistById(searchID).Success;
                if (!IsExist)
                {
                    Console.Write(Messages.ColorNotExist + " Try again. Rental ID: ");
                    searchID = Convert.ToInt32(Console.ReadLine());
                    IsExist = rentalManager.IsExistById(searchID).Success;
                }
            }

            rentalManager.Delete(rentalManager.GetById(searchID).Data);
        }
    }
}