namespace Garage {

    internal class UIManager(Garage i_Garage) {
        private Garage Garage { get; } = i_Garage;

        public void Start() {
            Console.WriteLine("Welcome to the Garage!");
        }
    }
}