using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TripExpenseTracker
{
    internal class Program
    {
        private static int userIdCounter = 0;
        private static int tripIdCounter = 0;
        static int generateUserId() { return userIdCounter++; }
        static int generateTripId() { return tripIdCounter++; }
        static void Main(string[] args)
        {
            // 'database' creation
            var userDataBase = new Dictionary<int, string[]>();
            var tripDataBase = new Dictionary<int, string[]>();

            while (true)
            {
                Console.Clear();
                Console.Write("1 - Korisnici\r\n2 - Putovanja\r\n0 - Izlaz iz aplikacije\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {

                    case '1':
                        userScreen(userDataBase, tripDataBase);
                        break;

                    case '2':
                        tripScreen(userDataBase, tripDataBase);
                        break;

                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nNepoznata komanda , pritisnite enter zatim pokušajte ponovo.");
                        Console.ReadKey();
                        break;

                }

            }
        }

        static void userScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Unos novog korisnika\r\n2 - Brisanje korisnika\r\n3 - Uređivanje korisnika\r\n4 - Pregled svih korisnika\r\n0 - Povratak na glavni izbornik\r\n");
                Console.Write("Unos:");

                switch (Console.ReadKey().KeyChar)
                {

                    case '1':
                        userDataBase.Add(generateUserId(), createNewUser());
                        foreach (var user in userDataBase) {
                            Console.WriteLine($" {user.Key} - {user.Value[0]} - {user.Value[1]} - {user.Value[2]}");
                        }
                        Console.ReadKey();
                        break;

                    case '2':

                        break;

                    case '3':

                        break;

                    case '4':

                        break;

                    case '0':
                        return;
                    default:
                        Console.WriteLine("\nNepoznata komanda , pritisnite enter zatim pokušajte ponovo.");
                        Console.ReadKey();
                        break;


                }
            }
        }
        static void tripScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Unos novog putovanja\r\n2 - Brisanje putovanja\r\n3 - Uređivanje postojećeg putovanja\r\n4 - Pregled svih putovanja\r\n5 - Izvještaji i analize\r\n0 - Povratak na glavni izbornik\r\n");
                Console.Write("Unos:");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("\nNepoznata komanda , pritisnite enter zatim pokušajte ponovo.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        // takes usern input and return an array with user information
        static string[] createNewUser() {

            string firstName="", lastName="", dateOfBirth="";
            
            
            //get first name
            while (true) {
                Console.Clear();
                Console.Write("\nUnesite ime korisnika: ");
                firstName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(firstName) || firstName.Length <2 || firstName.Length >20 || ContainsSpecialCharacters(firstName)) 
                { Console.Write("\nIme nesmije biti prazno, krace od 2 slova te duze od 20!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                break;
            }
            //get laste name
            while (true)
            {
                Console.Clear();
                Console.Write("\nUnesite prezime korisnika: ");
                lastName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 20 || ContainsSpecialCharacters(lastName))
                { Console.Write("\nPrezime nesmije biti prazno, krace od 2 slova te duze od 20!!! Pritisnite enter te pokusajte ponovno");Console.ReadKey(); continue; }
                break;
            }
            //get date of birth
            while (true)
            {
                Console.Clear();
                Console.Write("Unesite datum rodenja u formatu dd-mm-yyyy: ");
                dateOfBirth = Console.ReadLine();
                try
                {
                    DateTime dateOfBirthParased = DateTime.ParseExact(dateOfBirth, "dd-MM-yyyy", null);
                    if (dateOfBirthParased > DateTime.Now || dateOfBirthParased < DateTime.Now.AddYears(-100))
                    {
                        Console.Write("\nUneseni datum je u buducnosti ili je previse u proslosti!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    return new string[] { firstName, lastName, dateOfBirth };

                }
                catch (Exception)
                {
                    Console.Write("\nNeispravan format datuma!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                }
                
            }

        }
        /* 
         * 
         * 
         * helper functions
         * 
         */

        // checks if string contains special characters
        static bool ContainsSpecialCharacters(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
