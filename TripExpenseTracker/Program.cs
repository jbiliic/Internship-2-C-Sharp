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
            
            var userDataBase = new Dictionary<int, string[]>();
            var tripDataBase = new Dictionary<int, string[]>();
            var userTripRelationship = new Dictionary<int, List<int>>();
            userDataBase.Add(generateUserId(), new string[] { "Josip" , "Bilic" , "26-06-2003" });
            userDataBase.Add(generateUserId(), new string[] { "foo", "foocic", "26-06-2003" });
            userDataBase.Add(generateUserId(), new string[] { "batman", "superhero", "26-06-2003" });
            userDataBase.Add(generateUserId(), new string[] { "ante", "antic", "26-06-2018" });

            while (true)
            {
                Console.Clear();
                Console.Write("1 - Korisnici\r\n2 - Putovanja\r\n0 - Izlaz iz aplikacije\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {

                    case '1':
                        userScreen(userDataBase, tripDataBase, userTripRelationship);
                        break;

                    case '2':
                        tripScreen(userDataBase, tripDataBase, userTripRelationship);
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

        static void userScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
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
                        Console.WriteLine("Korisnik stvoren!!!");
                        Console.ReadKey();
                        break;

                    case '2':
                        deleteUser(userDataBase);
                        break;

                    case '3':
                        editUser(userDataBase);
                        break;

                    case '4':
                        printUsersScreen(userDataBase);
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
        static void tripScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Unos novog putovanja\r\n2 - Brisanje putovanja\r\n3 - Uređivanje postojećeg putovanja\r\n4 - Pregled svih putovanja\r\n5 - Izvještaji i analize\r\n0 - Povratak na glavni izbornik\r\n");
                Console.Write("Unos:");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        int tripId = generateTripId();
                        tripDataBase.Add(tripId, createNewTrip(userDataBase, tripDataBase, userTripRelationship, tripId));
                        Console.WriteLine("Putovanje uspjesno dodano");
                        Console.ReadKey();
                        break;
                    case '2':
                        deleteTripScreen(tripDataBase,userTripRelationship);
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
        /* 
         * 
         * 
         * 
         * 
         * 
         * 
         * user screen functions
         * 
         */

        static string[] createNewUser() {

            var firstName = getAndValidateInputStr("Ime");
            var lastName = getAndValidateInputStr("Prezime");
            var dateOfBirth = getAndValidateInputDate("Datum rođenja");
            return new string[] { firstName, lastName, dateOfBirth };
        }
        static void deleteUser(Dictionary<int, string[]> userDataBase) {
            while (true) {
                Console.Clear();
                Console.Write("1-Brisanje po id \n2-Brisanje po imenu i prezimenu \n0-Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        
                        var inputId = getAndValidateUserId(userDataBase);
                        userDataBase.Remove(inputId);
                        break;
                    case '2':
                        var firstName = getAndValidateInputStr("Ime");
                        var lastName = getAndValidateInputStr("Prezime");
                        foreach (var user in userDataBase.ToList()) 
                        {
                            if (firstName.ToUpper().Equals(user.Value[0].ToUpper()) && lastName.ToUpper().Equals(user.Value[1].ToUpper()))
                            {
                                userDataBase.Remove(user.Key);
                                Console.WriteLine("\nKorisnik uspjesno izbrisan");
                                Console.ReadKey();
                                return;
                            }
                        }
                        Console.WriteLine("\nNavedeni korisnik ne postoji");
                        Console.ReadKey();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Krivi unos");
                        break;
                }
            }
        }
        static void editUser(Dictionary<int, string[]> userDataBase) {
            var inputId = getAndValidateUserId(userDataBase);
            userDataBase[inputId] = createNewUser();
        }
        static void printUsersScreen(Dictionary<int, string[]> userDataBase) {
            while (true) {
                Console.Clear();
                Console.Write("1-Ispis po prezimenu sortirano\n2-Ispis svih starijih od 20g\n3-Ispis svih sa >2 putovanja\n0-Povratak\nUnos:");
                switch (Console.ReadKey().KeyChar) { 
                    case '1':
                        Console.Clear();
                        var sortedByLastName = userDataBase.OrderBy(user => user.Value[1]);
                        foreach (var user in sortedByLastName)
                        {
                            Console.WriteLine($"{user.Key} - {user.Value[0]} - {user.Value[1]} - {user.Value[2]}");
                        }
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        var usersOlderThan20 = userDataBase.Where(user => DateTime.ParseExact(user.Value[2], "dd-MM-yyyy", null) > DateTime.Now.AddYears(-20));
                        foreach (var user in usersOlderThan20)
                        {
                            Console.WriteLine($"{user.Key} - {user.Value[0]} - {user.Value[1]} - {user.Value[2]}");
                        }
                        Console.ReadKey();
                        break;
                    case '3':
                        break;
                    case '0':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nPritisnite enter zatim pokušajte ponovo.");
                        Console.ReadKey();
                        break;
                }
            }
        }
        /* 
         * 
         * 
         * trip screen functions
         * 
         */

        static string[] createNewTrip(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship,int tripId)
        {
            Console.Clear();
            
            var inputId = getAndValidateUserId(userDataBase).ToString();
            var dateOfTrip = getAndValidateInputDate("datum putovanja");
            var distance = getAndValidateInputInt("prijeđenu udaljenost u kilometrima");
            var fuelUsage = getAndValidateInputInt("potrošnju goriva u litrama");
            var costPerL = getAndValidateInputInt("cijenu goriva po litri");

            var fuelUsageParsed = float.Parse(fuelUsage);
            var costPerLParsed = float.Parse(costPerL);
            var costOfTrip = (fuelUsageParsed * costPerLParsed).ToString();

            if (userTripRelationship.ContainsKey(int.Parse(inputId)))
            {
                userTripRelationship[int.Parse(inputId)].Add(tripId);
            }
            else 
            {
                userTripRelationship[int.Parse(inputId)] = new List<int> { tripId };
            }


            return new string[] {dateOfTrip, distance , fuelUsage, costPerL, costOfTrip};
        }
        static void deleteTripScreen(Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true) { 
                Console.Clear();
                Console.Write("\n1-Brisanje putovanja po ID\n2-Brisanje putavanja koja su skuplja od unesenog iznosa\n3-Brisanje putavanja koja su jeftinija od unesenog iznosa\n0-Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var tripId = getAndValidateTripId(tripDataBase);
                        tripDataBase.Remove(tripId);
                        deleteTripUserRelationship(tripId, userTripRelationship);
                        Console.WriteLine("Uspijesno izbrisano");
                        Console.ReadKey();
                        break;
                    case '2':
                        deleteTripsByCost(tripDataBase, userTripRelationship, 1);
                        Console.WriteLine("Uspijesno izbrisano");
                        Console.ReadKey();
                        break;
                    case '3':
                        deleteTripsByCost(tripDataBase, userTripRelationship, -1);
                        Console.WriteLine("Uspijesno izbrisano");
                        Console.ReadKey();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("Krivi unos");
                        break;
                }
            }
        }
        
        




        /* 
         * 
         * 
         * helper functions
         * 
         */


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
        static void deleteTripUserRelationship(int tripId, Dictionary<int, List<int>> userTripRelationship)
        {
            foreach (var userTrips in userTripRelationship)
            {
                if (userTrips.Value.Contains(tripId))
                {
                    userTrips.Value.Remove(tripId);
                    return;
                }
            }
        }
        static string getAndValidateInputStr(string inputType) {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 2 || toReturn.Length > 20 || ContainsSpecialCharacters(toReturn))
                { Console.Write($"\n{toReturn} nesmije biti prazno, krace od 2 slova te duze od 20!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                return toReturn;
            }
        }
        static string getAndValidateInputDate(string inputType) {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType} u formatu dd-mm-yyyy: ");
                var toReturn = Console.ReadLine();
                try
                {
                    DateTime toReturnParased = DateTime.ParseExact(toReturn, "dd-MM-yyyy", null);
                    if (toReturnParased > DateTime.Now || toReturnParased < DateTime.Now.AddYears(-100))
                    {
                        Console.Write("\nUneseni datum je u buducnosti ili je previse u proslosti!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    return toReturn;
                }
                catch (Exception)
                {
                    Console.Write("\nNeispravan format datuma!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                }
            }
        }
        static string getAndValidateInputInt(string inputType) {
            while (true)
            {
                Console.Clear();
                Console.Write($"Unesite {inputType}: ");
                var toReturn = Console.ReadLine();
                try
                {
                    var toReturnParsed = float.Parse(toReturn);
                    if (toReturnParsed <= 0)
                    {
                        Console.Write("\nBroj mora biti veca od 0!!! Pritisnite enter te pokusajte ponovno");
                        Console.ReadKey();
                        continue;
                    }
                    return toReturn;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.Write("\nNeispravan format!!! Pritisnite enter te pokusajte ponovno");
                    Console.ReadKey();
                    continue;
                }
            }
        }
        static int getAndValidateUserId(Dictionary<int, string[]> userDataBase) {
            while (true)
            {
                Console.Clear();
                Console.Write("\nUnesite id korisnika koji putuje: ");
                var inputId = Console.ReadLine();
                if (int.TryParse(inputId, out int userId))
                {
                    if (userDataBase.ContainsKey(userId)) { return userId; }
                    else
                    {
                        Console.WriteLine("Korisnik s unesenim id-em ne postoji!!!");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Unos nije valjan pokusajte ponovno");
                    Console.ReadKey();
                    continue;
                }
            }
        }
        static int getAndValidateTripId(Dictionary<int, string[]> tripDataBase){
            while (true)
            {
                Console.Clear();
                Console.Write("\nUnesite id putovanja: ");
                var inputId = Console.ReadLine();
                if (int.TryParse(inputId, out int tripId))
                {
                    if (tripDataBase.ContainsKey(tripId)) { return tripId; }
                    else
                    {
                        Console.WriteLine("Putovanje s unesenim id-em ne postoji!!!");
                        Console.ReadKey();
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Unos nije valjan pokusajte ponovno");
                    Console.ReadKey();
                    continue;
                }
            }
        }
        static void deleteTripsByCost(Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship,int multiplyer) {
            var inputCostStr = getAndValidateInputInt("iznos");
            var inputCost = float.Parse(inputCostStr);
            foreach (var trip in tripDataBase.ToList())
            {
                var tripCost = float.Parse(trip.Value[4]);
                if (multiplyer*inputCost < multiplyer*tripCost)
                {
                    tripDataBase.Remove(trip.Key);
                    deleteTripUserRelationship(trip.Key, userTripRelationship);
                }
            }
            Console.WriteLine("Uspijesno izbrisano");
            Console.ReadKey();
        }
    }
}
