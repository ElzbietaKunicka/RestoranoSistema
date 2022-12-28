namespace newRest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customConsole = new CustomConsole();
            var mainApp = new MainApp(customConsole);
            //while (true)
            //{
            //    mainApp.Login();

            //}
            mainApp.Login();
            //Console.WriteLine("pasirinkite staliuka ");
            mainApp.ChooseTable();


            mainApp.CreateOrder(4);
            
        }

    }
}