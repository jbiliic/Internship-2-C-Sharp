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
            // Putovanje 1 - Josip (id 0)
            int tripId1 = generateTripId();
            tripDataBase.Add(tripId1, new string[] { "15-05-2024", "350", "28", "1.65", "46.20" });
            userTripRelationship[0] = new List<int> { tripId1 };

            // Putovanje 2 - Josip (id 0)
            int tripId2 = generateTripId();
            tripDataBase.Add(tripId2, new string[] { "22-06-2024", "180", "15", "1.70", "25.50" });
            userTripRelationship[0].Add(tripId2);

            // Putovanje 3 - foo (id 1)
            int tripId3 = generateTripId();
            tripDataBase.Add(tripId3, new string[] { "10-07-2024", "520", "42", "1.60", "67.20" });
            userTripRelationship[1] = new List<int> { tripId3 };

            // Putovanje 4 - batman (id 2)
            int tripId4 = generateTripId();
            tripDataBase.Add(tripId4, new string[] { "03-04-2024", "1200", "95", "1.75", "166.25" });
            userTripRelationship[2] = new List<int> { tripId4 };

            // Putovanje 5 - ante (id 3)
            int tripId5 = generateTripId();
            tripDataBase.Add(tripId5, new string[] { "18-08-2024", "75", "6", "1.68", "10.08" });
            userTripRelationship[3] = new List<int> { tripId5 };

            // Putovanje 6 - Josip (id 0) - još jedno putovanje
            int tripId6 = generateTripId();
            tripDataBase.Add(tripId6, new string[] { "30-09-2024", "420", "35", "1.62", "56.70" });
            userTripRelationship[0].Add(tripId6);

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
                        var inputTripId = getAndValidateTripId(tripDataBase);
                        tripDataBase[inputTripId] = createNewTrip(userDataBase, tripDataBase, userTripRelationship, inputTripId);
                        break;
                    case '4':
                        printTripsScreen(tripDataBase);
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
                if(!userTripRelationship[int.Parse(inputId)].Contains(tripId))
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
                        Console.ReadKey();
                        break;
                    case '2':
                        deleteTripsByCost(tripDataBase, userTripRelationship, 1);
                        Console.ReadKey();
                        break;
                    case '3':
                        deleteTripsByCost(tripDataBase, userTripRelationship, -1);
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
        static void printTripsScreen(Dictionary<int, string[]> tripDataBase) {
            while (true) {
                Console.SetCursorPosition(0, 0);
                Console.Clear();
                Console.Write("1-Sva putovanja\n2-Sva putovanja sortirana po trošku uzlazno\r\n3-Sva  putovanja sortirana po trošku silazno\r\n");
                Console.Write("4-Sva  putovanja sortirana po kilometraži uzlazno\r\n5-Sva  putovanja sortirana po kilometraži silazno\r\n6-Sva  putovanja sortirana po datumu uzlazno\r\n");
                Console.Write("7-Sva  putovanja sortirana po datumu silazno\r\n0-Povratak\nUnos:");

                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        foreach (var trip in tripDataBase)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '2':
                        Console.Clear();
                        var sortedByCostAsc = tripDataBase.OrderBy(trip => float.Parse(trip.Value[4]));
                        foreach (var trip in sortedByCostAsc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '3':
                        Console.Clear();
                        var sortedByCostDesc = tripDataBase.OrderByDescending(trip => float.Parse(trip.Value[4]));
                        foreach (var trip in sortedByCostDesc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '4':
                        Console.Clear();
                        var sortedByDistanceAsc = tripDataBase.OrderBy(trip => float.Parse(trip.Value[1]));
                        foreach (var trip in sortedByDistanceAsc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '5':
                        Console.Clear();
                        var sortedByDistanceDesc = tripDataBase.OrderByDescending(trip => float.Parse(trip.Value[1]));
                        foreach (var trip in sortedByDistanceDesc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '6':
                        Console.Clear();
                        var sortedByDateAsc = tripDataBase.OrderBy(trip => DateTime.ParseExact(trip.Value[0], "dd-MM-yyyy", null));
                        foreach (var trip in sortedByDateAsc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '7':
                        Console.Clear();
                        var sortedByDateDesc = tripDataBase.OrderByDescending(trip => DateTime.ParseExact(trip.Value[0], "dd-MM-yyyy", null));
                        foreach (var trip in sortedByDateDesc)
                        {
                            Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
                        }
                        Console.ReadKey();
                        Console.Clear();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                    case '0':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nPogresan unos. Pritisnite enter zatim pokušajte ponovo.");
                        Console.ReadLine();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
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
        }
    }
}
