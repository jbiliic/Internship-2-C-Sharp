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
            //testing data
            userDataBase.Add(generateUserId(), new string[] { "Josip", "Bilic", "26-06-2003" });
            userDataBase.Add(generateUserId(), new string[] { "foo", "foocic", "26-06-2008" });
            userDataBase.Add(generateUserId(), new string[] { "batman", "superhero", "26-06-2000" });
            userDataBase.Add(generateUserId(), new string[] { "ante", "antic", "26-06-2018" });
            userDataBase.Add(generateUserId(), new string[] { "aaaa", "antaaaaaic", "26-06-2018" });
            
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
                Console.Write("1 - Korisnici\r\n2 - Putovanja\r\n3 - Statistika\n0 - Izlaz iz aplikacije\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        userScreen(userDataBase, tripDataBase, userTripRelationship);
                        break;
                    case '2':
                        tripScreen(userDataBase, tripDataBase, userTripRelationship);
                        break;
                    case '3':
                        statsScreen(userDataBase, tripDataBase, userTripRelationship);
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
                        if (userDataBase.Count != 0)
                        {
                            deleteUser(userDataBase, tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema korisnika za brisanje");
                            Console.ReadKey();
                        }
                        break;

                    case '3':
                        if (userDataBase.Count != 0)
                        {
                            editUser(userDataBase);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema korisnika za uredivanje");
                            Console.ReadKey();
                        }
                        break;

                    case '4':


                        if (userDataBase.Count != 0)
                        {
                            printUsersScreen(userDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema korisnika za ispisivanje");
                            Console.ReadKey();
                        }
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

                        if (userDataBase.Count != 0)
                        {
                            int tripId = generateTripId();
                            tripDataBase.Add(tripId, createNewTrip(userDataBase, tripDataBase, userTripRelationship, tripId));
                            Console.WriteLine("Putovanje uspjesno dodano");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema korisnika pa se putovanje nemoze dodati");
                            Console.ReadKey();
                        }

                        break;
                    case '2':

                        if (tripDataBase.Count != 0)
                        {
                            deleteTripScreen(tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema putovanja za obrisati");
                            Console.ReadKey();
                            break;
                        }
                        break;
                    case '3':
                        if (tripDataBase.Count != 0)
                        {
                            var inputTripId = getAndValidateTripId(tripDataBase);
                            var newTripData = createNewTrip(userDataBase, tripDataBase, userTripRelationship, inputTripId);
                            tripDataBase[inputTripId] = newTripData;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema putovanja za urediti");
                            Console.ReadKey();
                            break;
                        }

                        break;
                    case '4':


                        if (tripDataBase.Count != 0)
                        {
                            printTripsScreen(tripDataBase);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nema putovanja za ispisati  ");
                            Console.ReadKey();
                            break;
                        }
                        break;
                    case '5':
                        analasysScreen(userDataBase, tripDataBase, userTripRelationship);
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

        static string[] createNewUser()
        {

            var firstName = getAndValidateInputStr("Ime");
            var lastName = getAndValidateInputStr("Prezime");
            var dateOfBirth = getAndValidateInputDate("Datum rođenja");
            return new string[] { firstName, lastName, dateOfBirth };
        }
        static void deleteUser(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1-Brisanje po id \n2-Brisanje po imenu i prezimenu \n0-Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var inputId = getAndValidateUserId(userDataBase);

                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Jeste Li sigurni?(Da/Ne)\nInput:");
                            switch (Console.ReadLine().ToString())
                            {
                                case "Da":
                                case "da":
                                case "DA":
                                    userDataBase.Remove(inputId);
                                    if (userTripRelationship.ContainsKey(inputId))
                                        foreach (var tripId in userTripRelationship[inputId])
                                        {
                                            tripDataBase.Remove(tripId);
                                            userTripRelationship.Remove(inputId);
                                        }
                                    Console.WriteLine("Korisnik uspijesno izbrisan");
                                    Console.ReadKey();
                                    break;
                                case "Ne":
                                case "ne":
                                case "NE":
                                    Console.WriteLine("Radnja odbacena");
                                    Console.ReadKey();
                                    break;
                                default:
                                    Console.WriteLine("Neispravan unos pokusajte ponovno"); Console.ReadKey(); break;
                            }
                            break;
                        }
                        break;
                        
                    case '2':
                        var firstName = getAndValidateInputStr("Ime");
                        var lastName = getAndValidateInputStr("Prezime");
                        foreach (var user in userDataBase.ToList())
                        {
                            if (firstName.ToUpper().Equals(user.Value[0].ToUpper()) && lastName.ToUpper().Equals(user.Value[1].ToUpper()))
                            {
                                while (true)
                                {
                                    Console.Clear();
                                    Console.Write("Jeste Li sigurni?(Da/Ne)\nInput:");
                                    switch (Console.ReadLine().ToString())
                                    {
                                        case "Da":
                                        case "da":
                                        case "DA":
                                            userDataBase.Remove(user.Key);
                                            if (userTripRelationship.ContainsKey(user.Key))
                                                foreach (var tripId in userTripRelationship[user.Key])
                                                {
                                                    tripDataBase.Remove(tripId);
                                                    userTripRelationship.Remove(user.Key);
                                                }
                                            Console.WriteLine("Korisnik uspijesno izbrisan");
                                            Console.ReadKey();
                                            return;
                                        case "Ne":
                                        case "ne":
                                        case "NE":
                                            Console.WriteLine("Radnja odbacena");
                                            Console.ReadKey();
                                            return;
                                        default:
                                            Console.WriteLine("Neispravan unos pokusajte ponovno"); Console.ReadKey(); break;
                                    }
                                }
                                return;
                            }
                        }
                        Console.WriteLine("\nNavedeni korisnik ne postoji");
                        Console.ReadKey();
                        break;
                    case '0':
                        return;
                    default:
                        Console.WriteLine("\nKrivi unos , pokusajte ponovno");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void editUser(Dictionary<int, string[]> userDataBase)
        {
            var inputId = getAndValidateUserId(userDataBase);
            var editedUserData = createNewUser();
            while (true)
            {
                Console.Clear();
                Console.Write("Jeste Li sigurni?(Da/Ne)\nInput:");
                switch (Console.ReadLine().ToString())
                {
                    case "Da":
                    case "da":
                    case "DA":
                        userDataBase[inputId] = editedUserData;
                        Console.WriteLine("Korisnik uspijesno ureden");
                        Console.ReadKey();
                        return;
                    case "Ne":
                    case "ne":
                    case "NE":
                        Console.WriteLine("Radnja odbacena");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Neispravan unos pokusajte ponovno"); Console.ReadKey(); break;
                }
            }
        }
        static void printUsersScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1-Ispis po prezimenu sortirano\n2-Ispis svih starijih od 20g\n3-Ispis svih sa >2 putovanja\n0-Povratak\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
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
                        var usersOlderThan20 = userDataBase.Where(user => DateTime.ParseExact(user.Value[2], "dd-MM-yyyy", null) < DateTime.Now.AddYears(-20));
                        foreach (var user in usersOlderThan20)
                        {
                            Console.WriteLine($"{user.Key} - {user.Value[0]} - {user.Value[1]} - {user.Value[2]}");
                        }
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        foreach (var user in userDataBase)
                        {
                            if (userTripRelationship.ContainsKey(user.Key) && userTripRelationship[user.Key].Count > 2)
                            {
                                Console.WriteLine($"{user.Key} - {user.Value[0]} - {user.Value[1]} - {user.Value[2]}");
                            }
                        }
                        Console.ReadKey();
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
        static string[] createNewTrip(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship, int tripId)
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
                if (!userTripRelationship[int.Parse(inputId)].Contains(tripId))
                    userTripRelationship[int.Parse(inputId)].Add(tripId);
            }
            else
            {
                userTripRelationship[int.Parse(inputId)] = new List<int> { tripId };
            }
            return new string[] { dateOfTrip, distance, fuelUsage, costPerL, costOfTrip };
        }
        static void deleteTripScreen(Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("\n1-Brisanje putovanja po ID\n2-Brisanje putavanja koja su skuplja od unesenog iznosa\n3-Brisanje putavanja koja su jeftinija od unesenog iznosa\n0-Povratak\nUnos: ");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var tripId = getAndValidateTripId(tripDataBase);

                        while (true)
                        {
                            Console.Clear();
                            Console.Write("Jeste Li sigurni?(Da/Ne)\nInput:");
                            switch (Console.ReadLine().ToString())
                            {
                                case "Da":
                                case "da":
                                case "DA":
                                    tripDataBase.Remove(tripId);
                                    deleteTripUserRelationship(tripId, userTripRelationship);
                                    Console.WriteLine("Putovanje uspijesno izbrisano");
                                    Console.ReadKey();
                                    return;
                                case "Ne":
                                case "ne":
                                case "NE":
                                    Console.WriteLine("Radnja odbacena");
                                    Console.ReadKey();
                                    return;
                                default:
                                    Console.WriteLine("Neispravan unos pokusajte ponovno"); Console.ReadKey(); break;
                            }
                        }
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
        static void printTripsScreen(Dictionary<int, string[]> tripDataBase)
        {
            while (true)
            {
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
                        printSortedKVPairs(tripDataBase.OrderBy(trip => float.Parse(trip.Value[4])));
                        break;
                    case '3':
                        printSortedKVPairs(tripDataBase.OrderByDescending(trip => float.Parse(trip.Value[4])));
                        break;
                    case '4':
                        printSortedKVPairs(tripDataBase.OrderBy(trip => float.Parse(trip.Value[1])));
                        break;
                    case '5':
                        printSortedKVPairs(tripDataBase.OrderByDescending(trip => float.Parse(trip.Value[1])));
                        break;
                    case '6':
                        printSortedKVPairs(tripDataBase.OrderBy(trip => DateTime.ParseExact(trip.Value[0], "dd-MM-yyyy", null)));
                        break;
                    case '7':
                        printSortedKVPairs(tripDataBase.OrderByDescending(trip => DateTime.ParseExact(trip.Value[0], "dd-MM-yyyy", null)));
                        break;
                    case '0':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nPogresan unos. Pritisnite enter zatim pokušajte ponovo.");
                        Console.ReadKey();
                        for (int i = 0; i < 50; i++) Console.WriteLine();
                        break;
                }
            }
        }
        static void analasysScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1-Ukupna potrošnja goriva\n2-Ukupni troškovi goriva\n3-Prosječna potrošnja goriva u L/100km\n");
                Console.Write("4-Putovanje s najvećom potrošnjom goriva\n5-Pregled putovanja po određenom datumu\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        var userId = getAndValidateUserId(userDataBase);
                        var fuelSum = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 2, userId);
                        Console.WriteLine($"\nUkupna potrošnja goriva za korisnika iznosi: {fuelSum} L");
                        Console.ReadKey();
                        break;
                    case '2':
                        var userId2 = getAndValidateUserId(userDataBase);
                        Console.Clear();
                        var fuelCost = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 4, userId2);
                        Console.WriteLine($"\nUkupni troškovi goriva za korisnika iznosi: {fuelCost} eur");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        var userId3 = getAndValidateUserId(userDataBase);
                        var totalFuel = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 2, userId3);
                        var totalDistance = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 1, userId3);

                        if (totalDistance != 0)
                        {
                            var averageConsumption = (totalFuel / totalDistance) * 100;
                            Console.WriteLine($"\nProsječna potrošnja goriva za korisnika iznosi: {averageConsumption} L/100km");
                        }
                        else
                        {
                            Console.WriteLine("\nKorisnik nije imao prijeđenih kilometara pa se prosječna potrošnja ne može izračunati.");
                        }
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.Clear();
                        var maxFuelUsageTripId = getMaxVaribaleTripId(userDataBase, tripDataBase, userTripRelationship, 2);
                        if (maxFuelUsageTripId == -1)
                            break;

                        Console.WriteLine($"\nPutovanje s najvecom potrosnjom goriva iznosi");
                        Console.WriteLine($"Putovanje{maxFuelUsageTripId}#\nDatum:{tripDataBase[maxFuelUsageTripId][0]}\nKilometri:{tripDataBase[maxFuelUsageTripId][1]} km \nGorivo:{tripDataBase[maxFuelUsageTripId][2]} l\nCijena/L:{tripDataBase[maxFuelUsageTripId][3]} eur/l\nCijena:{tripDataBase[maxFuelUsageTripId][4]} eur\n\n");
                        Console.ReadKey();
                        break;
                    case '5':
                        var inputId = getAndValidateUserId(userDataBase);
                        Console.Clear();

                        if (userTripRelationship.ContainsKey(inputId) && userTripRelationship[inputId].Count() != 0)
                        {
                            var inputDateLowerBound = getAndValidateInputDate("datum od kojeg zelite pretragu");
                            var inputDateUpperBound = getAndValidateInputDate("datum do kojeg zelite pretragu");

                            if (DateTime.ParseExact(inputDateLowerBound, "dd-MM-yyyy", null) > DateTime.ParseExact(inputDateUpperBound, "dd-MM-yyyy", null))
                            {
                                Console.WriteLine("Donji datum ne moze biti veci od gornjeg datuma");
                                Console.ReadKey();
                                break;
                            }
                            foreach (var tripId in userTripRelationship[inputId])
                            {
                                var tripDate = DateTime.ParseExact(tripDataBase[tripId][0], "dd-MM-yyyy", null);
                                if (tripDate >= DateTime.ParseExact(inputDateLowerBound, "dd-MM-yyyy", null) && tripDate <= DateTime.ParseExact(inputDateUpperBound, "dd-MM-yyyy", null))
                                {
                                    Console.Write($"Putovanje{tripId}#\nDatum:{tripDataBase[tripId][0]}\nKilometri:{tripDataBase[tripId][1]} km \nGorivo:{tripDataBase[tripId][2]} l\nCijena/L:{tripDataBase[tripId][3]} eur/l\nCijena:{tripDataBase[tripId][4]} eur\n\n");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Korisnik nema putovanja");
                        }
                        Console.ReadKey();
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
        static float calculateVariableUsage(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship, int variableId, int userId)
        {
            Console.Clear();

            var sum = 0.0f;
            if (!userTripRelationship.ContainsKey(userId) || userTripRelationship[userId] == null || userTripRelationship[userId].Count == 0)
            {
                Console.WriteLine("Uneseni korisnik jos nije bio na putu");

                return 0;
            }
            foreach (var tripId in userTripRelationship[userId])
            {
                sum += float.Parse(tripDataBase[tripId][variableId]);
            }
            return sum;
        }
        static int getMaxVaribaleTripId(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship, int variableId)
        {
            var userId = getAndValidateUserId(userDataBase);
            var allVariables = new List<float>();
            if (!userTripRelationship.ContainsKey(userId) || userTripRelationship[userId] == null || userTripRelationship[userId].Count == 0)
            {
                Console.WriteLine("Uneseni korisnik jos nije bio na putu");
                Console.ReadKey();
                return -1;
            }
            int maxTripId = -1;
            float maxValue = float.MinValue;

            foreach (var tripId in userTripRelationship[userId])
            {
                float currentValue = float.Parse(tripDataBase[tripId][variableId]);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxTripId = tripId;
                }
            }
            return maxTripId;
        }
        static void deleteTripsByCost(Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship, int multiplyer)
        {
            var inputCostStr = getAndValidateInputInt("iznos");
            var inputCost = float.Parse(inputCostStr);

            while (true)
            {
                Console.Clear();
                Console.Write("Jeste Li sigurni?(Da/Ne)\nInput:");
                switch (Console.ReadLine().ToString())
                {
                    case "Da":
                    case "da":
                    case "DA":
                        foreach (var trip in tripDataBase.ToList())
                        {
                            var tripCost = float.Parse(trip.Value[4]);
                            if (multiplyer * inputCost < multiplyer * tripCost)
                            {
                                tripDataBase.Remove(trip.Key);
                                deleteTripUserRelationship(trip.Key, userTripRelationship);
                            }
                        }
                        Console.WriteLine("Uspijesno izbrisano");

                        return;
                    case "Ne":
                    case "ne":
                    case "NE":
                        Console.WriteLine("Radnja odbacena");
                        Console.ReadKey();
                        return;
                    default:
                        Console.WriteLine("Neispravan unos pokusajte ponovno"); Console.ReadKey(); break;
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
        static string getAndValidateInputStr(string inputType)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\nUnesite {inputType}: ");
                var toReturn = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(toReturn) || toReturn.Length < 2 || toReturn.Length > 20 || ContainsSpecialCharacters(toReturn))
                { Console.Write($"\n{inputType} nesmije biti prazno, krace od 2 slova te duze od 20 i nesmije sadrzavati posebne znakove!!! Pritisnite enter te pokusajte ponovno"); Console.ReadKey(); continue; }
                return toReturn;
            }
        }
        static string getAndValidateInputDate(string inputType)
        {
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
        static string getAndValidateInputInt(string inputType)
        {
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
        static int getAndValidateUserId(Dictionary<int, string[]> userDataBase)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("\nUnesite id korisnika: ");
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
        static int getAndValidateTripId(Dictionary<int, string[]> tripDataBase)
        {
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
        static void printSortedKVPairs(IOrderedEnumerable<KeyValuePair<int, string[]>> listToPrint)
        {
            Console.Clear();
            foreach (var trip in listToPrint)
            {
                Console.Write($"Putovanje{trip.Key}#\nDatum:{trip.Value[0]}\nKilometri:{trip.Value[1]} km \nGorivo:{trip.Value[2]} l\nCijena/L:{trip.Value[3]} eur/l\nCijena:{trip.Value[4]} eur\n\n");
            }
            Console.ReadKey();
            Console.Clear();
            for (int i = 0; i < 50; i++) Console.WriteLine();
        }
        //BONUS
        static void statsScreen(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            while (true)
            {
                Console.Clear();
                Console.Write("1 - Korisnik s najvećim ukupnim troškom goriva\r\n2 - Korisnik s najviše putovanja\r\n3 - Prosječan broj putovanja po korisniku\r\n4 - Ukupan broj prijeđenih kilometara svih korisnika\r\n0 - Povratak na glavni izbornik\r\nUnos:");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        if (userDataBase.Count() != 0 && tripDataBase.Count() != 0)
                        {
                            maxCostUser(userDataBase, tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.WriteLine("Nemoguce napraviti ispis (nema korisnika/putovanja)");
                        }
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        if (userDataBase.Count() != 0 && tripDataBase.Count() != 0)
                        {
                            maxTripsUser(userDataBase, tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.WriteLine("Nemoguce napraviti ispis (nema korisnika/putovanja)");
                        }
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.Clear();
                        if (userDataBase.Count() != 0 && tripDataBase.Count() != 0)
                        {
                            avgTripsPerUser(userDataBase, tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.WriteLine("Nemoguce napraviti ispis (nema korisnika/putovanja)");
                        }
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.Clear();
                        if (userDataBase.Count() != 0 && tripDataBase.Count() != 0)
                        {
                            totalNumOfKms(userDataBase, tripDataBase, userTripRelationship);
                        }
                        else
                        {
                            Console.WriteLine("Nemoguce napraviti ispis (nema korisnika/putovanja)");
                        }
                        Console.ReadKey();
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

        static void maxCostUser(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            var listOfUsage = new Dictionary<int, float>();
            foreach (var user in userTripRelationship)
            {
                listOfUsage[user.Key] = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 4, user.Key);
            }
            var maxKey = listOfUsage.OrderByDescending(x => x.Value).First().Key;

            Console.WriteLine("Korisnik s najvećim ukupnim troskom goriva je:");
            Console.WriteLine($"{maxKey} - {userDataBase[maxKey][0]} - {userDataBase[maxKey][1]} - {userDataBase[maxKey][2]}");
        }
        static void maxTripsUser(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            var maxTripsId = userTripRelationship.OrderByDescending(x => x.Value.Count).First().Key;

            Console.WriteLine("Korisnik s najvećim brojem putovanja je:");
            Console.WriteLine($"{maxTripsId} - {userDataBase[maxTripsId][0]} - {userDataBase[maxTripsId][1]} - {userDataBase[maxTripsId][2]}");
        }
        static void avgTripsPerUser(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship)
        {
            var totalTrips = float.Parse(tripDataBase.Count().ToString());
            var totalUsers = float.Parse(userDataBase.Count().ToString());
            Console.WriteLine($"Prosjecan broj putovanja po korisniku je {totalTrips/totalUsers}");

        }
        static void totalNumOfKms(Dictionary<int, string[]> userDataBase, Dictionary<int, string[]> tripDataBase, Dictionary<int, List<int>> userTripRelationship) {
            var listOfKms = new Dictionary<int, float>();
            foreach (var user in userTripRelationship)
            {
                listOfKms[user.Key] = calculateVariableUsage(userDataBase, tripDataBase, userTripRelationship, 1, user.Key);
            }
            var totalKms = listOfKms.Values.Sum();

            Console.WriteLine($"Ukupni kilometri za sve korisnike su: {totalKms}");
        }
    }
}
