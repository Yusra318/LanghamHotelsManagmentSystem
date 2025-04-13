/*
* Project Name: LANGHAM Hotel Management System
* Author Name: Yusra Al-Murtadha
* Date: 13/04/2025
* Application Purpose: Implement Hotel Management Application System for the client “LANGHAM Hotels”
*
*/
using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNumber { get; set; }
        public bool IsAllocated { get; set; }

        public Room(int number)
        {
            RoomNumber = number;
            IsAllocated = false;
        }
    }

    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNumber { get; set; }
        public string Name { get; set; }

        public Customer(int number, string name)
        {
            CustomerNumber = number;
            Name = name;
        }
    }

    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int RoomNumber { get; set; }
        public Customer AllocatedCustomer { get; set; }

        public RoomAllocation(int roomNumber, Customer customer)
        {
            RoomNumber = roomNumber;
            AllocatedCustomer = customer;
        }
    }

    // Custom Main Class - Program
    class Program
    {
        public static List<Room> listOfRooms = new List<Room>();
        public static List<RoomAllocation> roomAllocations = new List<RoomAllocation>();

        static void Main(string[] args)
        {
            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("****************************************************************");
                Console.WriteLine("               LANGHAM HOTEL MANAGEMENT SYSTEM");
                Console.WriteLine("                             MENU");
                Console.WriteLine("****************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("9. Exit");
                Console.WriteLine("****************************************************************");
                Console.Write("Enter Your Choice Number Here: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddRooms();
                        break;
                    case 2:
                        DisplayRooms();
                        break;
                    case 3:
                        AllocateRoom();
                        break;
                    case 4:
                        DeallocateRoom();
                        break;
                    case 5:
                        DisplayRoomAllocations();
                        break;
                    case 6:
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                        break;
                    case 9:
                        Console.WriteLine("Exiting... Thank you!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                Console.Write("\nWould You Like To Continue(Y/N): ");
                ans = Convert.ToChar(Console.ReadLine());

            } while (ans == 'y' || ans == 'Y');
        }

        static void AddRooms()
        {
            Console.Write("Enter number of rooms to add: ");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.Write("Enter Room Number: ");
                int roomNo = Convert.ToInt32(Console.ReadLine());
                listOfRooms.Add(new Room(roomNo));
            }
            Console.WriteLine("Room(s) Added Successfully!");
        }

        static void DisplayRooms()
        {
            Console.WriteLine("List of Rooms:");
            foreach (Room room in listOfRooms)
            {
                Console.WriteLine($"Room No: {room.RoomNumber}, Allocated: {room.IsAllocated}");
            }
        }

        static void AllocateRoom()
        {
            Console.Write("Enter Room Number to Allocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            Room room = listOfRooms.Find(r => r.RoomNumber == roomNo);
            if (room == null)
            {
                Console.WriteLine("Room not found.");
                return;
            }
            if (room.IsAllocated)
            {
                Console.WriteLine("Room is already allocated.");
                return;
            }

            Console.Write("Enter Customer Number: ");
            int custNo = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Customer Name: ");
            string custName = Console.ReadLine();

            Customer customer = new Customer(custNo, custName);
            roomAllocations.Add(new RoomAllocation(roomNo, customer));
            room.IsAllocated = true;

            Console.WriteLine("Room allocated successfully.");
        }

        static void DeallocateRoom()
        {
            Console.Write("Enter Room Number to Deallocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            Room room = listOfRooms.Find(r => r.RoomNumber == roomNo);
            if (room != null && room.IsAllocated)
            {
                roomAllocations.RemoveAll(ra => ra.RoomNumber == roomNo);
                room.IsAllocated = false;
                Console.WriteLine("Room de-allocated successfully.");
            }
            else
            {
                Console.WriteLine("Room is not allocated or does not exist.");
            }
        }

        static void DisplayRoomAllocations()
        {
            Console.WriteLine("Room Allocation Details:");
            foreach (RoomAllocation ra in roomAllocations)
            {
                Console.WriteLine($"Room No: {ra.RoomNumber}, Customer No: {ra.AllocatedCustomer.CustomerNumber}, Name: {ra.AllocatedCustomer.Name}");
            }
        }
    }
}
