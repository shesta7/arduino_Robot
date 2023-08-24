using System.IO.Ports;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] ports = SerialPort.GetPortNames();

        for (var i = 0; i < ports.Length; i++)
            Console.WriteLine($"{i + 1}. {ports[i]}");
        Console.WriteLine($"{Environment.NewLine}Type a port number to connect and press enter. Type 'exit' to exit program");

        int portChosen = -1;

        while (portChosen == -1)
        {
            var userPortChose = Console.ReadLine();

            if (userPortChose?.Trim() == "exit")
                return;

            int portNum;

            try
            {
                portNum = int.Parse(userPortChose ?? "");
                if (portNum < 1 && portNum > ports.Length)
                    throw new Exception();

                portChosen = portNum;
            }
            catch
            {
                Console.WriteLine($"{Environment.NewLine}Invalid port number. Try another one");
            }
        }

        Console.WriteLine($"{Environment.NewLine}Connected to port {ports[portChosen - 1]}");

        SerialPort currentPort = new SerialPort(ports[portChosen - 1], 9600);
        currentPort.BaudRate = 9600;
        currentPort.DtrEnable = true;
        currentPort.ReadTimeout = 1000;
        bool IsExit = false;

        Console.WriteLine($"Type a command:{Environment.NewLine} {Environment.NewLine} {Environment.NewLine} 1. Rotate motor: motor number[1-16] - angle in degrees [0-180] (Example: 1-80){Environment.NewLine} 2. Delay - [255]-[100* value in ms] (Example 255-1 means 100ms delay)");
        try
        {

            currentPort.Open();
            Thread.Sleep(1000);

            while (!IsExit)
            {
                var command = Console.ReadLine();
                if (command == "exit")
                {
                    IsExit = true;
                    return;
                }

                var pairs = command?.Split('-');

                if (pairs?.Count() == 2)
                {
                    byte[] bytes =
                    {
                (byte)short.Parse(pairs[0]),
                (byte)short.Parse(pairs[1]),
            };
                    currentPort.Write(bytes, 0, bytes.Count());
                }
            }
            Thread.Sleep(100);
        }
        catch { }
        currentPort.Close();
    }
}