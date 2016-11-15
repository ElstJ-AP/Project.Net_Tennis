namespace TennisDomain.DataModel.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TennisDomain.Classes;
    using TennisDomain.Classes.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<TennisDomain.DataModel.TennisContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TennisDomain.DataModel.TennisContext context)
        {
            
            var roles = new List<Role> {
                new Role { Id = 1,Name = "League Manager" },
                new Role { Id = 2,Name = "Treasurer" },
                new Role { Id = 3,Name = "Player" },
                new Role { Id = 4,Name = "Drunk" }
            };

            roles.ForEach(r => context.Roles.AddOrUpdate(p => p.Name, r)); 
            context.SaveChanges();

            var divisions = new List<Division> {
                  new Division { Id = 1,Name = "First" },
                  new Division { Id = 2,Name = "Second" }
            };
            divisions.ForEach(r => context.Divisions.AddOrUpdate(p => p.Name, r));
            context.SaveChanges();

            var teams = new List<Team> {
                new Team { Id = 1,Name = "Springfield Slammers"},
                new Team { Id = 2,Name = "Nuclear Plant Exploders"},
                new Team { Id = 3,Name = "Moe's Drinkers"},
                new Team { Id = 4,Name = "Springfield Women Team"},
            };
            teams.ForEach(r => context.Teams.AddOrUpdate(p => p.Name, r));
            context.SaveChanges();
            
            var players = new List<Player> {
                new Player { Id = 1, PlayerNr = 1, LastName = "Simpson", FirstName = "Homer", Gender = Gender.Male, JoinYear = 1989, Street = "Evergreen Terrace", HouseNr = 742, City = "Springfield", ZipCode = "58008", FederationNr = 1, Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 4)}, Fines = new List<Fine> { new Fine { FineDate=DateTime.Today.Date, Amount=100}} },
                new Player { Id = 2, PlayerNr = 2, LastName = "Simpson", FirstName = "Marge", Gender = Gender.Female, JoinYear = 1989, Street = "Evergreen Terrace", HouseNr = 742, City = "Springfield", ZipCode = "58008", FederationNr = 2, Roles = new List<Role> {roles.Single(s => s.Id == 3)} },
                new Player { Id = 3, PlayerNr = 3, LastName = "Simpson", FirstName = "Bart", Gender = Gender.Male, JoinYear = 1989, Street = "Evergreen Terrace", HouseNr = 742, City = "Springfield", ZipCode = "58008", FederationNr = 3,Roles = new List<Role> {roles.Single(s => s.Id == 3)}, Fines = new List<Fine> { new Fine { FineDate = DateTime.Today.Date, Amount = 100 } } },
                new Player { Id = 4, PlayerNr = 4, LastName = "Simpson", FirstName = "Lisa", Gender = Gender.Female, JoinYear = 1989, Street = "Evergreen Terrace", HouseNr = 742, City = "Springfield", ZipCode = "58008", FederationNr = 4, Roles = new List<Role> {roles.Single(s => s.Id == 3)} },
                new Player { Id = 5, PlayerNr = 5, LastName = "Burns", FirstName = "Montgomery", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 5,Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 1)}, Fines = new List<Fine> { new Fine { FineDate = DateTime.Today.Date, Amount = 100000 } } },
                new Player { Id = 6, PlayerNr = 6, LastName = "Smithers", FirstName = "Waylon", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 6,Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 2)} },
                new Player { Id = 7, PlayerNr = 7, LastName = "Leonard", FirstName = "Lenny", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 7 ,Roles = new List<Role> {roles.Single(s => s.Id == 3), roles.Single(s => s.Id == 4)}, Fines = new List<Fine> { new Fine { FineDate=DateTime.Today.Date, Amount=100}}},
                new Player { Id = 8, PlayerNr = 8, LastName = "Carlson", FirstName = "Carl", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 8 ,Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 4)}, Fines = new List<Fine> { new Fine { FineDate=DateTime.Today.Date, Amount=100}}},
                new Player { Id = 9, PlayerNr = 9, LastName = "Gumble", FirstName = "Barney", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 9 ,Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 4)}, Fines = new List<Fine> { new Fine { FineDate=DateTime.Today.Date, Amount=100}}},
                new Player { Id = 10, PlayerNr = 10, LastName = "Szyslak", FirstName = "Moe", Gender = Gender.Male, JoinYear = 1989, City = "Springfield", ZipCode = "58008", FederationNr = 10 ,Roles = new List<Role> {roles.Single(s => s.Id == 3),roles.Single(s => s.Id == 4)}, Fines = new List<Fine> { new Fine { FineDate=DateTime.Today.Date, Amount=100}}},
            };
            players.ForEach(r => context.Players.AddOrUpdate(p => p.Id, r));
            context.SaveChanges();
            
            var playersteamsdivisions = new List<PlayerTeamDivision>
            {
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 1).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 1).Id, TeamId=2, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 1).Id, TeamId=3, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 2).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 2).Id, TeamId=4, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 3).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 4).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 4).Id, TeamId=4, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 5).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 5).Id, TeamId=2, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 6).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 6).Id, TeamId=2, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 7).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 7).Id, TeamId=2, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 7).Id, TeamId=3, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 8).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 8).Id, TeamId=2, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 8).Id, TeamId=3, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 9).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 9).Id, TeamId=3, DivisionId=2},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 10).Id, TeamId=1, DivisionId=1},
                new PlayerTeamDivision { PlayerId=players.Single(s => s.PlayerNr== 10).Id, TeamId=3, DivisionId=2},
            };
            foreach (var ptd in playersteamsdivisions)
            {
                var ptdInDataBase = context.PlayerTeamDivisions.Where(
                    s =>
                         s.PlayerId == ptd.PlayerId &&
                         s.TeamId == ptd.TeamId &&
                         s.DivisionId == ptd.DivisionId).SingleOrDefault();
                if (ptdInDataBase == null)
                {
                    context.PlayerTeamDivisions.Add(ptd);
                }
            }
            context.SaveChanges();

            var games = new List<Game>
            {
                new Game { Player1Id=1, Player2Id=2, Player1ScoreArray=new int[] {1,1,1,0,0}, Player2ScoreArray=new int[] {6,6,6,0,0}},
                new Game { Player1Id=3, Player2Id=4, Player1ScoreArray=new int[] {6,4,6,6,0}, Player2ScoreArray=new int[] {3,6,2,2,0}},
                new Game { Player1Id=5, Player2Id=6, Player1ScoreArray=new int[] {1,1,6,6,6}, Player2ScoreArray=new int[] {6,6,0,0,0}},
                new Game { Player1Id=7, Player2Id=8, Player1ScoreArray=new int[] {3,6,4,6,4}, Player2ScoreArray=new int[] {6,2,6,3,6}},
                new Game { Player1Id=9, Player2Id=10, Player1ScoreArray=new int[] {1,1,1,0,0}, Player2ScoreArray=new int[] {6,6,6,0,0}},
            };
            foreach (var game in games)
            {
                var gameInDataBase = context.Games.Where(
                    s =>
                         s.Player1Id == game.Player1Id &&
                         s.Player2Id == game.Player2Id).SingleOrDefault();
                if (gameInDataBase == null)
                {
                    context.Games.Add(game);
                }
            }
            context.SaveChanges();
        }
    }
}
