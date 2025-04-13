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

        static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string filePath = Path.Combine(folderPath, "lhms_850005363.txt");
        static string backupFilePath = Path.Combine(folderPath, "lhms_850005363_backup.txt");

        static void Main(string[] args)
        {
            char ans;
            do
            {
                try
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
                    Console.WriteLine("7. Save the Room Allocations To a File");
                    Console.WriteLine("8. Show the Room Allocations From a File");
                    Console.WriteLine("0. Backup Allocations File");
                    Console.WriteLine("9. Exit");
                    Console.WriteLine("****************************************************************");
                    Console.Write("Enter Your Choice Number Here: ");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1: AddRooms(); break;
                        case 2: DisplayRooms(); break;
                        case 3: AllocateRoom(); break;
                        case 4: DeallocateRoom(); break;
                        case 5: DisplayRoomAllocations(); break;
                        case 6: Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!"); break;
                        case 7: SaveRoomAllocationsToFile(); break;
                        case 8: ShowRoomAllocationsFromFile(); break;
                        case 0: BackupAllocations(); break;
                        case 9: Console.WriteLine("Exiting... Thank you!"); break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }

                Console.Write("\nWould You Like To Continue(Y/N): ");
                ans = Convert.ToChar(Console.ReadLine());

            } while (ans == 'y' || ans == 'Y');
        }

        static void AddRooms()
        {
            try
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
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter numbers only.");
            }
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
            try
            {
                Console.Write("Enter Room Number to Allocate: ");
                int roomNo = Convert.ToInt32(Console.ReadLine());

                Room room = listOfRooms.Find(r => r.RoomNumber == roomNo);
                if (room == null)
                {
                    throw new InvalidOperationException("Room not found.");
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
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter numbers only.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void DeallocateRoom()
        {
            try
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
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter numbers only.");
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

        static void SaveRoomAllocationsToFile()
        {
            try
            {
                // CHANGED from append mode to overwrite mode (false) so read-only exception will occur
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine("----- Room Allocations ----- " + DateTime.Now);
                    foreach (var allocation in roomAllocations)
                    {
                        writer.WriteLine($"Room: {allocation.RoomNumber}, Customer No: {allocation.AllocatedCustomer.CustomerNumber}, Name: {allocation.AllocatedCustomer.Name}");
                    }
                    writer.WriteLine("-------------------------------\n");
                }
                Console.WriteLine("Room allocations saved to file.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access denied: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
        }

        static void ShowRoomAllocationsFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine("\n----- Data from File -----");
                    Console.WriteLine(content);
                }
                else
                {
                    throw new FileNotFoundException("No saved file found.");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }

        static void BackupAllocations()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.AppendAllText(backupFilePath, File.ReadAllText(filePath));
                    File.WriteAllText(filePath, string.Empty);
                    Console.WriteLine("Backup created and original file cleared.");
                }
                else
                {
                    Console.WriteLine("Original file not found for backup.");
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access denied: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Backup failed: " + ex.Message);
            }
        }
    }
}
