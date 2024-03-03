using System;
namespace INVENTORY_MANAGER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Device Management System");
            Console.WriteLine("---------------------------------------");

            while (true)
            {
                Console.WriteLine("\nMenu:");

                Console.WriteLine("1. View Database Entries");
                Console.WriteLine("2. Add New Entry");
                Console.WriteLine("3. Edit Entry");
                Console.WriteLine("4. Remove Entry");
                Console.WriteLine("5. Exit");

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Thread.Sleep(500);
                        ViewDatabaseEntries();
                        break;
                    case "2":
                        Thread.Sleep(500);
                        AddNewEntry();
                        break;
                    case "3":
                        Thread.Sleep(500);
                        EditEntry();
                        break;
                    case "4":
                        Thread.Sleep(500);
                        RemoveEntry();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void ViewDatabaseEntries()
        {
            Console.WriteLine("\nLoading database entries...");
            Thread.Sleep(1000); // Delay for 1 second

            Console.WriteLine("\nDevice Database Entries:");
            Console.WriteLine("{0,-5} {1,-15} {2,-20} {3,-15} {4,-20} {5,-15} {6,-15} {7,-15} {8,-15} {9,-15}", "ID", "Device Type", "Device Name", "Serial Number", "Issued Person", "ID Number", "Date Issued", "Date Due", "Received", "Date Returned");
            Console.WriteLine("===============================================================================================================================================================");

            using (var db = new DeviceDbContext())
            {
                var devices = db.Devices.ToList();
                if (devices.Any())
                {
                    foreach (var device in devices)
                    {
                        Console.WriteLine("{0,-5} {1,-15} {2,-20} {3,-15} {4,-20} {5,-15} {6,-15} {7,-15} {8,-15} {9,-15}", device.Id, device.DeviceType, device.DeviceName, device.SerialNumber, device.IssuedPersonName, device.IssuedPersonIdNumber, device.DateIssued.ToString("yyyy-MM-dd"), device.DateDue.ToString("yyyy-MM-dd"), device.Received ? "Yes" : "No", device.DateReturned.HasValue ? device.DateReturned.Value.ToString("yyyy-MM-dd") : "Not returned");
                    }
                }
                else
                {
                    Console.WriteLine("No entries found in the database.");
                }
            }
        }


        static void AddNewEntry()
        {
            Console.WriteLine("\nEnter Device Information:");
            Thread.Sleep(500); // Delay for 500 milliseconds

            Console.Write("Device Type: ");
            string deviceType = Console.ReadLine();

            Console.Write("Device Name: ");
            string deviceName = Console.ReadLine();

            Console.Write("Serial Number: ");
            string serialNumber = Console.ReadLine();

            Console.Write("Issued Person's Name: ");
            string issuedPersonName = Console.ReadLine();

            Console.Write("Issued Person's ID Number: ");
            string issuedPersonIdNumber = Console.ReadLine();

            Console.Write("Date Issued (yyyy-MM-dd): ");
            DateTime dateIssued;
            if (!DateTime.TryParse(Console.ReadLine(), out dateIssued))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            Console.Write("Date Due (yyyy-MM-dd): ");
            DateTime dateDue;
            if (!DateTime.TryParse(Console.ReadLine(), out dateDue))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            // Create a new Device object
            var newDevice = new Device
            {
                DeviceType = deviceType,
                DeviceName = deviceName,
                SerialNumber = serialNumber,
                IssuedPersonName = issuedPersonName,
                IssuedPersonIdNumber = issuedPersonIdNumber,
                DateIssued = dateIssued,
                DateDue = dateDue,
                Received = false
            };

            // Save the device information to the database
            using (var db = new DeviceDbContext())
            {
                db.Database.EnsureCreated(); // Create the database if it doesn't exist
                db.Devices.Add(newDevice);
                db.SaveChanges();
            }

            Console.WriteLine("\nDevice information has been successfully saved to the database!");
        }

        static void EditEntry()
        {
            static void EditEntry()
            {
                Console.Write("\nEnter the ID of the device to edit: ");
                int deviceId;
                if (!int.TryParse(Console.ReadLine(), out deviceId))
                {
                    Console.WriteLine("Invalid device ID.");
                    return;
                }

                using (var db = new DeviceDbContext())
                {
                    var device = db.Devices.FirstOrDefault(d => d.Id == deviceId);
                    if (device == null)
                    {
                        Console.WriteLine("Device not found.");
                        return;
                    }

                    Console.WriteLine("\nCurrent Device Information:");
                    Console.WriteLine($"ID: {device.Id}, Type: {device.DeviceType}, Name: {device.DeviceName}, Serial Number: {device.SerialNumber}, Issued Person: {device.IssuedPersonName} (ID: {device.IssuedPersonIdNumber}), Date Issued: {device.DateIssued.ToString("yyyy-MM-dd")}, Date Due: {device.DateDue.ToString("yyyy-MM-dd")}, Received: {(device.Received ? "Yes" : "No")}, Date Returned: {(device.DateReturned.HasValue ? device.DateReturned.Value.ToString("yyyy-MM-dd") : "Not returned")}");

                    Console.WriteLine("\nEnter New Device Information:");

                    Console.Write("Device Type: ");
                    string deviceType = Console.ReadLine();

                    Console.Write("Device Name: ");
                    string deviceName = Console.ReadLine();

                    Console.Write("Serial Number: ");
                    string serialNumber = Console.ReadLine();

                    Console.Write("Issued Person's Name: ");
                    string issuedPersonName = Console.ReadLine();

                    Console.Write("Issued Person's ID Number: ");
                    string issuedPersonIdNumber = Console.ReadLine();

                    Console.Write("Date Issued (yyyy-MM-dd): ");
                    DateTime dateIssued;
                    if (!DateTime.TryParse(Console.ReadLine(), out dateIssued))
                    {
                        Console.WriteLine("Invalid date format.");
                        return;
                    }

                    Console.Write("Date Due (yyyy-MM-dd): ");
                    DateTime dateDue;
                    if (!DateTime.TryParse(Console.ReadLine(), out dateDue))
                    {
                        Console.WriteLine("Invalid date format.");
                        return;
                    }

                    Console.Write("Received (true/false): ");
                    bool received;
                    if (!bool.TryParse(Console.ReadLine(), out received))
                    {
                        Console.WriteLine("Invalid input for 'Received'. Please enter 'true' or 'false'.");
                        return;
                    }

                    // If received is true, prompt for the date returned
                    DateTime? dateReturned = null;
                    if (received)
                    {
                        Console.Write("Date Returned (yyyy-MM-dd): ");
                        if (!DateTime.TryParse(Console.ReadLine(), out DateTime tempDateReturned))
                        {
                            Console.WriteLine("Invalid date format.");
                            return;
                        }
                        dateReturned = tempDateReturned;
                    }

                    // Update device information
                    device.DeviceType = deviceType;
                    device.DeviceName = deviceName;
                    device.SerialNumber = serialNumber;
                    device.IssuedPersonName = issuedPersonName;
                    device.IssuedPersonIdNumber = issuedPersonIdNumber;
                    device.DateIssued = dateIssued;
                    device.DateDue = dateDue;
                    device.Received = received;
                    device.DateReturned = dateReturned;

                    db.SaveChanges();

                    Console.WriteLine("\nDevice information has been successfully updated.");
                }
            }

        }

        static void RemoveEntry()
        {
            Console.Write("\nEnter the ID of the device to remove: ");
            int deviceId;
            if (!int.TryParse(Console.ReadLine(), out deviceId))
            {
                Console.WriteLine("Invalid device ID.");
                return;
            }

            using (var db = new DeviceDbContext())
            {
                var device = db.Devices.FirstOrDefault(d => d.Id == deviceId);
                if (device == null)
                {
                    Console.WriteLine("Device not found.");
                    return;
                }

                Console.WriteLine("\nAre you sure you want to remove the following device?");
                Console.WriteLine($"ID: {device.Id}, Type: {device.DeviceType}, Name: {device.DeviceName}, Serial Number: {device.SerialNumber}, Issued Person: {device.IssuedPersonName} (ID: {device.IssuedPersonIdNumber})");
                Console.Write("Enter 'yes' to confirm: ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    db.Devices.Remove(device);
                    db.SaveChanges();

                    Console.WriteLine("\nDevice has been successfully removed from the database.");
                }
                else
                {
                    Console.WriteLine("\nDeletion canceled.");
                }
            }
        }

        static void ProcessReturn()
        {
            Console.Write("\nEnter the ID of the device to process return: ");
            int deviceId;
            if (!int.TryParse(Console.ReadLine(), out deviceId))
            {
                Console.WriteLine("Invalid device ID.");
                return;
            }

            using (var db = new DeviceDbContext())
            {
                var device = db.Devices.FirstOrDefault(d => d.Id == deviceId);
                if (device == null)
                {
                    Console.WriteLine("Device not found.");
                    return;
                }

                Console.WriteLine("\nAre you sure you want to mark the following device as returned?");
                Console.WriteLine($"ID: {device.Id}, Type: {device.DeviceType}, Name: {device.DeviceName}, Serial Number: {device.SerialNumber}, Issued Person: {device.IssuedPersonName} (ID: {device.IssuedPersonIdNumber})");
                Console.Write("Enter 'yes' to confirm: ");
                string confirmation = Console.ReadLine();

                if (confirmation.ToLower() == "yes")
                {
                    device.Received = true;
                    device.DateReturned = DateTime.Now;
                    db.SaveChanges();

                    Console.WriteLine("\nDevice has been successfully marked as returned.");
                }
                else
                {
                    Console.WriteLine("\nProcess return canceled.");
                }
            }
        }
    }
}