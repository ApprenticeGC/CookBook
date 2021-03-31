using System;

namespace UseSerilog
{
    using Serilog;
    using Serilog.Formatting.Compact;

    public class Position3
    {
        public float x;
        public float y;
        public float z;

        public override string ToString()
        {
            var desc = $"{x}, {y}, {z}";
            return desc;
        }
    }

    public class Npc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CombatStatus CombatStatus { get; set; }

        public override string ToString()
        {
            var desc = $"Id: {Id} Name: {Name} CombatStatus: {CombatStatus}";
            return desc;
        }
    }

    public class CombatStatus
    {
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public override string ToString()
        {
            var desc = $"Hp: {Hp} Attack: {Attack}, Defense: {Defense}";
            return desc;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "log.txt")
                // .WriteTo.File("log.txt")
                .CreateLogger();

            var position = new Position3();
            position.x = 1.0f;
            position.y = 2.0f;
            position.z = 3.0f;

            log.Information("Hello Serilog, {position}", position);
            log.Information("Hello Serilog, {@position}", position);

            var fruit = new[] {"Apple", "Pear"};
            log.Information("Hello Serilog, {fruit}", fruit);
            // Console.WriteLine("Hello World!");

            var gameLog = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "{Message:lj}")
                // .WriteTo.File(new CompactJsonFormatter(), "log.txt")
                .CreateLogger();

            var combatStatus = new CombatStatus
            {
                Hp = 10,
                Attack = 2,
                Defense = 3
            };
            var npc = new Npc
            {
                Id = 1000,
                Name = "Jack",
                CombatStatus = combatStatus
            };
            log.Information("Hello Serilog, {npc}", npc);
            log.Information("Hello Serilog, {@npc}", npc);
        }
    }
}
