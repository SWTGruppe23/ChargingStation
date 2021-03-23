using System;
using ChargingStation.Door;
using ChargingStation.IdReader;


namespace ChargingStation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IIdReader rfidReader = new RfidReader();
            IDoor door = new Door.Door();


            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OpenDoor();
                        break;

                    case 'C':
                        door.CloseDoor();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.ReadId(id);
                        break;
                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
