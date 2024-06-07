namespace Garage {

    internal sealed class ElectricCarFactory : CarFactory {
        protected override float MaxEnergy => 3.5f;
        
        protected sealed override Engine getEngineData() => new ElectricEngine(MaxEnergy);
    }
}