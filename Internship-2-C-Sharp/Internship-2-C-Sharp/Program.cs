using System;
using System.Runtime;
using System.Numerics;
using System.IO;
using System.Text;
using System.Collections.Immutable;
using System.Linq;
using System.ComponentModel.DataAnnotations;
// Global data
var Table = new Dictionary<string, (int points, int goals)>()
{
    {"Hrvatska",(0,0) },
    {"Belgija",(0,0) },
    {"Kanada",(0,0) },
    {"Maroko",(0,0) }
};
var Players = new Dictionary<string, (string position, int rating)>() {
    {"Luka Modrić", ("MF",88) },
    {"Marcelo Brozović", ("MF",86) },
    {"Mateo Kovacic", ("MF",84) },
    {"Ivan Perišić", ("FW",84) },
    {"Andrej Kramarić", ("FW",82) },
    {"Joško Gvardiol", ("DF",81) },
    {"Mario Pašalić", ("MF",81) },
    {"Lovro Majer", ("MF",80) },
    {"Borna Sosa", ("DF",78) },
    {"Nikola Vlašić", ("MF",78) },
    {"Dejan Lovren", ("DF",78) },
    {"Mislav Oršić", ("FW",77) },
    {"Marko Livaja", ("FW",77) },
    {"Domagoj Vida", ("DF",76) },
    {"Ante Budimir", ("FW",76) },
    {"Martin Erlić", ("DF",74) },
    {"Bruno Petković", ("FW",75) },
    {"Josip Stanišič ", ("DF",72) },
    {"Ivo Grbić", ("GK",78) },
    {"Dominik Livaković", ("GK",80) }
};
var Keys = new List<string>()
{
    "Luka Modrić",
    "Marcelo Brozović",
    "Mateo Kovacic",
    "Ivan Perišić",
    "Andrej Kramarić",
    "Joško Gvardiol",
    "Mario Pašalić",
    "Lovro Majer",
    "Borna Sosa",
    "Nikola Vlašić",
    "Dejan Lovren",
    "Mislav Oršić",
    "Marko Livaja",
    "Domagoj Vida",
    "Ante Budimir",
    "Martin Erlić",
    "Bruno Petković",
    "Josip Stanišič ",
    "Ivo Grbić",
    "Dominik Livaković"
};
var First11 = new Dictionary<string, (string position, int rating)>();
var Games = new Dictionary<int, (string team1, string team2, int goals1, int goals2)>(){
    {0, ("Hrvatska","Maroko",0,0)},
    {1, ("Belgija", "Kanada",0,0)},
    {3, ("Belgija", "Maroko",0,0)},
    {2, ("Hrvatska", "Kanada",0,0)},
    {4, ("Hrvatska", "Belgija",0,0)},
    {5, ("Kanada", "Maroko",0,0)}
};
var Scorers = new Dictionary<string, int>();
var poorTeam = false;
var gamesCounter = 0;
var lineUp = new List<string>() { "", "", "", "", "" };
Random random = new Random();
PrintManual();
var command = -1;
Console.WriteLine("\nUnesite broj ispred trazenog odabira: ");
command = int.Parse(Console.ReadLine());
while (command != 0)
{
    Console.Clear();
    switch (command)
    {
        case 1:
            var i = 1;
            var oldRating = 0;
            Console.WriteLine($"    REZULTAT TRENINGA\n\n {" ",2} {"IME I PREZIME",-20} {"POZICIJA",-15} {"STARI RATING",-15}  {"NOVI RATING",-15}  {"REZULTAT",-15}");
            /*foreach (var player in Players)
            {
                oldRating = player.Value.rating;
                //double r= player.Value.rating+oldRating*random.Next(-5,6)*0.01;
                var newRating = ChangeRating(oldRating);
                Players[player.Key]= (player.Value.position,newRating);
                 if (player.Value.rating > 100)
                    Players[player.Key] = (player.Value.position, 100);
                if (player.Value.rating < 1)
                    Players[player.Key] = (player.Value.position, 1);
                Console.WriteLine($" {i,2} {player.Key,-20} {player.Value.position,-15} {oldRating,-15}  {player.Value.rating,-15}");
                i++;
            }*/
            for (int j = 0; j < Players.Count; j++)
            {
                oldRating = Players[Keys[j]].rating;
                //double r= player.Value.rating+oldRating*random.Next(-5,6)*0.01;
                var newRating = ChangeRating(oldRating);
                Players[Keys[j]] = (Players[Keys[j]].position, newRating);
                if (Players[Keys[j]].rating > 100)
                    Players[Keys[j]] = (Players[Keys[j]].position, 100);
                if (Players[Keys[j]].rating < 1)
                    Players[Keys[j]] = (Players[Keys[j]].position, 1);
                Console.WriteLine($" {i,2} {Keys[j],-20} {Players[Keys[j]].position,-15} {oldRating,-15}  {Players[Keys[j]].rating,-15}  {Players[Keys[j]].rating - oldRating,2}");
                i++;
            }
            Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
            Console.ReadKey();
            Console.Clear();
            break;
        case 2:
            Console.Clear();
            gamesCounter += 2;
            if (gamesCounter > 6)
            {
                Console.WriteLine("Sve utakmice su odigrane. ");
                Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            if (Players.Count < 11)
            {
                Console.WriteLine("Nema dovoljno igrača za utakmicu.\n");
                Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                Console.ReadKey();
                Console.Clear();
                break;
            }
            First11.Clear();
            CreateFirst11();
            CreateGames();
            Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
            Console.ReadKey();
            Console.Clear();
            break;
        case 3:
            Console.Clear();
            var secondaryCommand = 0;
            PrintStatisticsManual();
            Console.WriteLine("\nUnesite broj iz ponudenog odabira: ");
            secondaryCommand = int.Parse(Console.ReadLine());
            switch (secondaryCommand)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"  SPREMLJENI IGRAČI\n\n {" ",2} {"IME I PREZIME",-20} {"POZICIJA",-15} {"RATING",-15}\n");
                    for (i = 0; i < Players.Count(); i++)
                    {
                        Console.WriteLine($" {i + 1,2} {Keys[i],-20} {Players[Keys[i]].position,-15} {Players[Keys[i]].rating,-15}");
                    }
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    i = 0;
                    Console.WriteLine("  IGRAČI PO UZLAZNOM RATINGU\n");
                    foreach (var player in Players.OrderBy(player => player.Value.rating)) { 
                        Console.WriteLine($" {i + 1,2} {player.Key,-20} {player.Value.position,-15} {player.Value.rating,-15}");
                        i++;
                    }
                    /*var sortedPlayers = Players.Values.OrderBy(s => s.rating).OrderBy(s=>s.position).Reverse();
                    for (int i = 0; i < sortedPlayers.Count ; i++) {
                        if (Players[Keys[i]].rating >= Players[Keys[i + 1]].rating) {
                            var temp = new Dictionary<string, (string position, int rating)>();
                            temp.Add(Keys[i + 1] , (Players[Keys[i + 1]].position , Players[Keys[i + 1]].rating));
                            Players[Keys[i]] = Players[Keys[i + 1]];
                            Players[Keys[i + 1]] = temp[Keys[i + 1]];
                        }

                    for (i = 0; i < sortedPlayers.Count(); i++)
                    {
                        Console.WriteLine($" {i + 1,2} {Keys[i],-20} {Players[Keys[i]].position,-15} {Players[Keys[i]].rating,-15}");
                    }}*/
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    i = 0;
                    Console.WriteLine("  IGRAČI PO UZLAZNOM RATINGU\n");
                    foreach (var player in Players.OrderBy(player => player.Value.rating).Reverse())
                    {
                        Console.WriteLine($" {i + 1,2} {player.Key,-20} {player.Value.position,-15} {player.Value.rating,-15}");
                        i++;
                    }
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 4:
                    Console.Clear();
                    i = 0;
                    string name = "";
                    Console.WriteLine("Unesite traženo ime i prezime: ");
                    name= Console.ReadLine();
                    foreach(var player in Players)
                    {
                        if (String.Equals(player.Key.ToUpper(), name.ToUpper()))
                        {
                            i++;
                            Console.WriteLine($"\nIgrač pronađen: {player.Key}  {player.Value.position}  {player.Value.rating}");
                        }
                    }
                    if(i==0)
                        Console.WriteLine("Igrač s tim imenom nije pronađen.");
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 5:
                    Console.Clear();
                    i = 0;
                    int rat=0;
                    Console.WriteLine("Unesite traženi rating: ");
                    rat = int.Parse(Console.ReadLine());
                    Console.Clear();
                    foreach (var player in Players)
                    {
                        if (player.Value.rating==rat)
                        {
                            i++;
                            if (i == 1)
                                Console.WriteLine($"\n  IGRAČI SA RATINGOM {rat}: \n");
                            Console.WriteLine($"  {player.Key,-15} {player.Value.position,10}");
                        }
                    }
                    if (i == 0)
                        Console.WriteLine("Igrač s tim ratingom ne postoji. ");
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 6:
                    Console.Clear();
                    i = 0;
                    string position = "";
                    Console.WriteLine("Unesite traženu poziciju: ");
                    position = Console.ReadLine();
                    Console.Clear();
                    foreach (var player in Players)
                    {
                        if (String.Equals(player.Value.position.ToUpper(), position.ToUpper()))
                        {
                            i++;
                            if(i==1)
                                Console.WriteLine($"\n  IGRAČI NA POZICIJI {position.ToUpper()}: \n");
                            Console.WriteLine($"  {player.Key,-15} {player.Value.rating,10}");
                        }
                    }
                    if (i == 0)
                        Console.WriteLine("Igrač s tom pozicijom ne postoji. ");
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 7:
                    Console.Clear();
                    CreateFirst11();
                    PrintLineUp();
                    
                    
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 8:
                    Console.Clear();
                    if (Scorers.Count == 0)
                    {
                        Console.WriteLine("Nema spremljenih strijelaca.");
                        Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    Console.WriteLine("\n  SRIJELCI:\n");
                    foreach(var player in Scorers.OrderBy(Scorers => Scorers.Value).Reverse()) 
                    {
                        Console.WriteLine($"  {player.Key,-20} {player.Value,-10}");
                    }
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 9:
                    i = gamesCounter;
                    Console.Clear();
                    if (gamesCounter == 0)
                    {
                        Console.WriteLine("Nema odigranih utakmica.");
                        Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    Console.WriteLine("\n  REZULTATI HRVATSKE:\n");
                    while (i>0) { 
                        Console.WriteLine( $"\t{Games[gamesCounter - i].team1,8} {Games[gamesCounter - i].goals1} : {Games[gamesCounter - i].goals2} {Games[gamesCounter - i].team2,-8} ");
                        i -= 2;
                    }
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 10:
                    i = gamesCounter;
                    Console.Clear();
                    if (gamesCounter == 0)
                    {
                        Console.WriteLine("Nema odigranih utakmica.");
                        Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    Console.WriteLine("\n  REZULTATI SVIH UTAKMICA:\n");
                    while (i > 0)
                    {
                        Console.WriteLine($"\t{Games[gamesCounter - i].team1,8} {Games[gamesCounter - i].goals1} : {Games[gamesCounter - i].goals2} {Games[gamesCounter - i].team2,-8}\n" +
                            $"\t{Games[gamesCounter - i+1].team1,8} {Games[gamesCounter - i+1].goals1} : {Games[gamesCounter - i+1].goals2} {Games[gamesCounter - i+1].team2,-8}");
                        i -= 2;
                    }
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 11:
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    break;
            }
            break;
        case 4:
            Console.Clear();
            Print4();
            var command4 = 0;
            Console.WriteLine("\nUnesite broj iz ponudenog odabira: ");
            command4 = int.Parse(Console.ReadLine());
            switch (command4) { 
                case 1:
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine(
                    "\n1 - Uredi ime i prezime igrača"+ 
                    "\n2 - Uredi poziciju igrača(GK, DF, MF ili FW)"+
                    "\n3 - Uredi rating igrača(od 1 do 100)");
                    Console.WriteLine("\nUnesite bilo koji znak za vratiti se na izbornik.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 0:
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    break;
            }

            break;
        case 0:
            Console.Clear();
            break;
        default:
            Console.Clear();
            Console.WriteLine("Krivi Unos.\n");
            break;
    }
    PrintManual();
    Console.WriteLine("\nUnesite broj ispred trazenog odabira: ");
    command = int.Parse(Console.ReadLine());
}



// Functions
void PrintManual()
{
    var manual =
    "1 - Odradi trening\n" +
    "2 - Odigraj utakmicu\n" +
    "3 - Statistika\n" +
    "4 - Kontrola igraca\n" +
    "0 - Izlaz iz aplikacije";
    Console.WriteLine(manual);
}
int ChangeRating(int currentRating)
{
    Random random = new Random();
    var diff = random.Next(-5, 6);
    int newRating = (int)(currentRating + (int)diff);
    return newRating;
}
void CreateFirst11()
{
    First11.Clear();
    int gk = 0,
        df = 0,
        mf = 0,
        fw = 0;
    foreach (var player in Players.OrderBy(player => player.Value.rating).Reverse())
    {
        if (gk != 1 && player.Value.position == "GK")
        {
            First11.Add(player.Key, (player.Value.position, player.Value.rating));
            gk++;
        }
        if (df != 4 && player.Value.position == "DF")
        {
            First11.Add(player.Key, (player.Value.position, player.Value.rating));
            df++;
        }
        if (mf != 3 && player.Value.position == "MF")
        {
            First11.Add(player.Key, (player.Value.position, player.Value.rating));
            mf++;
        }
        if (fw != 3 && player.Value.position == "FW")
        {
            First11.Add(player.Key, (player.Value.position, player.Value.rating));
            fw++;
        }
    }
    if (First11.Count != 11) //Ako nema dovoljno igraca na svim pozicijama,samo stavi 11 najboljih
    {
        First11.Clear();
        poorTeam = true;
        int y = 0;
        foreach (var player in Players.OrderBy(player => player.Value.rating).Reverse())
        {
            First11.Add(player.Key, (player.Value.position, player.Value.rating));
            y++;
            if (y == 11)
                break;
        }
    }
}
void CreateGames()
{
    PrintLineUp();
    Random random = new Random();
    Games[gamesCounter - 2] = (Games[gamesCounter - 2].team1, Games[gamesCounter - 2].team2, random.Next(0, 5), random.Next(0, 5));
    Games[gamesCounter - 1] = (Games[gamesCounter - 1].team1, Games[gamesCounter - 1].team2, random.Next(0, 5), random.Next(0, 5));
    Console.WriteLine($"\n  ODIGRANE UTAKMICE:\n\n" +
        $"\t{Games[gamesCounter - 2].team1,8} {Games[gamesCounter - 2].goals1} : {Games[gamesCounter - 2].goals2} {Games[gamesCounter - 2].team2,-8} \n" +
        $"\t{Games[gamesCounter - 1].team1,8} {Games[gamesCounter - 1].goals1} : {Games[gamesCounter - 1].goals2} {Games[gamesCounter - 1].team2,-8}  ");
    /*   if (Games[gamesCounter - 2].goals1 > Games[gamesCounter - 2].goals2)
           foreach (var player in First11)
               Players[player.Key].rating += (double)Players[player.Key].rating * 0.02;
       if (Games[gamesCounter - 2].goals1 < Games[gamesCounter - 2].goals2)
           foreach (var player in First11)
               Players[player.Key].rating += (double)Players[player.Key].rating * 0.02;
     */
    if (Games[gamesCounter - 2].goals1 > 0)
    {
        Console.WriteLine("\n  STRIJELCI:");
        for (var i = 0; i < Games[gamesCounter - 2].goals1; i++)
        {
            lineUp[i] = First11.ElementAt(random.Next(11)).Key;
            //Players[lineUp[i]].rating += Players[lineUp[i]].rating * 0.05;
            if (Scorers.Keys.Contains(lineUp[i]))
                Scorers[lineUp[i]]++;
            else
                Scorers.Add(lineUp[i], 1);
            Console.WriteLine("    " + lineUp[i]);
        }
    }
    //Match(Games[gamesCounter - 2].team1, Games[gamesCounter - 2].team2, Games[gamesCounter - 2].goals1, Games[gamesCounter - 2].goals2);
}
void PrintStatisticsManual()
{
    var manual =
    "\n1 - Ispis onako kako su spremljeni\n" +
    "2 - Ispis po rating uzlazno\n" +
    "3 - Ispis po ratingu silazno\n" +
    "4 - Ispis igrača po imenu i prezimenu\n" +
    "5 - Ispis igrača po ratingu \n" +
    "6 - Ispis igrača po poziciji \n" +
    "7 - Ispis trenutnih prvih 11 igrača\n" +
    "8 - Ispis strijelaca i koliko golova imaju\n" +
    "9 - Ispis svih rezultata ekipe\n" +
    "10 - Ispis rezultat svih ekipa\n" +
    "11 - Ispis tablice grupe (mjesto na tablici, ime ekipe, broj bodova, gol razlika)\n" +
    "0 - Povratak na glavni izbornik\n";
    Console.WriteLine(manual);
}
void Print4()
{
    var manual =
    "\n1 - Unos novog igrača\n" +
    "2 - Brisanje igrača\n" +
    "3 - Uređivanje igrača\n" +
    "0 - Povratak na glavni izbornik\n";
    Console.WriteLine(manual);
}
void PrintLineUp()
{
    if (poorTeam == false)
    {
        var i = 0;

        Console.WriteLine($"\n  POČETNI SASTAV:\n    ");
        foreach (var player in First11)
        {
            if (player.Value.position == "FW")
            {
                lineUp[i] = player.Key.Split(" ").Last();
                i++;
            }

        }
        i = 0;
        Console.WriteLine($"        {lineUp[0],-15}{lineUp[1],-15}{lineUp[2],-15}\n");
        foreach (var player in First11)
        {
            if (player.Value.position == "MF")
            {
                lineUp[i] = player.Key.Split(" ").Last();
                i++;
            }

        }
        i = 0;
        Console.WriteLine($"        {lineUp[0],-15}{lineUp[1],-15}{lineUp[2],-15}\n");
        foreach (var player in First11)
        {
            if (player.Value.position == "DF")
            {
                lineUp[i] = player.Key.Split(" ").Last();
                i++;
            }
            if (player.Value.position == "GK")
                lineUp[4] = player.Key.Split(" ").Last();


        }
        i = 0;
        Console.WriteLine($"  {lineUp[0],-15}{lineUp[1],-15}{lineUp[2],-15}{lineUp[3],-15}\n");
        Console.WriteLine($"{lineUp[4],32}");
    }
    else
    {
        var i = 0;
        Console.WriteLine($"\n  POČETNI SASTAV:\n    ");
        foreach (var player in Players)
        {
            if (i == 11)
                break;
            Console.WriteLine($" {i + 1,2} {player.Key,-20} {player.Value.position,-15} {player.Value.rating,-15}");
            i++;
        }
    }   
}
/*void Match(string team1,string team2,int goals1,int goals2)
{
    if (goals1 > goals2)
    {
        Table[team1] = (Table[team1], (Table[team1].points, Table[team1].goals));
        Table[team1].goals += (goals1-goals2);
        Table[team2].goals -= (goals1 - goals2);
    }
    if (goals1 < goals2)
    {
        Table[team2].points += 3;
        Table[team1].goals += (goals1 - goals2);
        Table[team2].goals -= (goals1 - goals2);
    }
    else
    {
        Table[team2].points += 1;
        Table[team2].points +=1;
    }
}*/